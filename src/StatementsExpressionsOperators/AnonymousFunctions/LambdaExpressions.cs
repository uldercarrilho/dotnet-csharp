using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AnonymousFunctions
{
    public class LambdaExpressions
    {
        // A lambda expression is an expression of any of the following two forms:
        //    Expression lambda that has an expression as its body:
        //        (input-parameters) => expression
        //    Statement lambda that has a statement block as its body:
        //        (input-parameters) => { <sequence-of-statements> }
        
        // Any lambda expression can be converted to a delegate type.
        // The delegate type to which a lambda expression can be converted is defined by the types of its parameters and return value.
        // If a lambda expression doesn't return a value, it can be converted to one of the Action delegate types;
        // otherwise, it can be converted to one of the Func delegate types. For example,
        // a lambda expression that has two parameters and returns no value can be converted to an Action<T1,T2> delegate.
        // A lambda expression that has one parameter and returns a value can be converted to a Func<T,TResult> delegate.
        
        public static void Example1()
        {
            // delegate type Func<int, int>
            // lambda expression x => x * x
            Func<int, int> square = x => x * x;
            Console.WriteLine(square(5));
            
            // Expression lambdas can also be converted to the expression tree types
            Expression<Func<int, int>> e = x => x * x;
            Console.WriteLine(e);
            // Output:
            // x => (x * x)
            
            // You can use lambda expressions in any code that requires instances of delegate types or expression trees,
            // for example as an argument to the Task.Run(Action) method to pass the code that should be executed in the background.
            
            // You can also use lambda expressions when you write LINQ in C#
            int[] numbers = { 2, 3, 4, 5 };
            var squaredNumbers = numbers.Select(x => x * x);
            Console.WriteLine(string.Join(" ", squaredNumbers));
            // Output:
            // 4 9 16 25
            
            // EXPRESSION LAMBDAS
            
            // A lambda expression with an expression on the right side of the => operator is called an expression lambda.
            // Expression lambdas are used extensively in the construction of expression trees.

            // The parentheses are optional only if the lambda has one input parameter; otherwise they are required.
            // Specify zero input parameters with empty parentheses:
            Action line = () => Console.WriteLine();

            // Two or more input parameters are separated by commas enclosed in parentheses:
            Func<int, int, bool> testForEquality = (x, y) => x == y;
            
            // Sometimes it's impossible for the compiler to infer the input types. You can specify the types explicitly
            Func<int, string, bool> isTooLong = (int x, string s) => s.Length > x;

            // The body of an expression lambda can consist of a method call.
            // However, if you are creating expression trees that are evaluated outside the context of the .NET common
            // language runtime, such as in SQL Server, you should not use method calls in lambda expressions.
            // The methods will have no meaning outside the context of the .NET common language runtime.
            
            
            // STATEMENT LAMBDAS
            
            // A statement lambda resembles an expression lambda except that the statement(s) is enclosed in braces:
            // (input-parameters) => { <sequence-of-statements> }
            // The body of a statement lambda can consist of any number of statements;
            // however, in practice there are typically no more than two or three.

            Action<string> greet = name =>
            {
                string greeting = $"Hello {name}!";
                Console.WriteLine(greeting);
            };
            greet("World");
            // Output:
            // Hello World!
        }
        
        public static void Example2()
        {
            Button button1 = new Button();
            button1.Click += button1_Click;
        }

        private static async void button1_Click(object sender, EventArgs e)
        {
            await ExampleMethodAsync();
            Console.Out.WriteLine("Control returned to Click event handler.");
        }

        public static void Example3()
        {
            // You can add the same event handler by using an async lambda.
            // To add this handler, add an async modifier before the lambda parameter list
            Button button1 = new Button();
            button1.Click += async (sender, e) =>
            {
                await ExampleMethodAsync();
                Console.Out.WriteLine("Control returned to Click event handler.");
            };
        }

        private static async Task ExampleMethodAsync()
        {
            // The following line simulates a task-returning asynchronous process.
            await Task.Delay(1000);
        }
        
        
        // LAMBDA EXPRESSIONS AND TUPLES
        
        // Starting with C# 7.0, the C# language provides built-in support for tuples.
        // You can provide a tuple as an argument to a lambda expression, and your lambda expression can also return a tuple.
        // In some cases, the C# compiler uses type inference to determine the types of tuple components.

        // You define a tuple by enclosing a comma-delimited list of its components in parentheses.
        private static void Example4()
        {
            Func<(int, int, int), (int, int, int)> doubleThem = ns => (2 * ns.Item1, 2 * ns.Item2, 2 * ns.Item3);
            var numbers = (2, 3, 4);
            var doubledNumbers = doubleThem(numbers);
            Console.WriteLine($"The set {numbers} doubled: {doubledNumbers}");
            // Output:
            // The set (2, 3, 4) doubled: (4, 6, 8)
            
            // Ordinarily, the fields of a tuple are named Item1, Item2, etc. You can, however, define a tuple with named components
            Func<(int n1, int n2, int n3), (int, int, int)> tripleThem = ns => (3 * ns.n1, 3 * ns.n2, 3 * ns.n3);
            var values = (2, 3, 4);
            var tripledValues = tripleThem(values);
            Console.WriteLine($"The set {values} tripled: {tripledValues}");
        }
        
        
        // LAMBDAS WITH THE STANDARD QUERY OPERATORS
        
        // LINQ to Objects, among other implementations, have an input parameter whose type is one of the Func<TResult> family of generic delegates.
        // These delegates use type parameters to define the number and type of input parameters, and the return type of the delegate.
        // Func delegates are very useful for encapsulating user-defined expressions that are applied to each element in a set of source data.
        private static void Example5()
        {
            Func<int, bool> equalsFive = x => x == 5;
            bool result = equalsFive(4);
            Console.WriteLine(result);   // False
            
            // You can also supply a lambda expression when the argument type is an Expression<TDelegate>,
            // for example in the standard query operators that are defined in the Queryable type.
            // When you specify an Expression<TDelegate> argument, the lambda is compiled to an expression tree.
            // The compiler can infer the type of the input parameter, or you can also specify it explicitly.
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            int oddNumbers = numbers.Count(n => n % 2 == 1);
            Console.WriteLine($"There are {oddNumbers} odd numbers in {string.Join(" ", numbers)}");
            
            var firstNumbersLessThanSix = numbers.TakeWhile(n => n < 6);
            Console.WriteLine(string.Join(" ", firstNumbersLessThanSix));
            // Output:
            // 5 4 1 3
            
            var firstSmallNumbers = numbers.TakeWhile((n, index) => n >= index);
            Console.WriteLine(string.Join(" ", firstSmallNumbers));
            // Output:
            // 5 4
        }
        
        // TYPE INFERENCE IN LAMBDA EXPRESSIONS

        // When writing lambdas, you often don't have to specify a type for the input parameters because the compiler can infer
        // the type based on the lambda body, the parameter types, and other factors as described in the C# language specification.
        // For most of the standard query operators, the first input is the type of the elements in the source sequence.
        
        // The general rules for type inference for lambdas are as follows:
        //     The lambda must contain the same number of parameters as the delegate type.
        //     Each input parameter in the lambda must be implicitly convertible to its corresponding delegate parameter.
        //     The return value of the lambda (if any) must be implicitly convertible to the delegate's return type.
        //
        // Note that lambda expressions in themselves don't have a type because the common type system has no intrinsic concept of
        // "lambda expression." However, it's sometimes convenient to speak informally of the "type" of a lambda expression.
        // In these cases the type refers to the delegate type or Expression type to which the lambda expression is converted.
        
        
        // CAPTURE OF OUTER VARIABLES AND VARIABLE SCOPE IN LAMBDA EXPRESSIONS
        
        // Lambdas can refer to outer variables. These are the variables that are in scope in the method that
        // defines the lambda expression, or in scope in the type that contains the lambda expression.
        // Variables that are captured in this manner are stored for use in the lambda expression even
        // if the variables would otherwise go out of scope and be garbage collected.
        // An outer variable must be definitely assigned before it can be consumed in a lambda expression.
        public static void Example6()
        {
            var game = new VariableCaptureGame();

            int gameInput = 5;
            game.Run(gameInput);

            int jTry = 10;
            bool result = game.isEqualToCapturedLocalVariable(jTry);
            Console.WriteLine($"Captured local variable is equal to {jTry}: {result}");

            int anotherJ = 3;
            game.updateCapturedLocalVariable(anotherJ);

            bool equalToAnother = game.isEqualToCapturedLocalVariable(anotherJ);
            Console.WriteLine($"Another lambda observes a new value of captured variable: {equalToAnother}");
        }
        // Output:
        // Local variable before lambda invocation: 0
        // 10 is greater than 5: True
        // Local variable after lambda invocation: 10
        // Captured local variable is equal to 10: True
        // 3 is greater than 5: False
        // Another lambda observes a new value of captured variable: True
    }
    
    public class VariableCaptureGame
    {
        internal Action<int> updateCapturedLocalVariable;
        internal Func<int, bool> isEqualToCapturedLocalVariable;

        public void Run(int input)
        {
            int j = 0;

            updateCapturedLocalVariable = x =>
            {
                j = x;
                bool result = j > input;
                Console.WriteLine($"{j} is greater than {input}: {result}");
            };

            isEqualToCapturedLocalVariable = x => x == j;

            Console.WriteLine($"Local variable before lambda invocation: {j}");
            updateCapturedLocalVariable(10);
            Console.WriteLine($"Local variable after lambda invocation: {j}");
        }
    }
    
    // The following rules apply to variable scope in lambda expressions:
    //    A variable that is captured will not be garbage-collected until the delegate that references it becomes eligible for garbage collection.
    //    Variables introduced within a lambda expression are not visible in the enclosing method.
    //    A lambda expression cannot directly capture an in, ref, or out parameter from the enclosing method.
    //    A return statement in a lambda expression doesn't cause the enclosing method to return.
    //    A lambda expression cannot contain a goto, break, or continue statement if the target of that jump statement
    //        is outside the lambda expression block. It's also an error to have a jump statement outside the
    //        lambda expression block if the target is inside the block.

    class Button
    {
        public delegate void OnClick(object sender, EventArgs eventArgs);
        public OnClick Click { get; set; }
    }
}