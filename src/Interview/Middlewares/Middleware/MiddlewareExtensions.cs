namespace Middlewares.Middleware;

// Extension methods keep Program.cs readable — common pattern in production apps.
public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseRequestTiming(this IApplicationBuilder app) =>
        app.UseMiddleware<ReferenceRequestTimingMiddleware>();
}
