using Microsoft.Extensions.Options;
using Resilience.Models;
using Resilience.Services;

// =============================================================================
// RESILIENCE — Interview practice scaffold
// =============================================================================
//
// Run:  dotnet run --project src/Interview/Resilience
// Test: use Resilience.http in your IDE, or curl/Postman
//
// Topics covered:
//   REST API          — minimal API routes, status codes, JSON binding
//   Retry policy      — re-attempt transient upstream failures (503, timeouts)
//   Circuit breaker   — stop calling a failing dependency; fail fast while open
//   Throttling        — rate-limit incoming client requests (protect your API)
//
// Resilience cheat sheet (memorize for interviews):
//   Retry           — use for transient errors (503, 408, network blips). Not for 400/404.
//   Circuit breaker — after N consecutive failures, open circuit → skip calls for a cooldown.
//   Throttling      — cap requests per time window (protects YOUR API from overload).
//   Timeout         — cancel work that takes too long (pair with retry carefully).
//
// .NET 8 packages used here:
//   Microsoft.Extensions.Http.Resilience — AddStandardResilienceHandler(), AddResilienceHandler()
//   Built-in rate limiting               — AddRateLimiter() + RequireRateLimiting()
//
// REST cheat sheet:
//   GET    /resources      → 200 OK
//   GET    /resources/{id} → 200 or 404
//   POST   /resources      → 201 Created
//
// HttpClient + resilience registration pattern:
//   builder.Services.AddHttpClient<IUpstreamQuoteClient, UpstreamQuoteClient>()
//       .AddStandardResilienceHandler();   // retry + circuit breaker + timeout (opinionated defaults)
//
//   // Or customize:
//   .AddResilienceHandler("upstream", pipeline => {
//       pipeline.AddRetry(new HttpRetryStrategyOptions { MaxRetryAttempts = 3 });
//       pipeline.AddCircuitBreaker(new HttpCircuitBreakerStrategyOptions { ... });
//   });

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ResilienceOptions>(
    builder.Configuration.GetSection(ResilienceOptions.SectionName));

builder.Services.AddSingleton<IQuoteRepository, QuoteRepository>();
builder.Services.AddSingleton<UpstreamSimulatorState>();

// Typed HttpClient — no resilience handler yet (you add it in Exercises 4–6).
// Interview tip: always use IHttpClientFactory (AddHttpClient) instead of `new HttpClient()`.
builder.Services.AddHttpClient<IUpstreamQuoteClient, UpstreamQuoteClient>();

// TODO (Exercise 6): register rate limiting here with builder.Services.AddRateLimiter(...)

var app = builder.Build();

app.UseHttpsRedirection();

// TODO (Exercise 6): enable rate limiting middleware — app.UseRateLimiter();

// -----------------------------------------------------------------------------
// PRE-BUILT — simulated flaky upstream (loopback; do not modify)
// -----------------------------------------------------------------------------
// UpstreamQuoteClient calls these routes via HttpClient. FailEveryNthCall in appsettings
// controls how often 503 is returned — ideal for retry and circuit-breaker exercises.
app.MapGet("/upstream/quotes/{id:int}/metadata", (int id, UpstreamSimulatorState state) =>
{
    if (state.ShouldFail())
    {
        return Results.Problem(
            detail: "Simulated transient upstream failure.",
            statusCode: StatusCodes.Status503ServiceUnavailable);
    }

    return Results.Ok(new QuoteMetadata(id, "verified", Random.Shared.Next(1, 6)));
});

app.MapPost("/upstream/reset", (UpstreamSimulatorState state) =>
{
    state.Reset();
    return Results.Ok(new { message = "Upstream failure counter reset." });
});

// -----------------------------------------------------------------------------
// REFERENCE — health check (works out of the box)
// -----------------------------------------------------------------------------
app.MapGet("/resilience/health", (IOptions<ResilienceOptions> options) =>
    Results.Ok(new
    {
        status = "ready",
        topic = "resilience",
        upstreamBaseUrl = options.Value.SelfBaseUrl,
        failEveryNthCall = options.Value.FailEveryNthCall,
    }));

// =============================================================================
// EXERCISE 1 — REST: GET all quotes
// =============================================================================
// Goal: GET /quotes returns every quote from IQuoteRepository with status 200.
//
// Steps:
//   1. app.MapGet("/quotes", ...)
//   2. Inject IQuoteRepository
//   3. Return Results.Ok(repository.GetAll())
//
// Test: GET http://localhost:5299/quotes
//
// Interview tip: GET is safe and idempotent — multiple calls have no side effects.

// TODO: Implement Exercise 1 here


// =============================================================================
// EXERCISE 2 — REST: GET by id and POST create
// =============================================================================
// Goal:
//   GET  /quotes/{id}  → 200 with quote, or 404 if missing
//   POST /quotes       → 201 Created with Location header
//
// POST body example:
//   { "text": "Stay hungry, stay foolish.", "author": "Steve Jobs" }
//
// Steps for GET:
//   1. Route "/quotes/{id:int}"
//   2. repository.GetById(id) → null ? NotFound() : Ok(quote)
//
// Steps for POST:
//   1. Accept CreateQuoteRequest (JSON body binding)
//   2. Validate Text and Author are not whitespace → BadRequest
//   3. var quote = repository.Add(request)
//   4. Results.Created($"/quotes/{quote.Id}", quote)
//
// Test: GET /quotes/1, GET /quotes/999, POST /quotes
//
// Interview tip: 201 Created + Location header is the REST standard for resource creation.

// TODO: Implement Exercise 2 here


// =============================================================================
// EXERCISE 3 — Enriched quote (upstream call WITHOUT resilience)
// =============================================================================
// Goal: GET /quotes/{id}/enriched merges local quote + upstream metadata.
//       Without retry/circuit breaker, upstream 503 surfaces as 502 Bad Gateway.
//
// Steps:
//   1. Route "/quotes/{id:int}/enriched"
//   2. Inject IQuoteRepository and IUpstreamQuoteClient
//   3. var quote = repository.GetById(id) → if null, NotFound()
//   4. try { var metadata = await upstream.GetMetadataAsync(id, cancellationToken) }
//      catch HttpRequestException → Results.Problem(statusCode: 502, detail: "...")
//   5. Return Results.Ok(new EnrichedQuote(quote.Id, quote.Text, quote.Author,
//        metadata.VerificationStatus, metadata.Rating))
//
// Test: call GET /quotes/1/enriched several times — some requests fail (503 upstream).
//       POST /upstream/reset between test runs to reset the failure counter.
//
// Interview tip: map upstream failures to your API contract. Log the exception;
// return ProblemDetails (502/503) instead of leaking raw exception messages.

// TODO: Implement Exercise 3 here


// =============================================================================
// EXERCISE 4 — Retry policy on HttpClient
// =============================================================================
// Goal: configure automatic retries on the typed HttpClient so Exercise 3 succeeds
//       even when the upstream fails intermittently.
//
// Steps:
//   1. In service registration (above), chain onto AddHttpClient:
//        .AddResilienceHandler("retry-only", pipeline =>
//        {
//            pipeline.AddRetry(new HttpRetryStrategyOptions
//            {
//                MaxRetryAttempts = 3,
//                BackoffType = DelayBackoffType.Exponential,
//                UseJitter = true,
//            });
//        });
//      — add: using Microsoft.Extensions.Http.Resilience;
//
//   2. Re-test GET /quotes/1/enriched — should succeed after transparent retries.
//
// Interview tip: retry only idempotent operations (GET) or requests with idempotency keys.
// Exponential backoff + jitter prevents thundering herd when upstream recovers.
// Do NOT retry 400 Bad Request or 404 Not Found — those are not transient.

// TODO: Implement Exercise 4 here (HttpClient registration in builder section)


// =============================================================================
// EXERCISE 5 — Circuit breaker
// =============================================================================
// Goal: after consecutive upstream failures, open the circuit and fail fast
//       without hammering the unhealthy dependency.
//
// Steps:
//   1. Add circuit breaker to the same resilience pipeline (or replace retry-only):
//        pipeline.AddCircuitBreaker(new HttpCircuitBreakerStrategyOptions
//        {
//            FailureRatio = 0.5,
//            MinimumThroughput = 3,
//            BreakDuration = TimeSpan.FromSeconds(10),
//            SamplingDuration = TimeSpan.FromSeconds(30),
//        });
//
//   2. Hammer GET /quotes/1/enriched until circuit opens → expect fast 502/503.
//   3. Wait BreakDuration, then call again — half-open probe should close circuit.
//
// Alternative one-liner for production defaults:
//   .AddStandardResilienceHandler()  // includes retry + circuit breaker + timeout
//
// Interview tip: circuit breaker states — Closed (normal), Open (fail fast),
// Half-Open (probe). Pair with health checks and observability (log state transitions).

// TODO: Implement Exercise 5 here


// =============================================================================
// EXERCISE 6 — Throttling (rate limiting incoming requests)
// =============================================================================
// Goal: limit clients to 5 requests per 10 seconds on the enriched endpoint.
//
// Steps:
//   1. Register rate limiter in builder section:
//        builder.Services.AddRateLimiter(options =>
//        {
//            options.AddFixedWindowLimiter("enriched", limiter =>
//            {
//                limiter.Window = TimeSpan.FromSeconds(10);
//                limiter.PermitLimit = 5;
//                limiter.QueueLimit = 0;
//            });
//            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
//        });
//
//   2. Add app.UseRateLimiter() before MapGet routes.
//
//   3. On the enriched route, chain: .RequireRateLimiting("enriched")
//
// Test: send 6+ rapid GET /quotes/1/enriched → 429 Too Many Requests.
//
// Interview tip: throttling protects YOUR API. Retry/circuit breaker protect calls
// TO dependencies. Use 429 + Retry-After header; clients should back off.

// TODO: Implement Exercise 6 here


app.Run();
