namespace Resilience.Models;

// Payload returned by the simulated upstream service.
public record QuoteMetadata(int QuoteId, string VerificationStatus, int Rating);
