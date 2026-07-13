using ExternalRequestApiProject.Models;

namespace ExternalRequestApiProject.Services;

// Typed HTTP client abstraction — your endpoints depend on this, not HttpClient directly.
// Interview tip: IHttpClientFactory + typed clients avoid socket exhaustion from new HttpClient()
// per request and centralize BaseAddress, headers, and resilience policies.
public interface IJsonPlaceholderClient
{
    Task<IReadOnlyList<ExternalPost>> GetPostsAsync(CancellationToken cancellationToken = default);
    Task<ExternalPost?> GetPostByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<ExternalPost> CreatePostAsync(CreateExternalPostRequest request, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ExternalPost>> GetPostsByUserIdAsync(int userId, CancellationToken cancellationToken = default);
    Task<int> GetCommentCountForPostAsync(int postId, CancellationToken cancellationToken = default);
}
