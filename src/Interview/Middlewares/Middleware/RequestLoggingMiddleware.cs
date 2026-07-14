namespace Middlewares.Middleware;

// EXERCISE 2 — class-based request/response logging middleware.
//
// Steps:
//   1. Before await _next(context): log "{Method} {Path}" (Console.WriteLine is fine for practice).
//   2. After await _next(context): log the response status code.
//   3. Register in Program.cs with app.UseMiddleware<RequestLoggingMiddleware>().
//
// Interview tip: ILogger<RequestLoggingMiddleware> can be added to the constructor for real apps.
public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public RequestLoggingMiddleware(RequestDelegate next) => _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        // TODO: log the incoming request (method + path), call _next, then log status code.
        await _next(context);
    }
}
