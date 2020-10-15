using System;

// Documentation
// https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/main-and-command-args/

namespace ConsoleApp
{
    class Program
    {
        // The Main method is the entry point of a C# application.
        // Libraries and services do not require a Main method as an entry point.
        
        // Overview
        // 1. The Main method is the entry point of an executable program
        // 2. Main is declared inside a class or struct.
        // 3. Main must be static and it need not be public. The enclosing class or struct is not required to be static.
        // 4. Main can either have a void, int, or, starting with C# 7.1, Task, or Task<int> return type.
        // 5. If and only if Main returns a Task or Task<int>, the declaration of Main may include the async modifier.
        //     Note that this specifically excludes an async void Main method.
        // 6. The Main method can be declared with or without a string[] parameter that contains command-line arguments.
        // 7. Use the GetCommandLineArgs() method to obtain the command-line arguments.
        //     Parameters are read as zero-indexed command-line arguments.
        //     Unlike C and C++, the name of the program is not treated as the first command-line argument in the args array,
        //     but it is the first element of the GetCommandLineArgs() method.
        static void Main(string[] args)
        {
            Console.Out.WriteLine("Hello World!");

            if (args.Length == 0)
            {
                Console.Out.WriteLine("No parameters");
                return;
            }
            
            Console.Out.WriteLine("\nRead command-line arguments from parameter 'args'");
            for (int i = 0; i < args.Length; i++)
            {
                Console.Out.WriteLine($"args[{i}]: {args[i]}");
            }

            Console.Out.WriteLine("\nRead command-line arguments from Environment");
            string[] array = Environment.GetCommandLineArgs();
            Console.Out.WriteLine(string.Join(",", array));

            // The program name can include path information, but is not required to do so.
            Console.Out.WriteLine("\nRead command-line from Environment");
            Console.Out.WriteLine(Environment.CommandLine);
        }

        // The following is a list of valid Main signatures:
        /*
        public static void Main() { }
        public static int Main() { }
        public static void Main(string[] args) { }
        public static int Main(string[] args) { }
        public static async Task Main() { }
        public static async Task<int> Main() { }
        public static async Task Main(string[] args) { }
        public static async Task<int> Main(string[] args) { }
        */        
    }
}