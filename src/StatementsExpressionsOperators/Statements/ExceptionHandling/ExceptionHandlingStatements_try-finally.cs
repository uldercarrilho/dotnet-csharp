using System;

namespace Statements.ExceptionHandling
{
    public class ExceptionHandlingStatements_try_finally
    {
        // By using a finally block, you can clean up any resources that are allocated in a try block, and you can run
        // code even if an exception occurs in the try block. Typically, the statements of a finally block run when control
        // leaves a try statement. The transfer of control can occur as a result of normal execution, of execution of a break,
        // continue, goto, or return statement, or of propagation of an exception out of the try statement.
        
        // Within a handled exception, the associated finally block is guaranteed to be run. However, if the exception
        // is unhandled, execution of the finally block is dependent on how the exception unwind operation is triggered.
        // That, in turn, is dependent on how your computer is set up.
        
        public static void Example1()
        {
            int i = 123;
            string s = "Some string";
            object obj = s;

            try
            {
                // Invalid conversion; obj contains a string, not a numeric type.
                i = (int)obj;

                // The following statement is not run.
                Console.WriteLine("WriteLine at the end of the try block.");
            }
            finally
            {
                // To run the program in Visual Studio, type CTRL+F5. Then
                // click Cancel in the error dialog.
                Console.WriteLine("\nExecution of the finally block after an unhandled\n" +
                                  "error depends on how the exception unwind operation is triggered.");
                Console.WriteLine("i = {0}", i);
            }
        }
        // Output:
        // Unhandled Exception: System.InvalidCastException: Specified cast is not valid.
        //
        // Execution of the finally block after an unhandled
        // error depends on how the exception unwind operation is triggered.
        // i = 123
        
        public static void Example2()
        {
            try
            {
                // TryCast produces an unhandled exception.
                TryCast();
            }
            catch (Exception ex)
            {
                // Catch the exception that is unhandled in TryCast.
                Console.WriteLine("Catching the {0} exception triggers the finally block.", ex.GetType());

                // Restore the original unhandled exception.
                // You might not know what exception to expect, or how to handle it, so pass it on.
                throw;
            }
        }

        public static void TryCast()
        {
            int i = 123;
            string s = "Some string";
            object obj = s;

            try
            {
                // Invalid conversion; obj contains a string, not a numeric type.
                i = (int)obj;

                // The following statement is not run.
                Console.WriteLine("WriteLine at the end of the try block.");
            }
            finally
            {
                // Report that the finally block is run, and show that the value of i has not been changed.
                Console.WriteLine("\nIn the finally block in TryCast, i = {0}.\n", i);
            }
        }
        // Output:
        // In the finally block in TryCast, i = 123.
        // Catching the System.InvalidCastException exception triggers the finally block.
        // Unhandled Exception: System.InvalidCastException: Specified cast is not valid.
    }
}