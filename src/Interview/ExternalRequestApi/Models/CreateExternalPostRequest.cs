namespace ExternalRequestApi.Models;

public record CreateExternalPostRequest(int UserId, string Title, string Body);
