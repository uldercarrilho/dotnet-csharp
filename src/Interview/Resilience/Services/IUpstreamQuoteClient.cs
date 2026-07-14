using Resilience.Models;

namespace Resilience.Services;

// Typed HTTP client for the simulated upstream — configure resilience on this registration in Program.cs.
public interface IUpstreamQuoteClient
{
    Task<QuoteMetadata?> GetMetadataAsync(int quoteId, CancellationToken cancellationToken = default);
}
