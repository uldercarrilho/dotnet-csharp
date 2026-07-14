using AwsCloud.Services;

namespace AwsCloud;

/// <summary>
/// DynamoDB — partition/sort keys, access patterns, conditional writes.
/// </summary>
public static class DynamoDbConcepts
{
    public static void RunReferenceDemo()
    {
        Console.WriteLine("--- REFERENCE: DynamoDB access patterns ---");

        var table = new InMemoryDynamoTable();

        // Single-table design: PK = entity type#id, SK = metadata or relation
        table.Put("ORDER#1001", "METADATA", new Dictionary<string, string>
        {
            ["Status"] = "PLACED",
            ["CustomerId"] = "C-42",
        });

        table.Put("ORDER#1001", "LINE#1", new Dictionary<string, string>
        {
            ["Sku"] = "WIDGET",
            ["Qty"] = "2",
        });

        var orderItems = table.QueryByPartitionKey("ORDER#1001");
        Console.WriteLine($"  Query PK=ORDER#1001 returned {orderItems.Count} item(s).");

        // Conditional write — idempotent create (e.g. webhook deduplication)
        try
        {
            table.Put("IDEMPOTENCY#evt-9", "RECORD", new Dictionary<string, string>
            {
                ["ProcessedAt"] = DateTime.UtcNow.ToString("O"),
            }, onlyIfNotExists: true);

            table.Put("IDEMPOTENCY#evt-9", "RECORD", new Dictionary<string, string>
            {
                ["ProcessedAt"] = DateTime.UtcNow.ToString("O"),
            }, onlyIfNotExists: true);
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"  Conditional write blocked duplicate: {ex.Message}");
        }

        Console.WriteLine();
    }

    // =============================================================================
    // EXERCISE 1 — Design a DynamoDB key schema
    // =============================================================================
    // Goal: Model a "User has many Orders" access pattern with GetUser and ListOrdersByUser.
    //
    // Steps:
    //   1. Choose PK/SK for USER profile and ORDER items in a single table.
    //   2. Insert one user and two orders via InMemoryDynamoTable.Put.
    //   3. Implement QueryByPartitionKey to list all orders for that user.
    //   4. Print order IDs or statuses.
    //
    // Interview tip: Design keys around query patterns, not relational normalization.
    //   Hot partitions happen when PK cardinality is too low (e.g. all rows share PK=STATUS).

    public static void Exercise1_DesignKeySchema()
    {
        Console.WriteLine("--- EXERCISE 1: DynamoDB key schema (TODO) ---");
        // TODO: Implement Exercise 1 here
        throw new NotImplementedException("Exercise 1 — design PK/SK and query orders by user.");
    }
}
