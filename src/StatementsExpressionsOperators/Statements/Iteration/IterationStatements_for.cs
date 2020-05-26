using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Statements.Iteration
{
    public class IterationStatements_for
    {
        // Structure of the for statement
        // for (initializer; condition; iterator)
        //     body

        // All three sections are optional. The body of the loop is either a statement or a block of statements.

        // The for statement executes a statement or a block of statements while a specified Boolean expression evaluates to true.

        // At any point within the for statement block, you can break out of the loop by using the break statement, or
        // step to the next iteration in the loop by using the continue statement.
        // You can also exit a for loop by the goto, return, or throw statements.

        private static void Example1()
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(i);
            }
        }

        // THE INITIALIZER SECTION
        //
        // The statements in the initializer section are executed only once, before entering the loop.
        // The initializer section is either of the following:
        //
        //     The declaration and initialization of a local loop variable, which can't be accessed from outside the loop.
        //     Zero or more statement expressions from the following list, separated by commas:
        //        assignment statement
        //        invocation of a method
        //        prefix or postfix increment expression, such as ++i or i++
        //        prefix or postfix decrement expression, such as --i or i--
        //        creation of an object by using the new operator
        //        await expression
        private static async Task InitializationSection()
        {
            int a = 0;
            int b = 0;
            int c = 0;
            int d = 0;
            int outside;
            List<int> numbers;
            for (outside = 0, InvocationMethod(), ++a, b++, --c, d++, numbers = new List<int>(), await Initialization(); true; )
            {
                Console.Out.WriteLine("Initialization Section");
                break;
            }
        }

        private static async Task Initialization()
        {
            await Task.Delay(100);
        }

        private static void InvocationMethod()
        {
        }
        
        // THE CONDITION SECTION
        //
        // The condition section, if present, must be a boolean expression.
        // That expression is evaluated before every loop iteration.
        // If the condition section is not present or the boolean expression evaluates to true,
        // the next loop iteration is executed; otherwise, the loop is exited.
        private static void ConditionSection(int a, int b)
        {
            for (; a > 2 && b < 3;)
            {
                Console.Out.WriteLine("Condition section");
                break;
            }
        }
        
        // THE ITERATOR SECTION
        // 
        // The iterator section defines what happens after each iteration of the body of the loop.
        // The iterator section contains zero or more of the following statement expressions, separated by commas:
        //        assignment statement
        //        invocation of a method
        //        prefix or postfix increment expression, such as ++i or i++
        //        prefix or postfix decrement expression, such as --i or i--
        //        creation of an object by using the new operator
        //        await expression
        private static async Task IteratorSection()
        {
            int a = 0;
            int b = 0;
            int c = 0;
            int d = 0;
            int outside;
            List<int> numbers;
            for ( ; ; outside = 0, InvocationMethod(), ++a, b++, --c, d++, numbers = new List<int>(), await Initialization())
            {
                Console.Out.WriteLine("Initialization Section");
                if (a == 1) break;
            }
        }

        private static void Examples()
        {
            // loop
            for (;;) { }
        }
    }
}