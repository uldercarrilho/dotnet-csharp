using AwsCloud.Services;

namespace AwsCloud;

/// <summary>
/// Event-driven architecture — SNS fan-out, SQS consumers, dead-letter queues.
/// </summary>
public static class EventDrivenMessaging
{
    public static void RunReferenceDemo()
    {
        Console.WriteLine("--- REFERENCE: SNS → SQS fan-out + DLQ ---");

        var bus = new InMemoryMessageBus();
        bus.CreateTopic("order-placed");
        bus.CreateQueue("inventory-queue", deadLetterQueueName: "inventory-dlq");
        bus.CreateQueue("email-queue", deadLetterQueueName: "email-dlq");
        bus.CreateQueue("inventory-dlq");
        bus.CreateQueue("email-dlq");

        bus.SubscribeQueueToTopic("order-placed", "inventory-queue");
        bus.SubscribeQueueToTopic("order-placed", "email-queue");

        var fanOut = bus.Publish("order-placed", """{"orderId":"ORD-7","total":49.99}""");
        Console.WriteLine($"  Published to topic; {fanOut} queue(s) received the message.");
        Console.WriteLine($"  inventory-queue depth: {bus.QueueDepth("inventory-queue")}");
        Console.WriteLine($"  email-queue depth: {bus.QueueDepth("email-queue")}");

        // Poison message → DLQ after retries (simulated)
        const string badMessage = "NOT-JSON";
        bus.Enqueue("inventory-queue", badMessage);
        bus.MoveToDlq("inventory-queue", badMessage);
        Console.WriteLine($"  Moved poison message to DLQ; depth: {bus.QueueDepth("inventory-dlq")}");

        Console.WriteLine();
    }

    // =============================================================================
    // EXERCISE 2 — SNS fan-out to multiple SQS subscribers
    // =============================================================================
    // Goal: When "payment-captured" fires, both fulfillment and analytics queues get a copy.
    //
    // Steps:
    //   1. Create topic + two queues; subscribe both.
    //   2. Publish a JSON payload with paymentId.
    //   3. Dequeue from each queue and print the payload.
    //
    // Interview tip: SNS = push fan-out (many subscribers). SQS = durable buffer with
    //   at-least-once delivery; consumers must be idempotent.

    public static void Exercise2_SnsFanOut()
    {
        Console.WriteLine("--- EXERCISE 2: SNS fan-out (TODO) ---");
        // TODO: Implement Exercise 2 here
        throw new NotImplementedException("Exercise 2 — fan-out payment-captured to two queues.");
    }

    // =============================================================================
    // EXERCISE 3 — Dead-letter queue handling
    // =============================================================================
    // Goal: Process messages from a queue; on repeated failure, route to DLQ.
    //
    // Steps:
    //   1. Create main queue + DLQ (wire via InMemoryMessageBus.CreateQueue second arg).
    //   2. Enqueue 3 messages — one invalid JSON.
    //   3. Loop dequeue: try parse JSON; on failure increment receive count; at 3 → MoveToDlq.
    //   4. Print DLQ depth and valid messages processed.
    //
    // Interview tip: DLQ is for inspection/replay, not silent drops. CloudWatch alarm on
    //   ApproximateNumberOfMessagesVisible on the DLQ.

    public static void Exercise3_DeadLetterQueue()
    {
        Console.WriteLine("--- EXERCISE 3: DLQ (TODO) ---");
        // TODO: Implement Exercise 3 here
        throw new NotImplementedException("Exercise 3 — route poison messages to DLQ.");
    }
}
