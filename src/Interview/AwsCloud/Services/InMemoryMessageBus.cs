namespace AwsCloud.Services;

/// <summary>
/// In-memory SNS → SQS fan-out and DLQ simulation for local practice.
/// </summary>
public sealed class InMemoryMessageBus
{
    private readonly Dictionary<string, List<string>> _topics = new();
    private readonly Dictionary<string, Queue<string>> _queues = new();
    private readonly Dictionary<string, string> _queueDlqMap = new();

    public void CreateTopic(string topicName) => _topics[topicName] = [];

    public void CreateQueue(string queueName, string? deadLetterQueueName = null)
    {
        _queues[queueName] = new Queue<string>();
        if (deadLetterQueueName is not null)
            _queueDlqMap[queueName] = deadLetterQueueName;
    }

    public void SubscribeQueueToTopic(string topicName, string queueName)
    {
        if (!_topics.ContainsKey(topicName))
            throw new InvalidOperationException($"Topic '{topicName}' does not exist.");
        if (!_queues.ContainsKey(queueName))
            throw new InvalidOperationException($"Queue '{queueName}' does not exist.");

        _topics[topicName].Add(queueName);
    }

    public int Publish(string topicName, string message)
    {
        if (!_topics.TryGetValue(topicName, out var subscribers))
            throw new InvalidOperationException($"Topic '{topicName}' does not exist.");

        foreach (var queueName in subscribers)
            _queues[queueName].Enqueue(message);

        return subscribers.Count;
    }

    public void Enqueue(string queueName, string message)
    {
        if (!_queues.ContainsKey(queueName))
            throw new InvalidOperationException($"Queue '{queueName}' does not exist.");

        _queues[queueName].Enqueue(message);
    }

    public bool TryDequeue(string queueName, out string? message)
    {
        message = null;
        if (!_queues.TryGetValue(queueName, out var queue) || queue.Count == 0)
            return false;

        message = queue.Dequeue();
        return true;
    }

    /// <summary>
    /// Simulates maxReceiveCount exceeded — message moves to configured DLQ.
    /// </summary>
    public void MoveToDlq(string queueName, string message)
    {
        if (!_queueDlqMap.TryGetValue(queueName, out var dlqName))
            throw new InvalidOperationException($"Queue '{queueName}' has no DLQ configured.");

        _queues[dlqName].Enqueue(message);
    }

    public int QueueDepth(string queueName) => _queues.TryGetValue(queueName, out var q) ? q.Count : 0;
}
