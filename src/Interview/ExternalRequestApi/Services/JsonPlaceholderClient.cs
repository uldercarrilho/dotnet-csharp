using System.Net;
using System.Net.Http.Json;
using ExternalRequestApi.Models;
using Microsoft.Extensions.Options;

namespace ExternalRequestApi.Services;

// Pre-built typed client — calls https://jsonplaceholder.typicode.com (free fake REST API).
// Focus your practice on the minimal API endpoints in Program.cs, not this class.
public class JsonPlaceholderClient : IJsonPlaceholderClient
{
    private readonly HttpClient _httpClient;

    public JsonPlaceholderClient(HttpClient httpClient, IOptions<JsonPlaceholderOptions> options)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(options.Value.BaseUrl.TrimEnd('/') + "/");
    }

    public async Task<IReadOnlyList<ExternalPost>> GetPostsAsync(CancellationToken cancellationToken = default)
    {
        var posts = await _httpClient.GetFromJsonAsync<List<ExternalPost>>("posts", cancellationToken);
        return posts ?? [];
    }

    public async Task<ExternalPost?> GetPostByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        using var response = await _httpClient.GetAsync($"posts/{id}", cancellationToken);

        if (response.StatusCode == HttpStatusCode.NotFound)
            return null;

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ExternalPost>(cancellationToken);
    }

    public async Task<ExternalPost> CreatePostAsync(
        CreateExternalPostRequest request,
        CancellationToken cancellationToken = default)
    {
        using var response = await _httpClient.PostAsJsonAsync("posts", request, cancellationToken);
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<ExternalPost>(cancellationToken))!;
    }

    public async Task<IReadOnlyList<ExternalPost>> GetPostsByUserIdAsync(
        int userId,
        CancellationToken cancellationToken = default)
    {
        var posts = await _httpClient.GetFromJsonAsync<List<ExternalPost>>(
            $"posts?userId={userId}",
            cancellationToken);
        return posts ?? [];
    }

    public async Task<int> GetCommentCountForPostAsync(int postId, CancellationToken cancellationToken = default)
    {
        var comments = await _httpClient.GetFromJsonAsync<List<ExternalComment>>(
            $"posts/{postId}/comments",
            cancellationToken);
        return comments?.Count ?? 0;
    }

    private sealed record ExternalComment(int Id);
}
