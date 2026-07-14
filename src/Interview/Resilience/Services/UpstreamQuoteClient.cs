using System.Net;
using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using Resilience.Models;

namespace Resilience.Services;

// Calls the loopback /upstream routes in the same app — no external API required.
public class UpstreamQuoteClient : IUpstreamQuoteClient
{
    private readonly HttpClient _httpClient;

    public UpstreamQuoteClient(HttpClient httpClient, IOptions<ResilienceOptions> options)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(options.Value.SelfBaseUrl.TrimEnd('/') + "/");
        _httpClient.Timeout = TimeSpan.FromSeconds(5);
    }

    public async Task<QuoteMetadata?> GetMetadataAsync(int quoteId, CancellationToken cancellationToken = default)
    {
        using var response = await _httpClient.GetAsync(
            $"upstream/quotes/{quoteId}/metadata",
            cancellationToken);

        if (response.StatusCode == HttpStatusCode.NotFound)
            return null;

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<QuoteMetadata>(cancellationToken);
    }
}
