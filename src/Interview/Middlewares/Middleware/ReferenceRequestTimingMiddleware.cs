using System.Diagnostics;

namespace Middlewares.Middleware;

// REFERENCE — class-based middleware that measures how long the rest of the pipeline takes.
//
// Pattern:
//   1. Constructor receives RequestDelegate next (the rest of the pipeline).
//   2. InvokeAsync runs your logic, then await _next(context), then optional post-processing.
//   3. Register with app.UseMiddleware<ReferenceRequestTimingMiddleware>() or an extension method.
//
// Interview tip: middleware classes are activated per request via DI when you add extra constructor
// parameters (e.g. ILogger<T>). RequestDelegate is always injected automatically.
public class ReferenceRequestTimingMiddleware
{
    private readonly RequestDelegate _next;

    public ReferenceRequestTimingMiddleware(RequestDelegate next) => _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();

        // Register before await _next — after the endpoint runs, the response may have already
        // started and new headers cannot be added. OnStarting fires just before headers are sent.
        context.Response.OnStarting(() =>
        {
            stopwatch.Stop();
            context.Response.Headers["X-Response-Time-Ms"] = stopwatch.ElapsedMilliseconds.ToString();
            return Task.CompletedTask;
        });

        await _next(context);
    }
}
