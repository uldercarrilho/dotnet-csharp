using CreateEndpointsProject.Models;
using CreateEndpointsProject.Services;

// =============================================================================
// CREATE ENDPOINTS — Interview practice scaffold
// =============================================================================
//
// Run:  dotnet run --project src/CreateEndpoints/CreateEndpointsProject
// Test: use CreateEndpointsProject.http in your IDE, or curl/Postman
//
// REST cheat sheet (memorize for interviews):
//   GET    /resources      → list        → 200 OK
//   GET    /resources/{id} → get one     → 200 OK or 404 Not Found
//   POST   /resources      → create      → 201 Created (+ Location header)
//   PUT    /resources/{id} → replace     → 200 OK or 404
//   DELETE /resources/{id} → remove      → 204 No Content or 404
//
// Minimal API mapping methods:
//   app.MapGet(...)    app.MapPost(...)    app.MapPut(...)    app.MapDelete(...)
//
// Common result helpers:
//   Results.Ok(data)           Results.NotFound()
//   Results.Created(uri, data) Results.BadRequest(problem)
//   Results.NoContent()        TypedResults.NotFound()  (better OpenAPI inference)

var builder = WebApplication.CreateBuilder(args);

// Register the in-memory repository so endpoint handlers can receive IBookRepository via DI.
// Lifetime cheat sheet:
//   Singleton  — one instance for the whole app (good for in-memory stores)
//   Scoped     — one per HTTP request (typical for EF Core DbContext)
//   Transient  — new instance every time it is injected
builder.Services.AddSingleton<IBookRepository, BookRepository>();

// Uncomment when working on Exercise 7 (controller-based endpoints):
// builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();

// -----------------------------------------------------------------------------
// REFERENCE — working minimal endpoint (study this before the exercises)
// -----------------------------------------------------------------------------
// MapGet registers a delegate for HTTP GET. The route template is the first argument.
// Handler parameters are resolved automatically:
//   - IBookRepository comes from DI
//   - int id would come from route: MapGet("/books/{id}", ...)
//   - [FromQuery] string? author — use explicit attribute or name match for query strings
app.MapGet("/books/health", () => Results.Ok(new { status = "ready", topic = "create-endpoints" }));

// =============================================================================
// EXERCISE 1 — GET all resources
// =============================================================================
// Goal: GET /books returns every book as JSON with status 200.
//
// Steps:
//   1. Call app.MapGet with route "/books"
//   2. Inject IBookRepository as a handler parameter
//   3. Return Results.Ok(repository.GetAll())
//
// Test: GET https://localhost:7xxx/books
//
// Interview tip: returning a raw List or array is fine; Results.Ok makes the status explicit.

// TODO: Implement Exercise 1 here
// app.MapGet(...);


// =============================================================================
// EXERCISE 2 — GET by id (route parameters)
// =============================================================================
// Goal: GET /books/{id} returns one book or 404 if missing.
//
// Steps:
//   1. Route template must include {id} — e.g. "/books/{id}"
//   2. Add an int id parameter to the handler (name must match the route token)
//   3. Use repository.GetById(id)
//   4. If null → Results.NotFound(); else → Results.Ok(book)
//
// Optional: constrain the route — "/books/{id:int}" rejects non-numeric ids early.
//
// Test: GET /books/1 (200) and GET /books/999 (404)

// TODO: Implement Exercise 2 here
// app.MapGet(...);


// =============================================================================
// EXERCISE 3 — POST create (request body + 201 Created)
// =============================================================================
// Goal: POST /books with JSON body creates a book and returns 201 with a Location header.
//
// Request body example:
//   { "title": "Refactoring", "author": "Martin Fowler", "publishedYear": 1999 }
//
// Steps:
//   1. app.MapPost("/books", ...)
//   2. Accept CreateBookRequest as a parameter — ASP.NET Core binds JSON from the body
//   3. Validate: if string.IsNullOrWhiteSpace(request.Title) → Results.BadRequest("...")
//   4. var book = repository.Add(request)
//   5. Return Results.Created($"/books/{book.Id}", book)
//      — first arg is the URI of the new resource (Location header)
//
// Interview tip: 201 Created is the correct status for successful POST that creates a resource.
// Mention idempotency: POST is NOT idempotent (calling twice creates two resources).

// TODO: Implement Exercise 3 here
// app.MapPost(...);


// =============================================================================
// EXERCISE 4 — PUT update
// =============================================================================
// Goal: PUT /books/{id} updates an existing book; 404 if id does not exist.
//
// Steps:
//   1. app.MapPut("/books/{id}", ...)
//   2. Parameters: int id, UpdateBookRequest request, IBookRepository repository
//   3. var updated = repository.Update(id, request)
//   4. updated is null → NotFound(); else → Ok(updated)
//
// Interview tip: PUT vs PATCH — PUT often replaces the whole entity; PATCH applies partial changes.
// This scaffold uses partial fields in UpdateBookRequest (PATCH-style) for flexibility.

// TODO: Implement Exercise 4 here
// app.MapPut(...);


// =============================================================================
// EXERCISE 5 — DELETE
// =============================================================================
// Goal: DELETE /books/{id} removes the book.
//
// Steps:
//   1. app.MapDelete("/books/{id}", ...)
//   2. If repository.Delete(id) returns false → NotFound()
//   3. Else → Results.NoContent() (HTTP 204 — success with no body)
//
// Interview tip: 204 No Content is standard for successful DELETE. Some APIs return 200 with a message instead.

// TODO: Implement Exercise 5 here
// app.MapDelete(...);


// =============================================================================
// EXERCISE 6 — Query string filtering
// =============================================================================
// Goal: GET /books/search?author=Martin&year=2008 returns filtered results.
//
// Steps:
//   1. app.MapGet("/books/search", ...)
//   2. Add optional parameters: string? author, int? year
//      — unmatched query keys are ignored; missing params are null
//   3. Return Results.Ok(repository.Search(author, year))
//
// Alternative syntax for clarity:
//   (string? author, int? year, IBookRepository repo) => ...
//
// Test: /books/search?author=Martin  and  /books/search?year=1999
//
// Interview tip: query strings are for filtering/sorting/paging — not for identifying a single resource (use route params).

// TODO: Implement Exercise 6 here
// app.MapGet(...);


// -----------------------------------------------------------------------------
// EXERCISE 7 — see Controllers/BooksController.cs
// -----------------------------------------------------------------------------
// Uncomment when implementing controller-based endpoints:
// app.MapControllers();

app.Run();
