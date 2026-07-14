using Microsoft.Extensions.Options;

namespace Resilience.Services;

// Tracks upstream call attempts so the loopback /upstream routes can simulate transient failures.
public class UpstreamSimulatorState
{
    private int _callCount;
    private readonly ResilienceOptions _options;

    public UpstreamSimulatorState(IOptions<ResilienceOptions> options) =>
        _options = options.Value;

    public bool ShouldFail()
    {
        var attempt = Interlocked.Increment(ref _callCount);
        var modulus = Math.Max(_options.FailEveryNthCall, 2);
        return attempt % modulus != 0;
    }

    public void Reset() => Interlocked.Exchange(ref _callCount, 0);
}
