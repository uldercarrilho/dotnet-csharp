// =============================================================================
// AWS CLOUD — Interview practice scaffold
// =============================================================================
//
// Run:  dotnet run --project src/Interview/AwsCloud
//
// Topics covered (study references first, then implement exercises):
//   DynamoDB              — partition/sort keys, conditional writes, access patterns
//   Event-driven          — SNS fan-out, SQS consumers, at-least-once delivery
//   DLQ                   — poison messages, replay, CloudWatch alarms
//   Serverless / Lambda   — handlers, idempotency, concurrency limits
//   API scale             — throttling, token bucket, 429 vs unbounded load
//   Cache                 — cache-aside, TTL, invalidation (ElastiCache mental model)
//   Lambda scale          — reserved vs provisioned concurrency, account limits
//   Lambda + SQS          — batch processing, partial batch failures
//   Lambda + cron         — EventBridge schedules (rate vs cron)
//   ECS                   — long-running services, auto scaling vs Lambda
//   EventBridge           — content-based routing, cross-service events
//   Microservices         — bounded contexts, choreography vs orchestration
//
// AWS cheat sheet (memorize for interviews):
//   SNS     — pub/sub fan-out; push to SQS/Lambda/HTTP subscribers
//   SQS     — durable queue; standard (ordering not guaranteed) vs FIFO
//   DLQ     — isolate poison messages after maxReceiveCount
//   Lambda  — stateless, event-driven; pair with DynamoDB for idempotency
//   ECS     — containers on Fargate/EC2; always-on APIs, sidecars, longer jobs
//   EventBridge — event bus + rules; schema registry; Scheduler for one-off cron
//
// This project uses in-memory simulators — no AWS account or SDK required.
// Uncomment exercise calls below as you complete each one.

#pragma warning disable CS8321 // Exercise stubs; uncomment calls as you implement them.

using AwsCloud;

Console.WriteLine("=== AwsCloud practice scaffold ===\n");

// REFERENCE — runnable demos (study these first)
DynamoDbConcepts.RunReferenceDemo();
EventDrivenMessaging.RunReferenceDemo();
ServerlessLambda.RunReferenceDemo();
await ScalingAndCache.RunReferenceDemoAsync();
EventBridgeRouting.RunReferenceDemo();
EcsMicroservices.RunReferenceDemo();

// EXERCISES — uncomment as you implement each TODO
// DynamoDbConcepts.Exercise1_DesignKeySchema();
// EventDrivenMessaging.Exercise2_SnsFanOut();
// EventDrivenMessaging.Exercise3_DeadLetterQueue();
// ServerlessLambda.Exercise4_IdempotentHandler();
// ServerlessLambda.Exercise5_LambdaScaling();
// ServerlessLambda.Exercise6_LambdaSqsTrigger();
// ServerlessLambda.Exercise7_LambdaCronTrigger();
// ScalingAndCache.Exercise8_ApiScale();
// await ScalingAndCache.Exercise9_CacheInvalidationAsync();
// EventBridgeRouting.Exercise10_EventBridgeRules();
// EcsMicroservices.Exercise11_EcsScaling();
// EcsMicroservices.Exercise12_MicroservicesSaga();

Console.WriteLine("Done. Implement the TODO exercises and uncomment their calls above.");
