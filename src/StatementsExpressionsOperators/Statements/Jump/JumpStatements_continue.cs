using System;

namespace Statements.Jump
{
    public class JumpStatements_continue
    {
        // The continue statement passes control to the next iteration of the enclosing while, do, for, or foreach statement in which it appears.

        public static void Example1()
        {
            for (int i = 1; i <= 10; i++)
            {
                if (i < 9)
                {
                    continue;
                }
                Console.WriteLine(i);
            }            
        }
    }
}