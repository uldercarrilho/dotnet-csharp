namespace ExternalRequestApi.Models;

// DTO matching JSONPlaceholder /posts — maps directly from external JSON.
// Interview tip: keep external API shapes separate from your domain models when they differ.
public record ExternalPost(int Id, int UserId, string Title, string Body);
