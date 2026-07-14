using System.Text.Json;
using AwsCloud.Services;

namespace AwsCloud;

/// <summary>
/// AWS Lambda — handlers, SQS triggers, EventBridge schedules, concurrency.
/// </summary>
public static class ServerlessLambda
{
    public static void RunReferenceDemo()
    {
        Console.WriteLine("--- REFERENCE: Lambda handler + SQS batch ---");

        var records = new[]
        {
            """{"id":"1","action":"ship"}""",
            """{"id":"2","action":"ship"}""",
        };

        var (processed, failures) = ProcessSqsBatch(records, maxConcurrency: 10);
        Console.WriteLine($"  Batch: {processed} succeeded, {failures} failed (partial batch failure).");

        var cronEvent = new ScheduledEvent("rate(5 minutes)", "nightly-report");
        Console.WriteLine($"  Scheduled trigger: {cronEvent.Rule} → {cronEvent.Target}");
        Console.WriteLine($"  Lambda scaling: concurrency rises with queue depth; account limit applies.");
        Console.WriteLine();
    }

    /// <summary>
    /// Simulates Lambda SQS event source mapping with partial batch response.
    /// Interview tip: return batchItemFailures so only failed messages are retried.
    /// </summary>
    public static (int Processed, int Failures) ProcessSqsBatch(
        IEnumerable<string> messages,
        int maxConcurrency)
    {
        _ = maxConcurrency; // reserved for Exercise 5 — compare reserved vs on-demand
        var processed = 0;
        var failures = 0;

        foreach (var body in messages)
        {
            try
            {
                var doc = JsonDocument.Parse(body);
                if (!doc.RootElement.TryGetProperty("action", out _))
                    throw new JsonException("Missing action");

                processed++;
            }
            catch (JsonException)
            {
                failures++;
            }
        }

        return (processed, failures);
    }

    public sealed record ScheduledEvent(string Rule, string Target);

    // =============================================================================
    // EXERCISE 4 — Idempotent Lambda handler
    // =============================================================================
    // Goal: Process the same SQS message twice without duplicating side effects.
    //
    // Steps:
    //   1. Use InMemoryDynamoTable with PK=IDEMPOTENCY#{messageId}.
    //   2. On first process: conditional Put (onlyIfNotExists) then perform work.
    //   3. On duplicate: catch conditional failure and return success (no-op).
    //
    // Interview tip: Combine idempotency keys with SQS visibility timeout; design for
    //   at-least-once delivery.

    public static void Exercise4_IdempotentHandler()
    {
        Console.WriteLine("--- EXERCISE 4: Idempotent Lambda (TODO) ---");
        // TODO: Implement Exercise 4 here
        throw new NotImplementedException("Exercise 4 — idempotent SQS message processing.");
    }

    // =============================================================================
    // EXERCISE 5 — Lambda scaling and concurrency
    // =============================================================================
    // Goal: Model how reserved concurrency caps a function vs account pool exhaustion.
    //
    // Steps:
    //   1. Define accountLimit=100, reservedForApi=20, reservedForWorker=30.
    //   2. Given incoming SQS depth of 500, compute max parallel invocations for worker.
    //   3. Print what happens when reserved sum exceeds account limit (interview trap).
    //
    // Interview tip: Reserved concurrency guarantees capacity AND sets a ceiling.
    //   Provisioned concurrency reduces cold starts; on-demand scales until limits.

    public static void Exercise5_LambdaScaling()
    {
        Console.WriteLine("--- EXERCISE 5: Lambda scaling (TODO) ---");
        // TODO: Implement Exercise 5 here
        throw new NotImplementedException("Exercise 5 — model Lambda concurrency limits.");
    }

    // =============================================================================
    // EXERCISE 6 — Lambda triggered by SQS
    // =============================================================================
    // Goal: Wire InMemoryMessageBus → batch processor mimicking Lambda's event source mapping.
    //
    // Steps:
    //   1. Enqueue 5 messages; dequeue in batches of 10 (Lambda default max).
    //   2. Call ProcessSqsBatch; re-enqueue failures or MoveToDlq after max receives.
    //
    // Interview tip: Batch size, batching window, and maximumConcurrency tune throughput
    //   vs blast radius when a bad message fails the whole batch (pre partial-response).

    public static void Exercise6_LambdaSqsTrigger()
    {
        Console.WriteLine("--- EXERCISE 6: Lambda + SQS trigger (TODO) ---");
        // TODO: Implement Exercise 6 here
        throw new NotImplementedException("Exercise 6 — SQS-triggered Lambda batch processing.");
    }

    // =============================================================================
    // EXERCISE 7 — Lambda triggered by cron (EventBridge schedule)
    // =============================================================================
    // Goal: Simulate EventBridge rate/cron invoking a scheduled Lambda.
    //
    // Steps:
    //   1. Parse rules: rate(5 minutes) vs cron(0 2 * * ? *).
    //   2. Implement RunScheduledJob(ScheduledEvent) that logs job name + UTC timestamp.
    //   3. Document when to use EventBridge Scheduler vs CloudWatch Events legacy.
    //
    // Interview tip: Cron = wall-clock (UTC). Long jobs need timeout > schedule interval
    //   or use Step Functions / SQS to avoid overlapping runs.

    public static void Exercise7_LambdaCronTrigger()
    {
        Console.WriteLine("--- EXERCISE 7: Lambda + cron (TODO) ---");
        // TODO: Implement Exercise 7 here
        throw new NotImplementedException("Exercise 7 — scheduled Lambda via EventBridge.");
    }
}
