namespace AwsCloud.Services;

/// <summary>
/// Minimal in-memory stand-in for DynamoDB access patterns (no AWS SDK required).
/// Interview focus: partition key choice, conditional writes, idempotent upserts.
/// </summary>
public sealed class InMemoryDynamoTable
{
    private readonly Dictionary<string, Dictionary<string, string>> _items = new();

    public bool TryGet(string partitionKey, string sortKey, out Dictionary<string, string>? item)
    {
        var key = ComposeKey(partitionKey, sortKey);
        if (_items.TryGetValue(key, out var stored))
        {
            item = new Dictionary<string, string>(stored);
            return true;
        }

        item = null;
        return false;
    }

    /// <summary>
    /// PutItem with optional condition — mirrors DynamoDB ConditionalCheckFailedException semantics.
    /// </summary>
    public void Put(
        string partitionKey,
        string sortKey,
        Dictionary<string, string> attributes,
        bool onlyIfNotExists = false)
    {
        var key = ComposeKey(partitionKey, sortKey);
        if (onlyIfNotExists && _items.ContainsKey(key))
            throw new InvalidOperationException("ConditionalCheckFailed: item already exists.");

        _items[key] = new Dictionary<string, string>(attributes)
        {
            ["PK"] = partitionKey,
            ["SK"] = sortKey,
        };
    }

    public IReadOnlyList<Dictionary<string, string>> QueryByPartitionKey(string partitionKey) =>
        _items.Values
            .Where(i => i["PK"] == partitionKey)
            .Select(i => new Dictionary<string, string>(i))
            .ToList();

    private static string ComposeKey(string partitionKey, string sortKey) => $"{partitionKey}#{sortKey}";
}
