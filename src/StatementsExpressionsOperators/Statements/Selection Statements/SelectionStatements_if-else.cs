using System;

namespace Statements.Selection_Statements
{
    public class SelectionStatements_if_else
    {
        public static void Examples(bool OutterConditional, bool InnerConditional)
        {
            // For a single statement, the braces are optional but recommended.
            if (OutterConditional)
                Console.Out.WriteLine("Do something");

            // if statement without an else
            if (OutterConditional)
            {
                Console.Out.WriteLine("Statements enclosed in braces");
            }

            // if-else statement
            if (OutterConditional)
            {
                Console.Out.WriteLine("then statement");
            } else {
                Console.Out.WriteLine("else statement");
            }
            
            // In nested if statements, each else clause belongs to the last if that doesn’t have a corresponding else
            if (OutterConditional)
                if (InnerConditional)
                {
                    Console.Out.WriteLine("InnerConditional if-then statement");
                }
                else
                {
                    Console.Out.WriteLine("InnerConditional if-else statement");
                }

            // you can specify that association by using braces to establish the start and end of the nested if statement
            if (OutterConditional)
            {
                if (InnerConditional) 
                    Console.Out.WriteLine("InnerConditional if-then statement");
            } else {
                Console.Out.WriteLine("OutterConditional if-else statement");
            }

            bool valueAnd = OutterConditional && InnerConditional;
            bool valueOr = OutterConditional || InnerConditional;
            bool valueNot = !OutterConditional;
            // many if-else
            if (valueAnd)
            {
                Console.Out.WriteLine("Value AND");
            } else if (valueOr)
            {
                Console.Out.WriteLine("Value OR");
            } else if (valueNot)
            {
                Console.Out.WriteLine("Value NOT");
            } 
            else 
            {
                Console.Out.WriteLine("Value default");
            }
            
            // ternary operator
            string boolStr = (OutterConditional) ? "true" : "false";
        }
    }
}