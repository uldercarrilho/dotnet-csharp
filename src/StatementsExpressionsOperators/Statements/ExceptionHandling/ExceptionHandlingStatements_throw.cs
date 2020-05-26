using System;

namespace Statements.ExceptionHandling
{
    public class ExceptionHandlingStatements_throw
    {
        // Signals the occurrence of an exception during program execution.
        // The syntax of throw is:
        // throw [e];
        // where e is an instance of a class derived from System.Exception.
        
        private static int[] numbers = { 2, 4, 6, 8, 10, 12, 14, 16, 18, 20 };

        private static int GetNumber(int index)
        {
            if (index < 0 || index >= numbers.Length) {
                throw new IndexOutOfRangeException();
            }
            return numbers[index];
        }
        
        public static void Example1()
        {
            int index = 10;
            try {
                int value = GetNumber(index);
                Console.WriteLine($"Retrieved {value}");
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine($"{e.GetType().Name}: {index} is outside the bounds of the array");
            }
        }

        public static void Example2()
        {
            GetFirstCharacter(null);
        }
        
        private static char GetFirstCharacter(string value)
        {
            try {
                return value[0];
            }
            catch (NullReferenceException e) {
                Console.Out.WriteLine("Occurs an exception");
                // throw can also be used in a catch block to re-throw an exception handled in a catch block.
                // In this case, throw does not take an exception operand.
                // It is most useful when a method passes on an argument from a caller to some other library method, and
                // the library method throws an exception that must be passed on to the caller.
                throw;
                
                // You can also use the throw e syntax in a catch block to instantiate a new exception that you pass on to the caller.
                // In this case, the stack trace of the original exception, which is available from the StackTrace property, is not preserved.
                // throw new Exception();
            }
        }
        
        // THE THROW EXPRESSION
        // Starting with C# 7.0, throw can be used as an expression as well as a statement.
        // This allows an exception to be thrown in contexts that were previously unsupported.
        
        // THE CONDITIONAL OPERATOR.
        // The following example uses a throw expression to throw an ArgumentException if a method is passed an empty string array.
        // Before C# 7.0, this logic would need to appear in an if/else statement.
        private static void DisplayFirstNumber(string[] args)
        {
            string arg = args.Length >= 1 ? args[0] : throw new ArgumentException("You must supply an argument");
            if (Int64.TryParse(arg, out var number))
                Console.WriteLine($"You entered {number:F0}");
            else
                Console.WriteLine($"{arg} is not a number.");
        }
        
        // THE NULL-COALESCING OPERATOR.
        // In the following example, a throw expression is used with a null-coalescing operator to throw an exception
        // if the string assigned to a Name property is null.
        private static string _name;
        public static string Name
        {
            get => _name;
            set => _name = value ?? throw new ArgumentNullException(paramName: nameof(value), message: "Name cannot be null");
        }

        // AN EXPRESSION-BODIED LAMBDA OR METHOD.
        // The following example illustrates an expression-bodied method that throws an InvalidCastException because a
        // conversion to a DateTime value is not supported.
        private static void Example3()
        {
            DateTime ToDateTime(IFormatProvider provider) =>
                throw new InvalidCastException("Conversion to a DateTime is not supported.");
        }
    }
}