namespace AwsCloud;

/// <summary>
/// ECS container orchestration and microservice boundaries on AWS.
/// </summary>
public static class EcsMicroservices
{
    public static void RunReferenceDemo()
    {
        Console.WriteLine("--- REFERENCE: ECS vs Lambda + microservice boundaries ---");

        var catalog = new ServiceDescriptor("catalog-api", Hosting.EcsFargate, Scaling.Horizontal);
        var orders = new ServiceDescriptor("orders-api", Hosting.EcsFargate, Scaling.Horizontal);
        var notifier = new ServiceDescriptor("notifier", Hosting.Lambda, Scaling.EventDriven);

        PrintTradeoff(catalog);
        PrintTradeoff(notifier);

        Console.WriteLine("  Microservices: bounded context per service, async integration (SQS/EventBridge).");
        Console.WriteLine("  Avoid distributed monolith — separate deploy units AND data stores when possible.");
        Console.WriteLine();
    }

    public enum Hosting { Lambda, EcsFargate, Eks }
    public enum Scaling { EventDriven, Horizontal, Manual }

    public sealed record ServiceDescriptor(string Name, Hosting Hosting, Scaling Scaling);

    private static void PrintTradeoff(ServiceDescriptor svc)
    {
        var note = svc.Hosting switch
        {
            Hosting.Lambda => "pay per invoke, cold start, 15 min max, great for spiky/async",
            Hosting.EcsFargate => "long-running HTTP/gRPC, WebSockets, predictable latency, ops overhead",
            Hosting.Eks => "Kubernetes portability, highest ops complexity",
            _ => "",
        };
        Console.WriteLine($"  {svc.Name} ({svc.Hosting}): {note}");
    }

    // =============================================================================
    // EXERCISE 11 — ECS service scaling
    // =============================================================================
    // Goal: Model target-tracking on CPU vs SQS backlog for an ECS service.
    //
    // Steps:
    //   1. Given desiredCount=2, cpu=70% → scale out +1; cpu=30% → scale in -1 (min 1).
    //   2. Alternate: queue depth > 100 per task → scale out.
    //   3. Print scaling decisions for a sample metric timeline.
    //
    // Interview tip: ECS Service Auto Scaling uses Application Auto Scaling; pair with
    //   ALB health checks. Lambda is poor fit for steady high RPS with low latency SLA.

    public static void Exercise11_EcsScaling()
    {
        Console.WriteLine("--- EXERCISE 11: ECS scaling (TODO) ---");
        // TODO: Implement Exercise 11 here
        throw new NotImplementedException("Exercise 11 — ECS target-tracking scaling.");
    }

    // =============================================================================
    // EXERCISE 12 — Microservices integration pattern
    // =============================================================================
    // Goal: Sketch saga/choreography for PlaceOrder across Catalog, Orders, Payment.
    //
    // Steps:
    //   1. Define three ServiceDescriptor records and their owned data stores.
    //   2. List events: OrderPlaced → ReserveInventory → PaymentCaptured → OrderConfirmed.
    //   3. Identify compensating actions if Payment fails after inventory reserved.
    //
    // Interview tip: Prefer choreography (events) for loose coupling; orchestration (Step
    //   Functions) when you need visible workflow state and timeouts in one place.

    public static void Exercise12_MicroservicesSaga()
    {
        Console.WriteLine("--- EXERCISE 12: Microservices (TODO) ---");
        // TODO: Implement Exercise 12 here
        throw new NotImplementedException("Exercise 12 — event-driven saga across services.");
    }
}
