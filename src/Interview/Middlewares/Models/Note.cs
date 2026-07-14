namespace Middlewares.Models;

// Simple domain model — endpoints are pre-built so you can focus on the middleware pipeline.
public record Note(int Id, string Title, string Body);
