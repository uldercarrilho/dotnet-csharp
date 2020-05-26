using System;

// Documentation
// https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/return

namespace Statements.Jump
{
    public class JumpStatements_return
    {
        // The return statement terminates execution of the method in which it appears and returns control to the calling method.
        public static void Run()
        {
            Example1();
            Example2();
            Example3(3);
        }

        private static void Example1()
        {
            Console.Out.WriteLine("Method return void");
            // If the method is a void type, the return statement can be omitted.
            // return;
        }

        private static int Example2()
        {
            Console.Out.WriteLine("Method return int");
            // It can also return an optional value.
            return 0;
        }

        private static void Example3(int value)
        {
            try
            {
                Console.Out.WriteLine("Execute return inside a try-finally block");
                if (value > 10) return;
                Console.Out.WriteLine("Value less then 10");
            }
            finally
            {
                Console.Out.WriteLine("Finally");
            }
        }
    }
}