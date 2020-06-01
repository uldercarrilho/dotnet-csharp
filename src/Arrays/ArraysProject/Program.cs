using System;

namespace ArraysProject
{
    // You can store multiple variables of the same type in an array data structure. You declare an array by specifying
    // the type of its elements. If you want the array to store elements of any type, you can specify object as its type.
    // In the unified type system of C#, all types, predefined and user-defined, reference types and value types,
    // inherit directly or indirectly from Object.
    
    // An array can be Single-Dimensional, Multidimensional or Jagged.
    
    // The number of dimensions and the length of each dimension are established when the array instance is created.
    // These values can't be changed during the lifetime of the instance.
    
    // The default values of numeric array elements are set to zero, and reference elements are set to null.
    
    // A jagged array is an array of arrays, and therefore its elements are reference types and are initialized to null.
    
    // Arrays are zero indexed: an array with n elements is indexed from 0 to n-1.
    
    // Array elements can be of any type, including an array type.
    
    // Array types are reference types derived from the abstract base type Array.
    // Since this type implements IEnumerable and IEnumerable<T>, you can use foreach iteration on all arrays in C#.
    
    class Program
    {
        static void Main(string[] args)
        {
            // Declare a single-dimensional array of 5 integers.
            // The new operator is used to create the array and initialize the array elements to their default values.
            int[] array1 = new int[5];

            // Declare and set array element values.
            // It is possible to initialize an array upon declaration, in which case, the length specifier is
            // not needed because it is already supplied by the number of elements in the initialization list.
            int[] array2 = new int[] { 1, 3, 5, 7, 9 };

            // Alternative syntax.
            int[] array3 = { 1, 2, 3, 4, 5, 6 };
            string[] weekDays2 = { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" };
            
            // It is possible to declare an array variable without initialization,
            // but you must use the new operator when you assign an array to this variable.
            int[] array4;
            array4 = new int[] { 1, 3, 5, 7, 9 };   // OK
            //array4 = {1, 3, 5, 7, 9};   // Error

            // Declare a two dimensional array.
            int[,] multiDimensionalArray1 = new int[2, 3];

            // Declare and set array element values.
            int[,] multiDimensionalArray2 = { { 1, 2, 3 }, { 4, 5, 6 } };

            // A jagged array is an array whose elements are arrays.
            // A jagged array is an array of arrays, and therefore its elements are reference types and are initialized to null.
            // The elements of a jagged array can be of different dimensions and sizes.
            // A jagged array is sometimes called an "array of arrays.
            int[][] jaggedArray = new int[6][];

            // Set the values of the first array in the jagged array structure.
            // Each of the elements is a single-dimensional array of integers.
            jaggedArray[0] = new int[4] { 1, 2, 3, 4 };
            jaggedArray[1] = new int[] { 4, 5, 6 };
            jaggedArray[2] = new [] { 7, 8, 9 };
            
            // You can also initialize the array upon declaration like this:
            int[][] jaggedArray2 = new int[][]
            {
                new int[] { 1, 3, 5, 7, 9 },
                new int[] { 0, 2, 4, 6 },
                new int[] { 11, 22 }
            };
            
            // You can use the following shorthand form. Notice that you cannot omit the new operator from the elements
            // initialization because there is no default initialization for the elements:
            int[][] jaggedArray3 =
            {
                new int[] { 1, 3, 5, 7, 9 },
                new int[] { 0, 2, 4, 6 },
                new int[] { 11, 22 }
            };
            
            // It is possible to mix jagged and multidimensional arrays. The following is a declaration and initialization
            // of a single-dimensional jagged array that contains three two-dimensional array elements of different sizes.
            int[][,] jaggedArray4 = new int[3][,]
            {
                new int[,] { {1,3}, {5,7} },
                new int[,] { {0,2}, {4,6}, {8,10} },
                new int[,] { {11,22}, {99,88}, {0,9} }
            };
            
            // The method Length returns the number of arrays contained in the jagged array
            Console.WriteLine(jaggedArray4.Length);
            
            // In C#, arrays are actually objects, and not just addressable regions of contiguous memory as in C and C++.
            // Array is the abstract base type of all array types. You can use the properties and other class members that
            // Array has. An example of this is using the Length property to get the length of an array.
            int[] numbers = { 1, 2, 3, 4, 5 };
            int lengthOfNumbers = numbers.Length;
            
            // This example uses the Rank property to display the number of dimensions of an array.
            int[,] theArray = new int[5, 10];
            Console.WriteLine("The array has {0} dimensions.", theArray.Rank);
            
            // The foreach statement provides a simple, clean way to iterate through the elements of an array.
            // For single-dimensional arrays, the foreach statement processes elements in increasing index order,
            // starting with index 0 and ending with index Length - 1:
            int[] numbers2 = { 4, 5, 6, 1, 2, 3, -2, -1, 0 };
            foreach (int i in numbers2)
            {
                Console.Write("{0} ", i);
            }
            // Output: 4 5 6 1 2 3 -2 -1 0
            
            // For multi-dimensional arrays, elements are traversed such that the indices of the rightmost dimension are
            // increased first, then the next left dimension, and so on to the left:
            int[,] numbers2D = new int[3, 2] { { 9, 99 }, { 3, 33 }, { 5, 55 } };
            foreach (int i in numbers2D)
            {
                Console.Write("{0} ", i);
            }
            // Output: 9 99 3 33 5 55
            
            // However, with multidimensional arrays, using a nested for loop gives you more control over the order in which to process the array elements.
            
            
            // Arrays can be passed as arguments to method parameters.
            // Because arrays are reference types, the method can change the value of the elements.
            int[] theArray2 = { 1, 3, 5, 7, 9 };
            PrintArray(theArray2);
            // You can initialize and pass a new array in one step
            PrintArray(new int[] { 1, 3, 5, 7, 9 });
            PrintArray(new [] { 1, 3, 5, 7, 9 });

            // You pass an initialized multidimensional array to a method in the same way that you pass a one-dimensional array.
            int[,] theArray3 = { { 1, 2 }, { 2, 3 }, { 3, 4 } };
            Print2DArray(theArray3);
            // You can initialize and pass a new array in one step
            Print2DArray(new int[,] { { 1, 2 }, { 3, 4 }, { 5, 6 }, { 7, 8 } });
            
            
            // You can create an implicitly-typed array in which the type of the array instance is inferred from the elements
            // specified in the array initializer. The rules for any implicitly-typed variable also apply to implicitly-typed arrays.
            // Implicitly-typed arrays are usually used in query expressions together with anonymous types and object and collection initializers.
            // Notice that with implicitly-typed arrays, no square brackets are used on the left side of the initialization statement.
            var a = new[] { 1, 10, 100, 1000 }; // int[]
            var b = new[] { "hello", null, "world" }; // string[]

            // Note also that jagged arrays are initialized by using new [] just like single-dimension arrays.
            // single-dimension jagged array
            var c = new[]
            {
                new[]{1,2,3,4},
                new[]{5,6,7,8}
            };

            // jagged array of strings
            var d = new[]
            {
                new[]{"Luca", "Mads", "Luke", "Dinesh"},
                new[]{"Karen", "Suma", "Frances"}
            };
            
            // When you create an anonymous type that contains an array, the array must be implicitly typed in the type's object initializer.
            var contacts = new[]
            {
                new {
                    Name = " Eugene Zabokritski",
                    PhoneNumbers = new[] { "206-555-0108", "425-555-0001" }
                },
                new {
                    Name = " Hanying Feng",
                    PhoneNumbers = new[] { "650-555-0199" }
                }
            };
        }

        static void PrintArray(int[] arr) => Console.Out.WriteLine(arr);
        static void Print2DArray(int[,] arr) => Console.Out.WriteLine(arr);
    }
}