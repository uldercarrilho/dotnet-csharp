using System;

namespace Statements.Jump
{
    public class JumpStatements_break
    {
        // The break statement terminates the closest enclosing loop or switch statement in which it appears.
        // Control is passed to the statement that follows the terminated statement, if any.

        public static void Example1()
        {
            // In this example, the conditional statement contains a counter that is supposed to count from 1 to 100;
            // however, the break statement terminates the loop after 4 counts.
            
            for (int i = 1; i <= 100; i++)
            {
                if (i == 5)
                {
                    break;
                }
                Console.WriteLine(i);
            }
        }

        public static void Example2()
        {
            // This example demonstrates the use of break in a switch statement.

            Console.Write("Enter your selection (1, 2, or 3): ");
            string s = Console.ReadLine();
            int n = Int32.Parse(s);

            switch (n)
            {
                case 1:
                    Console.WriteLine("Current value is 1");
                    break;
                case 2:
                    Console.WriteLine("Current value is 2");
                    break;
                case 3:
                    Console.WriteLine("Current value is 3");
                    break;
                default:
                    Console.WriteLine("Sorry, invalid selection.");
                    break;
            }
        }

        public static void Example3()
        {
            // In this example, the break statement is used to break out of an inner nested loop, and return control to the outer loop.
            // Control is only returned one level up in the nested loops.
            
            int[] numbers = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            char[] letters = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j' };

            // Outer loop.
            for (int i = 0; i < numbers.Length; i++)
            {
                Console.WriteLine($"num = {numbers[i]}");

                // Inner loop.
                for (int j = 0; j < letters.Length; j++)
                {
                    if (j == i)
                    {
                        // Return control to outer loop.
                        break;
                    }
                    Console.Write($" {letters[j]} ");
                }
                Console.WriteLine();
            }
        }

        public static void Example4()
        {
            // In this example, the break statement is only used to break out of the current branch during each iteration of the loop.
            // The loop itself is unaffected by the instances of break that belong to the nested switch statement.
            
            for (int i = 1; i <= 3; i++)
            {
                switch(i)
                {
                    case 1:
                        Console.WriteLine("Current value is 1");
                        break;
                    case 2:
                        Console.WriteLine("Current value is 2");
                        break;
                    case 3:
                        Console.WriteLine("Current value is 3");
                        break;
                    default:
                        Console.WriteLine("This shouldn't happen.");
                        break;
                }
            }
        }
    }
}