using System;

namespace Statements
{
    public class FixedStatement
    {
        // The fixed statement prevents the garbage collector from relocating a movable variable.
        // The fixed statement is only permitted in an unsafe context.
        // You can also use the fixed keyword to create fixed size buffers.

        // The fixed statement sets a pointer to a managed variable and "pins" that variable during the execution of the statement.
        // Pointers to movable managed variables are useful only in a fixed context.
        // Without a fixed context, garbage collection could relocate the variables unpredictably.
        // The C# compiler only lets you assign a pointer to a managed variable in a fixed statement.

        // compile with: -unsafe
        
        // You can allocate memory on the stack, where it is not subject to garbage collection and
        // therefore does not need to be pinned. To do that, use a stackalloc expression.
        
        class Point
        {
            public int x;
            public int y;
        }

        private static unsafe void ModifyFixedStorage()
        {
            // Variable pt is a managed variable, subject to garbage collection.
            Point pt = new Point();

            // Using fixed allows the address of pt members to be taken,
            // and "pins" pt so that it is not relocated.

            fixed (int* p = &pt.x)
            {
                *p = 1;
            }
        }

        private static unsafe void Example2()
        {
            Point point = new Point();
            double[] arr = { 0, 1.5, 2.3, 3.4, 4.0, 5.9 };
            string str = "Hello World";

            // The following two assignments are equivalent. Each assigns the address
            // of the first element in array arr to pointer p.

            // You can initialize a pointer by using an array.
            fixed (double* p = arr) { /*...*/ }

            // You can initialize a pointer by using the address of a variable.
            fixed (double* p = &arr[0]) { /*...*/ }

            // The following assignment initializes p by using a string.
            fixed (char* p = str) { /*...*/ }

            // The following assignment is not valid, because str[0] is a char,
            // which is a value, not a variable.
            //fixed (char* p = &str[0]) { /*...*/ }

            // To initialize pointers of different types, simply nest fixed statements
            fixed (int* p1 = &point.x)
            {
                fixed (double* p2 = &arr[5])
                {
                    // Do something with p1 and p2.
                    
                    // Pointers initialized in fixed statements are readonly variables.
                    // If you want to modify the pointer value, you must declare a second pointer variable, and modify that.
                    // The variable declared in the fixed statement cannot be modified.
                    double* p2Copy = p2;
                    p2Copy++; // point to the next element.
                    // p2++; // invalid: cannot modify p2, as it is declared in the fixed statement.
                }
            }
            // After the code in the statement is executed, any pinned variables are unpinned and subject to garbage collection.
            // Therefore, do not point to those variables outside the fixed statement.
            // The variables declared in the fixed statement are scoped to that statement.
            // p1 and p2 are no longer in scope here

        }
        
        // Starting with C# 7.3, the fixed statement operates on additional types beyond arrays, strings, fixed size buffers,
        // or unmanaged variables. Any type that implements a method named GetPinnableReference can be pinned.
        // The GetPinnableReference must return a ref variable of an unmanaged type. The .NET types System.Span<T> and
        // System.ReadOnlySpan<T> introduced in .NET Core 2.0 make use of this pattern and can be pinned.
        private static unsafe void FixedSpanExample()
        {
            int[] PascalsTriangle = {
                1,
                1,  1,
                1,  2,  1,
                1,  3,  3,  1,
                1,  4,  6,  4,  1,
                1,  5,  10, 10, 5,  1
            };

            Span<int> RowFive = new Span<int>(PascalsTriangle, 10, 5);

            fixed (int* ptrToRow = RowFive)
            {
                // Sum the numbers 1,4,6,4,1
                var sum = 0;
                for (int i = 0; i < RowFive.Length; i++)
                {
                    sum += *(ptrToRow + i);
                }
                Console.WriteLine(sum);
            }
        }
    }
}