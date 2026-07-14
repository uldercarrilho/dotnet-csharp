namespace CreateEndpoints.Models;

// Request DTO (Data Transfer Object) — what the client sends when creating a resource.
// Interview tip: use a dedicated type for input so you can validate it without polluting the domain model.
// For POST bodies, ASP.NET Core binds JSON to this type automatically when it is a route handler parameter.
public record CreateBookRequest(string Title, string Author, int PublishedYear);
