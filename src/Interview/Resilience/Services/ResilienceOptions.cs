namespace Resilience.Services;

public class ResilienceOptions
{
    public const string SectionName = "Resilience";

    // Loopback base URL — must match the http profile port in launchSettings.json.
    public string SelfBaseUrl { get; set; } = "http://localhost:5299";

    // Upstream fails when (callCount % FailEveryNthCall) != 0 — good for retry practice.
    public int FailEveryNthCall { get; set; } = 3;
}
