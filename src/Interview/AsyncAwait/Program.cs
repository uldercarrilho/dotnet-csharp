// =============================================================================
// ASYNC / AWAIT & INNER EXCEPTIONS — Interview practice scaffold
// =============================================================================
//
// Run:  dotnet run --project src/Interview/AsyncAwait
//
// What you will learn:
//   - async/await syntax and when the compiler rewrites your method into a state machine
//   - Why await unwraps exceptions directly, but .Wait() / .Result wrap them in AggregateException
//   - How to walk the InnerException chain to find the root cause
//   - Safe rethrow patterns that preserve stack traces (throw; vs throw ex;)
//
// Uncomment exercise calls in Main below as you complete each one.

#pragma warning disable CS8321 // Exercise stubs; uncomment calls in Main as you implement them.

using System.Reflection.Metadata;
using System.Runtime.CompilerServices;

Console.WriteLine("=== AsyncAwait practice scaffold ===\n");

await ReferenceExampleAsync();

// Uncomment as you implement each exercise:
await Exercise1_BasicAsyncTryCatchAsync();
await Exercise2_AwaitVsBlockingAsync();
await Exercise3_WalkInnerExceptionChainAsync();
await Exercise4_WrapWithMeaningfulMessageAsync();
await Exercise5_TaskWhenAllMultipleFailuresAsync();
await Exercise6_PreserveStackTraceOnRethrowAsync();

Console.WriteLine("\nDone. Implement the TODO exercises and uncomment their calls above.");

// -----------------------------------------------------------------------------
// REFERENCE — working async/await with exception handling (study this first)
// -----------------------------------------------------------------------------
// async marks a method as asynchronous; await suspends without blocking the thread.
// When an awaited Task faults, the exception is thrown at the await point — NOT wrapped
// in AggregateException (that only happens with synchronous blocking: .Wait(), .Result).
static async Task ReferenceExampleAsync()
{
    Console.WriteLine("--- REFERENCE ---");

    try
    {
        await SimulateRemoteCallAsync(shouldFail: true);
        Console.WriteLine("This line never runs when the task faults.");
    }
    catch (InvalidOperationException ex)
    {
        // With await, ex is the original exception type — no AggregateException wrapper.
        Console.WriteLine($"Caught: {ex.GetType().Name} — {ex.Message}");
    }

    Console.WriteLine();
}

static async Task SimulateRemoteCallAsync(bool shouldFail)
{
    await Task.Delay(50); // simulates I/O; releases the thread while waiting

    if (shouldFail)
        throw new InvalidOperationException("Simulated downstream failure");
}

// =============================================================================
// EXERCISE 1 — Basic async try/catch
// =============================================================================
// Goal: Write an async method that calls SimulateRemoteCallAsync(shouldFail: true)
//       inside try/catch and prints the exception message without crashing.
//
// Steps:
//   1. Create async Task Exercise1_BasicAsyncTryCatchAsync()
//   2. await SimulateRemoteCallAsync(shouldFail: true) inside try
//   3. catch (Exception ex) and print ex.Message
//
// Test: Uncomment the call in Main — should print the error and exit cleanly.
//
// Interview tip: Prefer catching specific exception types when you can handle them;
// catch (Exception) is acceptable at app boundaries (logging, middleware).

static async Task Exercise1_BasicAsyncTryCatchAsync()
{
    Console.WriteLine("--- EXERCISE 1 ---");

    try {
        await SimulateRemoteCallAsync(shouldFail: true);
    } catch (Exception ex) {
        Console.WriteLine(ex.Message);
    }
}

// =============================================================================
// EXERCISE 2 — await vs blocking (.Wait / .Result)
// =============================================================================
// Goal: Demonstrate the difference between await and synchronous blocking.
//
// Steps:
//   1. Create a helper that throws: static async Task FailingTaskAsync()
//      => throw new ArgumentException("Leaf failure");
//   2. Part A — use await FailingTaskAsync() in try/catch; print caught type + message
//   3. Part B — call FailingTaskAsync().Wait() in try/catch
//   4. Observe Part B catches AggregateException; print ex.InnerException details
//
// Test: Part A should catch ArgumentException directly.
//       Part B should catch AggregateException with InnerException = ArgumentException.
//
// Interview tip: Never use .Wait() or .Result on async code in ASP.NET — it can cause
// deadlocks (capture of sync context) and hides the real exception type.

static async Task Exercise2_AwaitVsBlockingAsync()
{
    Console.WriteLine("--- EXERCISE 2 ---");

    try
    {
        await FailingTaskAsync();
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.GetType() + " " + ex.Message);
    }

    try
    {
        FailingTaskAsync().Wait();
    }
    catch (AggregateException ex)
    {
        Console.WriteLine(ex.GetType() + " " + ex.InnerException?.Message);
    }
}

#pragma warning disable CS1998
static async Task FailingTaskAsync()
    => throw new ArgumentException("Leaf failure");
#pragma warning restore CS1998

// =============================================================================
// EXERCISE 3 — Walk the InnerException chain
// =============================================================================
// Goal: Given a nested exception (A wraps B wraps C), print every message in the chain.
//
// Steps:
//   1. Build a chain: new ApplicationException("outer",
//         new InvalidOperationException("middle",
//           new ArgumentException("root")))
//   2. Write static void PrintExceptionChain(Exception ex) that loops:
//        while (ex is not null) { print type + message; ex = ex.InnerException; }
//   3. Call it from Exercise3_WalkInnerExceptionChainAsync
//
// Test: Should print three lines — ApplicationException, InvalidOperationException,
//       ArgumentException (root cause last).
//
// Interview tip: Production logging often walks InnerException to find the root cause.
// AggregateException.InnerExceptions (plural) is a collection — use Flatten() first.

static Task Exercise3_WalkInnerExceptionChainAsync()
{
    Console.WriteLine("--- EXERCISE 3 ---");

    var ex = new ApplicationException("outer",
                new InvalidOperationException("middle",
                    new ArgumentException("root")));
    PrintExceptionChain(ex);

    return Task.CompletedTask;
}

static void PrintExceptionChain(Exception? ex)
{
    while (ex is not null) {
        Console.WriteLine(ex.GetType() + " " + ex.Message);
        ex = ex.InnerException;
    }
}

// =============================================================================
// EXERCISE 4 — Wrap with a meaningful outer exception
// =============================================================================
// Goal: Catch a low-level exception and rethrow a domain-specific one that preserves
//       the original as InnerException (same pattern as EF, HttpClient wrappers).
//
// Steps:
//   1. Write async Task<int> ParseUserIdAsync(string raw)
//   2. await Task.Delay(10) then int.Parse(raw) inside try
//   3. On FormatException, throw new ArgumentException(
//        $"Invalid user id: '{raw}'", nameof(raw), innerException: caught)
//   4. In Exercise4, call with "abc" and catch ArgumentException; verify InnerException
//      is FormatException via PrintExceptionChain or similar
//
// Test: Outer message mentions 'abc'; InnerException is FormatException.
//
// Interview tip: Always pass the original exception as inner when wrapping — it keeps
// the stack trace and root cause for diagnostics.

static async Task Exercise4_WrapWithMeaningfulMessageAsync()
{
    Console.WriteLine("--- EXERCISE 4 ---");

    try
    {
        await ParseUserIdAsync("abc");    
    }
    catch (Exception ex)
    {
        PrintExceptionChain(ex);
    }
    
}

static async Task<int> ParseUserIdAsync(string raw)
{
    await Task.Delay(10);
    try
    {
        return int.Parse(raw);
    }
    catch (Exception caught)
    {
        throw new ArgumentException($"Invalid user id: '{raw}'", nameof(raw), innerException: caught);        
    }
}

// =============================================================================
// EXERCISE 5 — Task.WhenAll and multiple failures
// =============================================================================
// Goal: When several tasks fail, inspect AggregateException from .Wait().
//
// Steps:
//   1. Start three tasks that each await Task.Delay then throw distinct exceptions
//   2. await Task.WhenAll(tasks) in try/catch — note: only the FIRST fault is thrown
//   3. Also try Task.WhenAll(tasks).Wait() and catch AggregateException
//   4. Use aggEx.Flatten().InnerExceptions to list ALL failures
//
// Test: await path throws one exception; .Wait() path exposes all via Flatten().
//
// Interview tip: Task.WhenAll with await throws the first exception only. For parallel
// fan-out where you need every failure, consider Task.WhenAll + per-task try/catch,
// or catch and inspect if you must block (rare).

static async Task Exercise5_TaskWhenAllMultipleFailuresAsync()
{
    Console.WriteLine("--- EXERCISE 5 ---");

    var tasksForAwait = new Task[]
    {
        DelayThenThrowAsync(30, new InvalidOperationException("failure 1")),
        DelayThenThrowAsync(20, new ArgumentException("failure 2")),
        DelayThenThrowAsync(10, new ApplicationException("failure 3")),
    };

    try
    {
        await Task.WhenAll(tasksForAwait);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"await path (first fault only): {ex.GetType().Name} — {ex.Message}");
    }

    var tasksForWait = new Task[]
    {
        DelayThenThrowAsync(30, new InvalidOperationException("failure 1")),
        DelayThenThrowAsync(20, new ArgumentException("failure 2")),
        DelayThenThrowAsync(10, new ApplicationException("failure 3")),
    };

    try
    {
        Task.WhenAll(tasksForWait).Wait();
    }
    catch (AggregateException aggEx)
    {
        Console.WriteLine("Wait() path — all failures via Flatten():");
        foreach (var inner in aggEx.Flatten().InnerExceptions)
            Console.WriteLine($"  {inner.GetType().Name} — {inner.Message}");
    }
}

static async Task DelayThenThrowAsync(int delayMs, Exception ex)
{
    await Task.Delay(delayMs);
    throw ex;
}


// =============================================================================
// EXERCISE 6 — Preserve stack trace when rethrowing
// =============================================================================
// Goal: Compare bad rethrow (throw ex) vs correct rethrow (throw).
//
// Steps:
//   1. Write async Task ThrowFromDeepAsync() that throws InvalidOperationException
//   2. Write async Task MiddleLayerAsync() that catches, logs, then rethrows
//   3. Demonstrate TWO versions (or comment one out):
//        BAD:  catch (Exception ex) { Console.WriteLine("logged"); throw ex; }
//        GOOD: catch (Exception)   { Console.WriteLine("logged"); throw; }
//   4. Print StackTrace in the outer catch and observe GOOD keeps the original frame
//
// Test: With `throw;`, stack trace still points at ThrowFromDeepAsync.
//       With `throw ex;`, stack trace starts at MiddleLayerAsync (information loss).
//
// Interview tip: In async code, `throw;` and ExceptionDispatchInfo.Capture(ex).Throw()
// preserve stack traces. Never `throw ex;` unless you intentionally reset the stack.

static async Task Exercise6_PreserveStackTraceOnRethrowAsync()
{
    Console.WriteLine("--- EXERCISE 6 ---");

    Console.WriteLine("GOOD — throw; preserves original stack:");
    try
    {
        await MiddleLayerGoodRethrowAsync();
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.StackTrace);
    }

    Console.WriteLine();
    Console.WriteLine("BAD — throw ex; resets stack trace:");
    try
    {
        await MiddleLayerBadRethrowAsync();
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.StackTrace);
    }
}

static async Task ThrowFromDeepAsync()
{
    await Task.Delay(10);
    throw new InvalidOperationException("Deep failure");
}

static async Task MiddleLayerGoodRethrowAsync()
{
    try
    {
        await ThrowFromDeepAsync();
    }
    catch (Exception)
    {
        Console.WriteLine("logged");
        throw;
    }
}

static async Task MiddleLayerBadRethrowAsync()
{
    try
    {
        await ThrowFromDeepAsync();
    }
    catch (Exception ex)
    {
        Console.WriteLine("logged");
        throw ex;
    }
}
