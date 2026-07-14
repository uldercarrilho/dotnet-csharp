using Middlewares.Middleware;
using Middlewares.Services;

// =============================================================================
// MIDDLEWARES — Interview practice scaffold
// =============================================================================
//
// Run:  dotnet run --project src/Interview/Middlewares
// Test: use Middlewares.http in your IDE, or curl/Postman
//
// What you will learn:
//   - How the ASP.NET Core request pipeline works (middleware chain)
//   - Inline vs class-based middleware
//   - Short-circuiting (returning without calling next)
//   - Pipeline ordering (request vs response direction)
//   - Exception-handling middleware at the app boundary
//
// Pipeline cheat sheet:
//   Request  →  Middleware A  →  Middleware B  →  Endpoint  →  Response
//   Response ←  Middleware A  ←  Middleware B  ←  (handler) ←
//
// Registration order matters: first Use* runs first on the way IN, last on the way OUT.

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<INoteRepository, NoteRepository>();

var app = builder.Build();

app.UseHttpsRedirection();

// -----------------------------------------------------------------------------
// REFERENCE — working timing middleware (study before the exercises)
// -----------------------------------------------------------------------------
// Adds X-Response-Time-Ms to every response. Register BEFORE endpoints.
// Test: GET /notes/health and inspect response headers.
app.UseRequestTiming();

// =============================================================================
// EXERCISE 1 — Inline middleware (logging)
// =============================================================================
// Goal: Log every request method and path to the console before the endpoint runs.
//
// Steps:
//   1. Call app.Use with a lambda: (HttpContext context, RequestDelegate next) => ...
//   2. Log context.Request.Method and context.Request.Path
//   3. await next(context) to pass control to the rest of the pipeline
//
// Test: GET /notes — you should see a console line like "GET /notes"
//
// Interview tip: inline middleware is fine for tiny cross-cutting concerns; extract a class
// when you need DI, unit tests, or multiple middleware in one file.

// TODO: Implement Exercise 1 here


// =============================================================================
// EXERCISE 2 — Class-based middleware
// =============================================================================
// Goal: Implement RequestLoggingMiddleware (see Middleware/RequestLoggingMiddleware.cs).
//
// Steps:
//   1. Complete InvokeAsync — log before and after await _next(context)
//   2. Register below with: app.UseMiddleware<RequestLoggingMiddleware>();
//
// Test: GET /notes — console shows request line and status code (200)
//
// Interview tip: UseMiddleware<T> resolves T from DI per request. Only RequestDelegate is
// required in the constructor unless you inject other services.

// TODO: Register Exercise 2 here — app.UseMiddleware<RequestLoggingMiddleware>();


// =============================================================================
// EXERCISE 3 — Short-circuit (API key gate)
// =============================================================================
// Goal: Reject requests to /notes/* (except /notes/health) when header X-Api-Key is missing.
//
// Steps:
//   1. app.Use(async (context, next) => { ... })
//   2. If path starts with "/notes" but is NOT "/notes/health":
//        - if X-Api-Key header is missing → set StatusCode 401, write body, return (do NOT call next)
//   3. Otherwise await next(context)
//
// Test:
//   GET /notes           → 401 without header, 200 with header X-Api-Key: dev-key
//   GET /notes/health    → 200 without header (health stays public)
//
// Interview tip: authentication middleware often short-circuits; authorization runs after auth.

// TODO: Implement Exercise 3 here


// =============================================================================
// EXERCISE 4 — Exception-handling middleware
// =============================================================================
// Goal: Catch unhandled exceptions and return JSON ProblemDetails with status 500.
//
// Steps:
//   1. Wrap await next(context) in try/catch
//   2. On exception: set StatusCode 500, Content-Type application/problem+json
//   3. Write a small JSON body: { "title": "Internal Server Error", "detail": ex.Message }
//   4. Register this middleware EARLY (near the top), so it wraps later middleware + endpoints
//
// Test: GET /notes/boom → 500 with problem JSON instead of an unhandled exception page
//
// Interview tip: production apps use app.UseExceptionHandler() or IExceptionHandler (.NET 8+).
// Custom middleware teaches the pattern; mention both in interviews.

// TODO: Implement Exercise 4 here (register near the top of the pipeline when done)


// =============================================================================
// EXERCISE 5 — Pipeline ordering
// =============================================================================
// Goal: Prove registration order by appending to a custom response header.
//
// Steps:
//   1. Add two inline middleware blocks that append "A," then "B," to X-Pipeline-Order
//      (initialize the header if missing, then append your letter before calling next)
//   2. Register A before B
//   3. Hit GET /notes/health — response header should be "A,B," (request order)
//   4. Swap registration order and observe the header change
//
// Test: inspect X-Pipeline-Order on /notes/health
//
// Interview tip: Exception handling and HTTPS redirection are middleware too — know where they
// sit relative to your custom components (exception handler should be outermost).

// TODO: Implement Exercise 5 here


// -----------------------------------------------------------------------------
// Endpoints — pre-built so you can test middleware immediately
// -----------------------------------------------------------------------------
app.MapGet("/notes/health", () => Results.Ok(new { status = "ready", topic = "middlewares" }));

app.MapGet("/notes", (INoteRepository repository) => Results.Ok(repository.GetAll()));

app.MapGet("/notes/{id:int}", (int id, INoteRepository repository) =>
{
    var note = repository.GetById(id);
    return note is null ? Results.NotFound() : Results.Ok(note);
});

// Intentionally throws — used by Exercise 4 (exception middleware).
app.MapGet("/notes/boom", () =>
{
    throw new InvalidOperationException("Demo exception for middleware practice.");
});

app.Run();
