using ExternalRequestApi.Models;
using ExternalRequestApi.Services;
using Microsoft.Extensions.Options;

// =============================================================================
// EXTERNAL REQUEST API — Interview practice scaffold
// =============================================================================
//
// Run:  dotnet run --project src/Interview/ExternalRequestApi
// Test: use ExternalRequestApi.http in your IDE, or curl/Postman
//
// This app exposes YOUR endpoints that call an EXTERNAL API (JSONPlaceholder).
// The typed client in Services/ is pre-built — you wire up minimal API routes here.
//
// HttpClient cheat sheet (memorize for interviews):
//   GetFromJsonAsync<T>     — GET + deserialize JSON
//   PostAsJsonAsync         — POST JSON body
//   EnsureSuccessStatusCode — throw if not 2xx
//   IHttpClientFactory      — register with AddHttpClient<TInterface, TImpl>()
//
// Why not `new HttpClient()` in a handler?
//   - Disposing HttpClient does not release sockets immediately (socket exhaustion)
//   - Factory manages handler lifetime and supports named/typed clients + Polly retries

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JsonPlaceholderOptions>(
    builder.Configuration.GetSection(JsonPlaceholderOptions.SectionName));

// Typed HttpClient: DI injects IJsonPlaceholderClient; factory manages HttpClient lifetime.
builder.Services.AddHttpClient<IJsonPlaceholderClient, JsonPlaceholderClient>();

var app = builder.Build();

app.UseHttpsRedirection();

// -----------------------------------------------------------------------------
// REFERENCE — health check (no external call; works offline)
// -----------------------------------------------------------------------------
app.MapGet("/external/health", (IOptions<JsonPlaceholderOptions> options) =>
    Results.Ok(new
    {
        status = "ready",
        topic = "external-request-api",
        upstream = options.Value.BaseUrl,
    }));

// =============================================================================
// EXERCISE 1 — GET proxy: list posts from external API
// =============================================================================
// Goal: GET /posts returns posts from JSONPlaceholder with status 200.
//
// Steps:
//   1. app.MapGet("/posts", ...)
//   2. Inject IJsonPlaceholderClient
//   3. await client.GetPostsAsync()
//   4. Return Results.Ok(posts)
//
// Test: GET https://localhost:7288/posts
//
// Interview tip: handlers should be async when calling I/O. Use `async` lambda or
// return Task<IResult> from a named method.

app.MapGet("/posts", async (IJsonPlaceholderClient client, CancellationToken cancellationToken) => {
    var posts = await client.GetPostsAsync(cancellationToken);
    return Results.Ok(posts);
});


// =============================================================================
// EXERCISE 2 — GET by id with upstream 404 mapping
// =============================================================================
// Goal: GET /posts/{id} returns one post or 404 when the external API has no match.
//
// Steps:
//   1. Route "/posts/{id:int}"
//   2. var post = await client.GetPostByIdAsync(id)
//   3. post is null → Results.NotFound(); else → Results.Ok(post)
//
// Test: GET /posts/1 (200) and GET /posts/99999 (404)
//
// Interview tip: map upstream status codes to your API contract. Do not leak raw
// HttpRequestException messages to clients — log details, return ProblemDetails.

app.MapGet("/posts/{id:int}", async (int id, IJsonPlaceholderClient client, CancellationToken cancellationToken) => {
    var post = await client.GetPostByIdAsync(id, cancellationToken);
    return post == null ? Results.NotFound() : Results.Ok(post);
});


// =============================================================================
// EXERCISE 3 — POST: forward create to external API
// =============================================================================
// Goal: POST /posts with JSON body forwards to JSONPlaceholder and returns 201.
//
// Request body example:
//   { "userId": 1, "title": "Hello", "body": "World" }
//
// Steps:
//   1. app.MapPost("/posts", ...)
//   2. Accept CreateExternalPostRequest (JSON body binding)
//   3. Validate Title and Body are not whitespace → Results.BadRequest(...)
//   4. var created = await client.CreatePostAsync(request)
//   5. Return Results.Created($"/posts/{created.Id}", created)
//
// Note: JSONPlaceholder fakes creation (always returns id 101) — fine for practice.
//
// Interview tip: set timeouts on HttpClient (client.Timeout) and propagate
// CancellationToken on long-running upstream calls.

app.MapPost("/posts", async (CreateExternalPostRequest request, IJsonPlaceholderClient client, CancellationToken cancellationToken) => {
    if (string.IsNullOrWhiteSpace(request.Title))
        return Results.BadRequest($"{nameof(request.Title)} is required.");

    if (string.IsNullOrWhiteSpace(request.Body))
        return Results.BadRequest($"{nameof(request.Body)} is required.");

    var created = await client.CreatePostAsync(request, cancellationToken);
    return Results.Created($"/posts/{created.Id}", created);
});

// =============================================================================
// EXERCISE 4 — Query string filter via external API
// =============================================================================
// Goal: GET /posts/user/{userId} returns posts for that user from the upstream API.
//
// Steps:
//   1. app.MapGet("/posts/user/{userId:int}", ...)
//   2. await client.GetPostsByUserIdAsync(userId)
//   3. Return Results.Ok(posts) — empty list is still 200
//
// Test: GET /posts/user/1
//
// Interview tip: prefer letting the external API filter when it supports query params
// rather than fetching everything and filtering in memory.

app.MapGet("/posts/user/{userId:int}", async (int userId, IJsonPlaceholderClient client, CancellationToken cancellationToken) => {
    var posts = await client.GetPostsByUserIdAsync(userId, cancellationToken);
    return Results.Ok(posts);
});


// =============================================================================
// EXERCISE 5 — Parallel external calls (Task.WhenAll)
// =============================================================================
// Goal: GET /posts/{id}/summary returns title + comment count from two upstream calls.
//
// Steps:
//   1. Route "/posts/{id}/summary"
//   2. Start both tasks without awaiting immediately:
//        var postTask = client.GetPostByIdAsync(id);
//        var countTask = client.GetCommentCountForPostAsync(id);
//   3. await Task.WhenAll(postTask, countTask)
//   4. If postTask.Result is null → NotFound()
//   5. Else return Results.Ok(new PostSummary(post.Id, post.Title, countTask.Result))
//
// Test: GET /posts/1/summary
//
// Interview tip: Task.WhenAll reduces latency vs sequential awaits. Mention trade-offs:
//   - more concurrent load on upstream
//   - use CancellationToken to cancel both when client disconnects

app.MapGet("/posts/{id:int}/summary", async (int id, IJsonPlaceholderClient client, CancellationToken cancellationToken) => {
    var postTask = client.GetPostByIdAsync(id, cancellationToken);
    var countTask = client.GetCommentCountForPostAsync(id, cancellationToken);
    await Task.WhenAll(postTask, countTask);

    return postTask.Result == null ? 
        Results.NotFound() : 
        Results.Ok(new PostSummary(
            postTask.Result.Id, 
            postTask.Result.Title,
            countTask.Result
        ));
});

// =============================================================================
// EXERCISE 6 — CancellationToken propagation
// =============================================================================
// Goal: GET /posts honors client disconnect — pass RequestAborted to the client.
//
// Steps:
//   1. app.MapGet("/posts", ...)  (or add a dedicated /posts/cancellable route)
//   2. Add HttpContext httpContext (or CancellationToken cancellationToken) parameter
//   3. await client.GetPostsAsync(httpContext.RequestAborted)
//
// Test: call GET /posts and cancel the request mid-flight (IDE stop or curl Ctrl+C).
//       Upstream work should be cancelled when possible.
//
// Interview tip: ASP.NET Core binds CancellationToken parameters from RequestAborted.
// Always pass tokens through to HttpClient calls in production APIs.

app.MapGet("/posts/cancellable", async (IJsonPlaceholderClient client, CancellationToken cancellationToken) => {
    // add a timeout of 1 second
    cancellationToken.ThrowIfCancellationRequested();
    await Task.Delay(3000, cancellationToken);

    var posts = await client.GetPostsAsync(cancellationToken);
    return Results.Ok(posts);
});


app.Run();
