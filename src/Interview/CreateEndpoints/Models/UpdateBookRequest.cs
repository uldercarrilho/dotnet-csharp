namespace CreateEndpoints.Models;

// Partial update DTO — all fields optional so clients can send only what changes.
// Interview tip: PUT often replaces the whole resource; PATCH sends partial updates.
// For interviews, know both verbs and when to return 204 No Content vs 200 OK with the updated body.
public record UpdateBookRequest(string? Title, string? Author, int? PublishedYear);
