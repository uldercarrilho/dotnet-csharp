using System.Text.Json;

namespace AwsCloud;

/// <summary>
/// Amazon EventBridge — event bus, rules, content-based filtering.
/// </summary>
public static class EventBridgeRouting
{
    public static void RunReferenceDemo()
    {
        Console.WriteLine("--- REFERENCE: EventBridge rule routing ---");

        var bus = new InMemoryEventBus();
        bus.PutRule("order-events", """{"source":["com.shop.orders"],"detail-type":["OrderPlaced"]}""");
        bus.PutRule("audit-all", """{"source":["prefix:com.shop"]}""");

        bus.PutTarget("order-events", "FulfillmentLambda");
        bus.PutTarget("audit-all", "AuditQueue");

        var evt = """{"source":"com.shop.orders","detail-type":"OrderPlaced","detail":{"orderId":"99"}}""";
        var targets = bus.Route(evt);
        Console.WriteLine($"  Event routed to: {string.Join(", ", targets)}");

        Console.WriteLine();
    }

    // =============================================================================
    // EXERCISE 10 — EventBridge content filtering
    // =============================================================================
    // Goal: Route high-value orders to a priority queue, others to standard processing.
    //
    // Steps:
    //   1. Add rules matching detail.total > 1000 (simulate with JsonDocument parse).
    //   2. Publish two events; assert correct targets per rule.
    //
    // Interview tip: EventBridge vs SNS — EventBridge adds schema registry, archives,
    //   cross-account buses, and content filtering. SNS is simpler pub/sub.

    public static void Exercise10_EventBridgeRules()
    {
        Console.WriteLine("--- EXERCISE 10: EventBridge (TODO) ---");
        // TODO: Implement Exercise 10 here
        throw new NotImplementedException("Exercise 10 — content-based EventBridge routing.");
    }

    public sealed class InMemoryEventBus
    {
        private readonly Dictionary<string, string> _rules = new();
        private readonly Dictionary<string, List<string>> _targets = new();

        public void PutRule(string name, string eventPatternJson) => _rules[name] = eventPatternJson;

        public void PutTarget(string ruleName, string target) =>
            (_targets.TryGetValue(ruleName, out var list) ? list : _targets[ruleName] = []).Add(target);

        public IReadOnlyList<string> Route(string eventJson)
        {
            using var doc = JsonDocument.Parse(eventJson);
            var root = doc.RootElement;
            var source = root.GetProperty("source").GetString() ?? "";
            var detailType = root.TryGetProperty("detail-type", out var dt) ? dt.GetString() : "";

            var matched = new List<string>();
            foreach (var (ruleName, pattern) in _rules)
            {
                if (MatchesPattern(pattern, source, detailType))
                {
                    if (_targets.TryGetValue(ruleName, out var t))
                        matched.AddRange(t);
                }
            }

            return matched;
        }

        private static bool MatchesPattern(string patternJson, string source, string? detailType)
        {
            using var pattern = JsonDocument.Parse(patternJson);
            if (pattern.RootElement.TryGetProperty("source", out var sources))
            {
                foreach (var s in sources.EnumerateArray())
                {
                    var val = s.GetString() ?? "";
                    if (val.StartsWith("prefix:", StringComparison.Ordinal))
                    {
                        if (source.StartsWith(val["prefix:".Length..], StringComparison.Ordinal))
                            return true;
                    }
                    else if (val == source)
                        return true;
                }
            }

            if (pattern.RootElement.TryGetProperty("detail-type", out var types) && detailType is not null)
            {
                foreach (var t in types.EnumerateArray())
                {
                    if (t.GetString() == detailType)
                        return true;
                }
            }

            return false;
        }
    }
}
