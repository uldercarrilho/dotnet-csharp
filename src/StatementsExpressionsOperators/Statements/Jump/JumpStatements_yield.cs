using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Statements.Jump
{
    public class JumpStatements_yield
    {
        // When you use the yield contextual keyword in a statement, you indicate that the method, operator, or
        // get accessor in which it appears is an iterator. Using yield to define an iterator removes the need for an
        // explicit extra class (the class that holds the state for an enumeration, see IEnumerator<T> for an example)
        // when you implement the IEnumerable and IEnumerator pattern for a custom collection type.
        
        // The following example shows the two forms of the yield statement.
        // yield return <expression>;
        // yield break;
        
        // You use a yield return statement to return each element one at a time.
        
        // The sequence returned from an iterator method can be consumed by using a foreach statement or LINQ query.
        // Each iteration of the foreach loop calls the iterator method.
        // When a yield return statement is reached in the iterator method, expression is returned, and the current location in code is retained.
        // Execution is restarted from that location the next time that the iterator function is called.
        
        // You can use a yield break statement to end the iteration.
        
        // ITERATOR METHODS AND GET ACCESSORS
        //
        // The declaration of an iterator must meet the following requirements:
        //    The return type must be IEnumerable, IEnumerable<T>, IEnumerator, or IEnumerator<T>.
        //    The declaration can't have any in ref or out parameters.
        //
        // The yield type of an iterator that returns IEnumerable or IEnumerator is object.
        // If the iterator returns IEnumerable<T> or IEnumerator<T>, there must be an implicit conversion from
        // the type of the expression in the yield return statement to the generic type parameter .
        //
        // You can't include a yield return or yield break statement in:
        //    Lambda expressions and anonymous methods.
        //    Methods that contain unsafe blocks.

        public static void Example1()
        {
            // Display powers of 2 up to the exponent of 8:
            foreach (int i in Power(2, 8))
            {
                Console.Write("{0} ", i);
            }
        }

        private static IEnumerable<int> Power(int number, int exponent)
        {
            int result = 1;

            for (int i = 0; i < exponent; i++)
            {
                result = result * number;
                yield return result;
            }
        }

        public static IEnumerable<int> Example2()
        {
            try
            {
                // A yield return statement can be located in the try block of a try-finally statement.
                yield return 0;
                yield return 1;
                yield return 2;
            }
            // A yield return statement can't be located in a try-catch block.
            // catch { }
            
            // If the foreach body (outside of the iterator method) throws an exception, a finally block in the iterator method is executed.
            finally
            {
                Console.Out.WriteLine("Finally of IEnumerable");
                // A yield break statement can be located in a try block or a catch block but not a finally block.
                // yield break;
            }
        }

        public static IEnumerable<int> Example3(int start)
        {
            try
            {
                // A yield break statement can be located in a try block or a catch block but not a finally block.
                if (start / 2 <= 0) yield break;
            }
            catch(Exception e)
            {
                Console.Out.WriteLine("Exception " + e.Message);
                // A yield break statement can be located in a try block or a catch block but not a finally block.
                yield break;
            }

            yield return start;
        }
    }
    
    public static class GalaxyClass
    {
        public static void ShowGalaxies()
        {
            var theGalaxies = new Galaxies();
            foreach (Galaxy theGalaxy in theGalaxies.NextGalaxy)
            {
                Debug.WriteLine(theGalaxy.Name + " " + theGalaxy.MegaLightYears.ToString());
            }
        }

        public class Galaxies
        {

            public IEnumerable<Galaxy> NextGalaxy
            {
                get
                {
                    yield return new Galaxy { Name = "Tadpole", MegaLightYears = 400 };
                    yield return new Galaxy { Name = "Pinwheel", MegaLightYears = 25 };
                    yield return new Galaxy { Name = "Milky Way", MegaLightYears = 0 };
                    yield return new Galaxy { Name = "Andromeda", MegaLightYears = 3 };
                }
            }
        }

        public class Galaxy
        {
            public String Name { get; set; }
            public int MegaLightYears { get; set; }
        }
    }
}