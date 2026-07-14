namespace AwsCloud.Services;

/// <summary>
/// Cache-aside (lazy loading) pattern — mirrors ElastiCache / DAX usage in .NET apps.
/// </summary>
public sealed class CacheAsideStore<TKey, TValue> where TKey : notnull
{
    private readonly Dictionary<TKey, (TValue Value, DateTimeOffset ExpiresAt)> _cache = new();
    private readonly TimeSpan _defaultTtl;
    private int _sourceReads;

    public CacheAsideStore(TimeSpan? defaultTtl = null) =>
        _defaultTtl = defaultTtl ?? TimeSpan.FromMinutes(5);

    public int SourceReadCount => _sourceReads;

    public async Task<TValue> GetOrLoadAsync(
        TKey key,
        Func<TKey, Task<TValue>> loadFromSource,
        TimeSpan? ttl = null)
    {
        if (_cache.TryGetValue(key, out var entry) && entry.ExpiresAt > DateTimeOffset.UtcNow)
            return entry.Value;

        _sourceReads++;
        var value = await loadFromSource(key);
        _cache[key] = (value, DateTimeOffset.UtcNow.Add(ttl ?? _defaultTtl));
        return value;
    }

    public void Invalidate(TKey key) => _cache.Remove(key);
}
