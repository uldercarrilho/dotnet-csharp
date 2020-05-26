using System;

namespace Statements.Iteration
{
    public class IterationStatements_do
    {
        public static void Example()
        {
            int n = 0;
            
            // The do statement executes a statement or a block of statements while a specified Boolean expression evaluates to true.
            // Because that expression is evaluated after each execution of the loop, a do-while loop executes one or more times.
            // This differs from the while loop, which executes zero or more times.
            do
            {
                Console.WriteLine(n);
                
                // At any point within the do statement block, you can break out of the loop by using the break statement.
                // break;
                
                // You can step directly to the evaluation of the while expression by using the continue statement.
                // If the expression evaluates to true, execution continues at the first statement in the loop.
                // Otherwise, execution continues at the first statement after the loop.
                // continue;
                
                // You can also exit a do-while loop by the goto, return, or throw statements.
                // goto label;
                // return;
                // throw new Exception("Exception");
                
                n++;
            } while (n < 5);
        }
    }
}