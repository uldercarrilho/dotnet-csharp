namespace Resilience.Models;

// Local quote merged with upstream metadata — returned by the enriched endpoint.
public record EnrichedQuote(int Id, string Text, string Author, string VerificationStatus, int Rating);
