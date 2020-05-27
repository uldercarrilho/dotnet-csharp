using System;

namespace AnonymousFunctions
{
    // An anonymous function is an "inline" statement or expression that can be used wherever a delegate type is expected.
    // You can use it to initialize a named delegate or pass it instead of a named delegate type as a method parameter.
    
    // You can use a lambda expression or an anonymous method to create an anonymous function.
    // We recommend using lambda expressions as they provide more concise and expressive way to write inline code.
    // Unlike anonymous methods, some types of lambda expressions can be converted to the expression tree types.
    
    class Program
    {
        delegate void TestDelegate(string s);
        static void M(string s)
        {
            Console.WriteLine(s);
        }

        static void Main(string[] args)
        {
            // Original delegate syntax required
            // initialization with a named method.
            TestDelegate testDelA = new TestDelegate(M);

            // C# 2.0: A delegate can be initialized with
            // inline code, called an "anonymous method." This
            // method takes a string as an input parameter.
            TestDelegate testDelB = delegate(string s) { Console.WriteLine(s); };

            // C# 3.0. A delegate can be initialized with
            // a lambda expression. The lambda also takes a string
            // as an input parameter (x). The type of x is inferred by the compiler.
            TestDelegate testDelC = (x) => { Console.WriteLine(x); };

            // Invoke the delegates.
            testDelA("Hello. My name is M and I write lines.");
            testDelB("That's nothing. I'm anonymous and ");
            testDelC("I'm a famous author.");
        }
    }
}