using System;

namespace Statements.Iteration
{
    public class IterationStatements_while
    {
        public static void Example1()
        {
            // The while statement executes a statement or a block of statements while a specified Boolean expression evaluates to true.
            // Because that expression is evaluated before each execution of the loop, a while loop executes zero or more times.
            // This differs from the do loop, which executes one or more times.
            int n = 0;
            while (n < 5)
            {
                Console.WriteLine(n);
                
                // At any point within the while statement block, you can break out of the loop by using the break statement.

                // You can step directly to the evaluation of the while expression by using the continue statement.
                // If the expression evaluates to true, execution continues at the first statement in the loop.
                // Otherwise, execution continues at the first statement after the loop.
                
                // You can also exit a while loop by the goto, return, or throw statements.

                n++;
            }
        }
    }
}