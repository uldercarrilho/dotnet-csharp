using System;
using System.Collections.Generic;

// Documentation
// https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/statements-expressions-operators/statements

/*
Declaration statements	
    A declaration statement introduces a new variable or constant. 
    A variable declaration can optionally assign a value to the variable. 
    In a constant declaration, the assignment is required.

Expression statements
    Expression statements that calculate a value must store the value in a variable. 
    For more information, see Expression Statements.
    
Selection statements	
    Selection statements enable you to branch to different sections of code, depending on one or more specified conditions. 
    For more information, see the following topics:
        if
        else
        switch
        case

Iteration statements
    Iteration statements enable you to loop through collections like arrays, or perform the same set of statements 
    repeatedly until a specified condition is met. For more information, see the following topics:
        do
        for
        foreach
        in
        while

Jump statements
    Jump statements transfer control to another section of code. For more information, see the following topics:
        break
        continue
        default
        goto
        return
        yield

Exception handling statements
    Exception handling statements enable you to gracefully recover from exceptional conditions that occur at run time. 
    For more information, see the following topics:
        throw
        try-catch
        try-finally
        try-catch-finally

Checked and unchecked
    Checked and unchecked statements enable you to specify whether numerical operations are allowed to cause an 
    overflow when the result is stored in a variable that is too small to hold the resulting value. 
    For more information, see checked and unchecked.
    
The await statement
    If you mark a method with the async modifier, you can use the await operator in the method. 
    When control reaches an await expression in the async method, control returns to the caller, 
    and progress in the method is suspended until the awaited task completes. 
    When the task is complete, execution can resume in the method.

    For a simple example, see the "Async Methods" section of Methods. 
    For more information, see Asynchronous Programming with async and await.
    
The yield return statement
    An iterator performs a custom iteration over a collection, such as a list or an array. 
    An iterator uses the yield return statement to return each element one at a time. 
    When a yield return statement is reached, the current location in code is remembered. 
    Execution is restarted from that location when the iterator is called the next time.
    For more information, see Iterators.

The fixed statement	
    The fixed statement prevents the garbage collector from relocating a movable variable. For more information, see fixed.
    
The lock statement
    The lock statement enables you to limit access to blocks of code to only one thread at a time. For more information, see lock.
    
Labeled statements
    You can give a statement a label and then use the goto keyword to jump to the labeled statement.
    
The empty statement
    The empty statement consists of a single semicolon. 
    It does nothing and can be used in places where a statement is required but no action needs to be performed.
 */

namespace Statements
{
    public class Overview
    {
        private static void DeclarationStatements()
        {
            // Variable declaration statements.
            int counter;
            double area;
            double radius = 2;
            
            // Constant declaration statement.
            const double pi = 3.14159;
            int[] radii = { 15, 32, 108, 74, 9 }; // Declare and initialize an array

            // Assignment statement.
            counter = 1;
            // Expression statement (assignment).
            area = 3.14 * (radius * radius);
        }

        private static void ExpressionStatements()
        {
            // Error. Not  statement because no assignment:
            //circ * 2;

            // Expression statement (method invocation).
            System.Console.WriteLine();

            // Expression statement (new object creation).
            List<string> strings = new List<string>();
        }

        private static void EmptyStatement1()
        {
            while (ProcessMessage())
                ; // Statement needed here.
        }

        private static bool ProcessMessage() => false;

        private static void EmptyStatement2()
        {
            //...
            if (true) goto exit;
            //...
            exit:
            ; // Statement needed here.
        }

        private static void EmbeddedStatements()
        {
            // Some statements, including do, while, for, and foreach, always have an embedded statement that follows them
            
            // Recommended style. Embedded statement in  block.
            foreach (string s in System.IO.Directory.GetDirectories(Environment.CurrentDirectory))
            {
                Console.WriteLine(s);
            }

            // Not recommended.
            foreach (string s in System.IO.Directory.GetDirectories(Environment.CurrentDirectory))
                Console.WriteLine(s);
            
            // An embedded statement that is not enclosed in {} brackets cannot be a declaration statement or a labeled statement.
            /*
            if(pointB == true)
                //Error CS1023:
                int radius = 5;
            */
            
            if (true)
            {
                // OK:
                DateTime d = DateTime.Now;
                Console.WriteLine(d.ToLongDateString());
            }
        }

        private static string NestedStatementBlocks()
        {
            foreach (string s in System.IO.Directory.GetDirectories(Environment.CurrentDirectory))
            {
                if (s.StartsWith("CSharp"))
                {
                    if (s.EndsWith("TempFolder"))
                    {
                        return s;
                    }
                }
            }
            return "Not found.";
        }

        private static void UnreachableStatements()
        {
            // An over-simplified example of unreachable code.
            const int val = 5;
            if (val < 4)
            {
                System.Console.WriteLine("I'll never write anything."); //CS0162
            }
        }
    }

}