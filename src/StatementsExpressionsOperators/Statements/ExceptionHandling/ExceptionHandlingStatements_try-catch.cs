using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace Statements.ExceptionHandling
{
    public class ExceptionHandlingStatements_try_catch
    {
        // The try-catch statement consists of a try block followed by one or more catch clauses, which specify handlers for different exceptions.

        // When an exception is thrown, the common language runtime (CLR) looks for the catch statement that handles this exception.
        // If the currently executing method does not contain such a catch block, the CLR looks at the method that called
        // the current method, and so on up the call stack. If no catch block is found, then the CLR displays an unhandled
        // exception message to the user and stops execution of the program.
        
        // The try block contains the guarded code that may cause the exception.
        // The block is executed until an exception is thrown or it is completed successfully.

        public static void Example1()
        {
            object o2 = null;
            try
            {
                int i2 = (int) o2; // Error
            }
            // It is possible to use more than one specific catch clause in the same try-catch statement.
            // In this case, the order of the catch clauses is important because the catch clauses are examined in order.
            // Catch the more specific exceptions before the less specific ones.
            // The compiler produces an error if you order your catch blocks so that a later block can never be reached.
            catch (OverflowException e)
            {
            }
            // You can catch one exception and throw a different exception.
            // When you do this, specify the exception that you caught as the inner exception
            catch (InvalidCastException e)
            {
                // You can also re-throw an exception when a specified condition is true
                if (e.Data == null)
                {
                    throw;
                } else {
                    // Perform some action here, and then throw a new exception.
                    throw new YourCustomException("Put your error message here.", e);
                }
            }
            // Using catch arguments is one way to filter for the exceptions you want to handle.
            // You can also use an exception filter that further examines the exception to decide whether to handle it.
            // If the exception filter returns false, then the search for a handler continues.
            catch (ArgumentException e) when (e.ParamName == "…")
            {
            }
            // Exception filters are preferable to catching and rethrowing because filters leave the stack unharmed.
            // If a later handler dumps the stack, you can see where the exception originally came from,
            // rather than just the last place it was rethrown.
            catch (IOException e)
            {
                // Extract some information from this exception, and then
                // throw it to the parent method.
                if (e.Source != null)
                    Console.WriteLine("IOException source: {0}", e.Source);
                throw;
            }
            // Although the catch clause can be used without arguments to catch any type of exception,
            // this usage is not recommended. In general, you should only catch those exceptions that you know how
            // to recover from. Therefore, you should always specify an object argument derived from System.Exception
            catch
            {
            }
        }

        public static void Example2()
        {
            // From inside a try block, initialize only variables that are declared therein.
            // Otherwise, an exception can occur before the execution of the block is completed.
            
            int n;
            try
            {
                // Do not initialize this variable here.
                n = 123;
            } catch {
            }
            // Error: Use of unassigned local variable 'n'.
            // Console.Write(n);
        }
        
        public async Task DoSomethingAsync()
        {
            Task<string> theTask = DelayAsync();
            try
            {
                string result = await theTask;
                Debug.WriteLine("Result: " + result);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception Message: " + ex.Message);
            }
            Debug.WriteLine("Task IsCanceled: " + theTask.IsCanceled);
            Debug.WriteLine("Task IsFaulted:  " + theTask.IsFaulted);
            if (theTask.Exception != null)
            {
                Debug.WriteLine("Task Exception Message: " + theTask.Exception.Message);
                Debug.WriteLine("Task Inner Exception Message: " + theTask.Exception.InnerException.Message);
            }
        }
        // Output when no exception is thrown in the awaited method:
        //   Result: Done
        //   Task IsCanceled: False
        //   Task IsFaulted:  False

        // Output when an Exception is thrown in the awaited method:
        //   Exception Message: Something happened.
        //   Task IsCanceled: False
        //   Task IsFaulted:  True
        //   Task Exception Message: One or more errors occurred.
        //   Task Inner Exception Message: Something happened.

        // Output when a OperationCanceledException or TaskCanceledException
        // is thrown in the awaited method:
        //   Exception Message: canceled
        //   Task IsCanceled: True
        //   Task IsFaulted:  False

        private async Task<string> DelayAsync()
        {
            await Task.Delay(100);

            // Uncomment each of the following lines to
            // demonstrate exception handling.

            //throw new OperationCanceledException("canceled");
            //throw new Exception("Something happened.");
            return "Done";
        }

        public async Task DoMultipleAsync()
        {
            Task theTask1 = ExcAsync("First Task");
            Task theTask2 = ExcAsync("Second Task");
            Task theTask3 = ExcAsync("Third Task");

            Task allTasks = Task.WhenAll(theTask1, theTask2, theTask3);

            try
            {
                await allTasks;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception: " + ex.Message);
                Debug.WriteLine("Task IsFaulted: " + allTasks.IsFaulted);
                foreach (var inEx in allTasks.Exception.InnerExceptions)
                {
                    Debug.WriteLine("Task Inner Exception: " + inEx.Message);
                }
            }
        }
        
        // Output:
        //   Exception: Error-First Task
        //   Task IsFaulted: True
        //   Task Inner Exception: Error-First Task
        //   Task Inner Exception: Error-Second Task
        //   Task Inner Exception: Error-Third Task

        private async Task ExcAsync(string info)
        {
            await Task.Delay(100);

            throw new Exception("Error-" + info);
        }
    }

    public class YourCustomException : Exception
    {
        private string _message;
        private Exception _exception = null;
        public YourCustomException(string message, Exception original)
        {
            _message = message;
            _exception = original;
        }
    }
}