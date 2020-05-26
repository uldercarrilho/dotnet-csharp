using System;

// Documentation
// https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/checked-and-unchecked

namespace Statements
{
    public class CheckedStatement
    {
        // C# statements can execute in either checked or unchecked context.
        
        // In a checked context, arithmetic overflow raises an exception.
        
        // In an unchecked context, arithmetic overflow is ignored and the result is truncated by discarding any
        // high-order bits that don't fit in the destination type.
        
        // The following operations are affected by the overflow checking:
        //    Expressions using the following predefined operators on integral types: ++, --, unary -, +, -, *, /
        //    Explicit numeric conversions between integral types, or from float or double to an integral type.
        
        // If neither checked nor unchecked is specified, the default context for non-constant expressions
        // (expressions that are evaluated at run time) is defined by the value of the -checked compiler option.
        // By default the value of that option is unset and arithmetic operations are executed in an unchecked context.
        
        // For constant expressions (expressions that can be fully evaluated at compile time),
        // the default context is always checked. Unless a constant expression is explicitly placed in an unchecked context,
        // overflows that occur during the compile-time evaluation of the expression cause compile-time errors.
        
        // Overflow checking can be enabled by compiler options, environment configuration, or use of the checked keyword.
        
        // Because checking for overflow takes time, the use of unchecked code in situations where there is no danger of
        // overflow might improve performance. However, if overflow is a possibility, a checked environment should be used.
        
        public static void Example1()
        {
            // The following example causes compiler error CS0220 because 2147483647
            // is the maximum value for integers.
            //int i1 = 2147483647 + 10;

            // The following example, which includes variable ten, does not cause
            // a compiler error.
            int ten = 10;
            int i2 = 2147483647 + ten;

            // By default, the overflow in the previous statement also does
            // not cause a run-time exception. The following line displays
            // -2,147,483,639 as the sum of 2,147,483,647 and 10.
            Console.WriteLine(i2);
            
            // If the previous sum is attempted in a checked environment, an OverflowException error is raised.

            // Checked expression.
            Console.WriteLine(checked(2147483647 + ten));

            // Checked block.
            checked
            {
                int i3 = 2147483647 + ten;
                Console.WriteLine(i3);
            }
        }

        private static void Example2()
        {
            // Unchecked sections frequently are used to break out of a checked
            // environment in order to improve performance in a portion of code
            // that is not expected to raise overflow exceptions.
            checked
            {
                // Code that might cause overflow should be executed in a checked
                // environment.
                unchecked
                {
                    // This section is appropriate for code that you are confident
                    // will not result in overflow, and for which performance is
                    // a priority.
                }
                // Additional checked code here.
            }
        }

        // Set maxIntValue to the maximum value for integers.
        static int maxIntValue = 2147483647;

        static int CheckedMethod()
        {
            int z = 0;
            try
            {
                // The following line raises an exception because it is checked.
                z = checked(maxIntValue + 10);
            }
            catch (System.OverflowException e)
            {
                // The following line displays information about the error.
                Console.WriteLine("CHECKED and CAUGHT:  " + e.ToString());
            }
            // The value of z is still 0.
            return z;
        }

        static int UncheckedMethod()
        {
            int z = 0;
            try
            {
                // The following calculation is unchecked and will not
                // raise an exception.
                z = maxIntValue + 10;
            }
            catch (System.OverflowException e)
            {
                // The following line will not be executed.
                Console.WriteLine("UNCHECKED and CAUGHT:  " + e.ToString());
            }
            // Because of the undetected overflow, the sum of 2147483647 + 10 is
            // returned as -2147483639.
            return z;
        }
    }
}