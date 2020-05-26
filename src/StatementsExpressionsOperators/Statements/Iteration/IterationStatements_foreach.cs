using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Statements.Iteration
{
    public class IterationStatements_foreach
    {
        // The foreach statement executes a statement or a block of statements for each element in an instance of the
        // type that implements the System.Collections.IEnumerable or System.Collections.Generic.IEnumerable<T> interface.
        // The foreach statement is not limited to those types and can be applied to an instance of any type that satisfies the following conditions:
        //
        //     has the public parameterless GetEnumerator method whose return type is either class, struct, or interface type,
        //     the return type of the GetEnumerator method has the
        //         public Current property and
        //         the public parameterless MoveNext method whose return type is Boolean.
        
        public static void Example1()
        {
            //  List<T> type that implements the IEnumerable<T> interface
            var fibNumbers = new List<int> { 0, 1, 1, 2, 3, 5, 8, 13 };
            int count = 0;
            foreach (int element in fibNumbers)
            {
                count++;
                Console.WriteLine($"Element #{count}: {element}");
                
                // At any point within the foreach statement block, you can break out of the loop by using the break statement,
                // or step to the next iteration in the loop by using the continue statement.
                // You can also exit a foreach loop by the goto, return, or throw statements.
            }
            Console.WriteLine($"Number of elements: {count}");
        }
        
        public static void Example2()
        {
            // the System.Span<T> type doesn't implement any interfaces
            Span<int> numbers = new int[] { 3, 14, 15, 92, 6 };
            foreach (int number in numbers)
            {
                Console.Write($"{number} ");
            }
            Console.WriteLine();
        }
        
        // Beginning with C# 7.3, if the enumerator's Current property returns a reference return value
        // (ref T where T is the type of the collection element),
        // you can declare the iteration variable with the ref or ref readonly modifier.
        public static void Example3()
        {
            Span<int> storage = stackalloc int[10];
            int num = 0;
            foreach (ref int item in storage)
            {
                item = num++;
            }

            foreach (ref readonly var item in storage)
            {
                Console.Write($"{item} ");
            }
            // Output:
            // 0 1 2 3 4 5 6 7 8 9
        }

        // Beginning with C# 8.0, the await operator may be applied to the foreach statement when the collection type
        // implements the IAsyncEnumerable<T> interface. Each iteration of the loop may be suspended while the next
        // element is retrieved asynchronously. By default, stream elements are processed in the captured context.
        // If you want to disable capturing of the context, use the TaskAsyncEnumerableExtensions.ConfigureAwait extension method.
        public static async Task Example4()
        {
            await foreach (var item in GenerateSequenceAsync())
            {
                Console.WriteLine(item);
            }
        }

        private static async IAsyncEnumerable<int> GenerateSequenceAsync()
        {
            await Task.Delay(100);
            yield return 0;
            await Task.Delay(100);
            yield return 1;
        }

        public static void Example5()
        {
            // If the source collection of the foreach statement is empty, the body of the foreach loop isn't executed and skipped.
            List<int> emptyList = new List<int>();
            foreach (int number in emptyList)
            {
                Console.Out.WriteLine($"number = {number}");
            }
        }

        private static void Example6()
        {
            // If the foreach statement is applied to null, a NullReferenceException is thrown.
            List<int> emptyList = null;
            foreach (int number in emptyList)
            {
                Console.Out.WriteLine($"number = {number}");
            }
        }
    }
}