using System;

namespace Statements.Jump
{
    public class JumpStatements_goto
    {
        // The goto statement transfers the program control directly to a labeled statement.

        public static void Example1()
        {
            // A common use of goto is to transfer control to a specific switch-case label or the default label in a switch statement.

            Console.WriteLine("Coffee sizes: 1=Small 2=Medium 3=Large");
            Console.Write("Please enter your selection: ");
            string s = Console.ReadLine();
            int n = int.Parse(s);
            int cost = 0;
            switch (n)
            {
                case 1:
                    cost += 25;
                    break;
                case 2:
                    cost += 25;
                    goto case 1;
                case 3:
                    cost += 50;
                    goto case 1;
                default:
                    Console.WriteLine("Invalid selection.");
                    break;
            }
            if (cost != 0)
            {
                Console.WriteLine($"Please insert {cost} cents.");
            }
            Console.WriteLine("Thank you for your business.");
        }

        public static void Example2()
        {
            // The goto statement is also useful to get out of deeply nested loops.

            int x = 200, y = 4;
            int count = 0;
            string[,] array = new string[x, y];

            // Initialize the array.
            for (int i = 0; i < x; i++)
                for (int j = 0; j < y; j++)
                    array[i, j] = (++count).ToString();

            // Read input.
            Console.Write("Enter the number to search for: ");

            // Input a string.
            string myNumber = Console.ReadLine();

            // Search.
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    if (array[i, j].Equals(myNumber))
                    {
                        goto Found;
                    }
                }
            }

            Console.WriteLine($"The number {myNumber} was not found.");
            goto Finish;

            Found:
            Console.WriteLine($"The number {myNumber} is found.");

            Finish:
            Console.WriteLine("End of search.");
        }
    }
}