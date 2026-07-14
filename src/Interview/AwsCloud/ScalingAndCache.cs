using AwsCloud.Services;

namespace AwsCloud;

/// <summary>
/// API Gateway scale, throttling, and caching (ElastiCache / CloudFront patterns).
/// </summary>
public static class ScalingAndCache
{
    public static async Task RunReferenceDemoAsync()
    {
        Console.WriteLine("--- REFERENCE: API throttling + cache-aside ---");

        var limiter = new TokenBucket(ratePerSecond: 5, burst: 10);
        var allowed = Enumerable.Range(0, 12).Count(_ => limiter.TryAcquire());
        Console.WriteLine($"  Token bucket (5/s, burst 10): {allowed}/12 requests allowed.");

        var cache = new CacheAsideStore<string, string>(TimeSpan.FromSeconds(30));
        async Task<string> LoadFromDb(string id) => await Task.FromResult($"Product-{id}");

        _ = await cache.GetOrLoadAsync("42", LoadFromDb);
        _ = await cache.GetOrLoadAsync("42", LoadFromDb);
        Console.WriteLine($"  Cache-aside: source reads = {cache.SourceReadCount} (expect 1).");

        Console.WriteLine();
    }

    // =============================================================================
    // EXERCISE 8 — API scale and throttling
    // =============================================================================
    // Goal: Protect a hot endpoint with client-side awareness of 429 Too Many Requests.
    //
    // Steps:
    //   1. Extend TokenBucket or add sliding-window limiter per API key.
    //   2. Simulate 50 requests; return 200 for allowed, 429 when throttled.
    //   3. Print p99 latency story: why throttling beats unbounded queue depth.
    //
    // Interview tip: API Gateway usage plans + WAF; ALB for L7 routing; ECS/EKS for
    //   long-lived connections. Lambda + API Gateway HTTP API for spiky traffic.

    public static void Exercise8_ApiScale()
    {
        Console.WriteLine("--- EXERCISE 8: API scale (TODO) ---");
        // TODO: Implement Exercise 8 here
        throw new NotImplementedException("Exercise 8 — API throttling and 429 handling.");
    }

    // =============================================================================
    // EXERCISE 9 — Cache invalidation
    // =============================================================================
    // Goal: On product update, invalidate cache and optionally warm critical keys.
    //
    // Steps:
    //   1. Load product into CacheAsideStore.
    //   2. Simulate update event; call Invalidate.
    //   3. Next read should hit source again (SourceReadCount increments).
    //
    // Interview tip: Cache-aside = app manages cache. Write-through / write-behind
    //   trade consistency vs complexity. Use TTL + explicit invalidation for catalog data.

    public static async Task Exercise9_CacheInvalidationAsync()
    {
        Console.WriteLine("--- EXERCISE 9: Cache (TODO) ---");
        await Task.CompletedTask;
        // TODO: Implement Exercise 9 here
        throw new NotImplementedException("Exercise 9 — cache invalidation on update.");
    }

    /// <summary>Simple token bucket for local API throttle simulation.</summary>
    public sealed class TokenBucket
    {
        private readonly double _ratePerSecond;
        private readonly int _burst;
        private double _tokens;
        private DateTimeOffset _lastRefill = DateTimeOffset.UtcNow;

        public TokenBucket(double ratePerSecond, int burst)
        {
            _ratePerSecond = ratePerSecond;
            _burst = burst;
            _tokens = burst;
        }

        public bool TryAcquire()
        {
            Refill();
            if (_tokens < 1)
                return false;

            _tokens -= 1;
            return true;
        }

        private void Refill()
        {
            var now = DateTimeOffset.UtcNow;
            var elapsed = (now - _lastRefill).TotalSeconds;
            _tokens = Math.Min(_burst, _tokens + elapsed * _ratePerSecond);
            _lastRefill = now;
        }
    }
}
