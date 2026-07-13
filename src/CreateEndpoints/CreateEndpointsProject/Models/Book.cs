namespace CreateEndpointsProject.Models;

// Domain model — the shape of data your API works with internally.
// Interview tip: keep domain models separate from request/response DTOs when validation or
// serialization rules differ (e.g. you never expose internal IDs the same way on create).
public record Book(int Id, string Title, string Author, int PublishedYear);
