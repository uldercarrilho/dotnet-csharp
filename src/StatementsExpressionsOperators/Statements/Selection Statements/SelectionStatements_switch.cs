using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Statements.Selection_Statements
{
    public class SelectionStatements_switch
    {
        public enum Color { Red, Green, Blue }
        
        private static void Example1()
        {
            // In C# 6 and earlier, the match expression must be an expression that returns a value of the following types:
            //    a char.
            //    a string.
            //    a bool.
            //    an integral value, such as an int or a long.
            //    an enum value.
            // Starting with C# 7.0, the match expression can be any non-null expression.
            
            Color c = (Color) (new Random()).Next(0, 3);
            switch (c)
            {
                case Color.Red:
                    Console.WriteLine("The color is red");
                    break;
                case Color.Green:
                    Console.WriteLine("The color is green");
                    break;
                case Color.Blue:
                    Console.WriteLine("The color is blue");
                    break;
                default:
                    Console.WriteLine("The color is unknown.");
                    break;
            }
        }

        private static void Example2()
        {
            Random rnd = new Random();
            int caseSwitch = rnd.Next(1,4);

            // A switch statement can include any number of switch sections, and each section can have one or more case labels.
            // However, no two case labels may contain the same expression.
            switch (caseSwitch)
            {
                case 1:
                    Console.WriteLine("Case 1");
                    break;
                case 2:
                case 3:
                    Console.WriteLine($"Case {caseSwitch}");
                    break;
                default:
                    Console.WriteLine($"An unexpected value ({caseSwitch})");
                    break;
            }
        }

        private static void Example3()
        {
            // Only one switch section in a switch statement executes.
            // C# doesn't allow execution to continue from one switch section to the next.
            // Because of this, the following code generates a compiler error,
            // CS0163: "Control cannot fall through from one case label (<case label>) to another."
            
            /*
            switch (caseSwitch)
            {
                // The following switch section causes an error.
                case 1:
                    Console.WriteLine("Case 1...");
                // Add a break or other jump statement here.
                case 2:
                    Console.WriteLine("... and/or Case 2");
                    break;
            }
            */
        }

        private static void Example4()
        {
            Random rnd = new Random();
            int caseSwitch = rnd.Next(1,4);

            // This requirement is usually met by explicitly exiting the switch section by using a break, goto, or return statement.
            // However, the following code is also valid, because it ensures that program control can't fall through to the default switch section.
            switch (caseSwitch)
            {
                case 1:
                    Console.WriteLine("Case 1...");
                    break;
                case 2:
                case 3:
                    Console.WriteLine("... and/or Case 2");
                    break;
                case 4:
                    while (true)
                        Console.WriteLine("Endless looping. . . .");
                default:
                    Console.WriteLine("Default value...");
                    break;
            }
        }
        
        // Execution of the statement list in the switch section with a case label that matches the match expression
        // begins with the first statement and proceeds through the statement list, typically until a jump statement,
        // such as a break, goto case, goto label, return, or throw, is reached. At that point, control is transferred
        // outside the switch statement or to another case label. A goto statement, if it's used, must transfer control
        // to a constant label. This restriction is necessary, since attempting to transfer control to a non-constant
        // label can have undesirable side-effects, such transferring control to an unintended location in code or
        // creating an endless loop.
        
        // Because C# 6 supports only the constant pattern and doesn't allow the repetition of constant values,
        // case labels define mutually exclusive values, and only one pattern can match the match expression.
        // As a result, the order in which case statements appear is unimportant.

        // In C# 7.0, however, because other patterns are supported, case labels need not define mutually exclusive values,
        // and multiple patterns can match the match expression. Because only the statements in the first switch section
        // that contains the matching pattern are executed, the order in which case statements appear is now important.
        // If C# detects a switch section whose case statement or statements are equivalent to or are subsets of previous
        // statements, it generates a compiler error, CS8120, "The switch case has already been handled by a previous case."
        private static int DiceSum(IEnumerable<object> values)
        {
            var sum = 0;
            foreach (var item in values)
            {
                switch (item)
                {
                    // A single zero value.
                    case 0:
                        break;
                    
                    // case type varname
                    //
                    // where type is the name of the type to which the result of expr is to be converted, and varname is
                    // the object to which the result of expr is converted if the match succeeds. The compile-time type
                    // of expr may be a generic type parameter, starting with C# 7.1.
                    // 
                    // The case expression is true if any of the following is true:
                    //    expr is an instance of the same type as type.
                    //
                    //    expr is an instance of a type that derives from type.
                    //     In other words, the result of expr can be upcast to an instance of type.
                    //
                    //    expr has a compile-time type that is a base class of type, and expr has a runtime type that is
                    //     type or is derived from type. The compile-time type of a variable is the variable's type as
                    //     defined in its type declaration. The runtime type of a variable is the type of the instance
                    //     that is assigned to that variable.
                    //
                    //    expr is an instance of a type that implements the type interface.
                    //
                    // If the case expression is true, varname is definitely assigned and has local scope within the switch section only.
                    
                    // A single value.
                    case int val:
                        sum += val;
                        break;
                    
                    // The default case specifies the switch section to execute if the match expression doesn't match
                    // any other case label. If a default case is not present and the match expression doesn't match any
                    // other case label, program flow falls through the switch statement.
                    // 
                    // The default case can appear in any order in the switch statement.
                    // Regardless of its order in the source code, it's always evaluated last, after all case labels have been evaluated.
                    default:
                        throw new InvalidOperationException("unknown item type");

                    // A non-empty collection.
                    case IEnumerable<object> subList when subList.Any():
                        sum += DiceSum(subList);
                        break;
                    // An empty collection.
                    case IEnumerable<object> subList:
                        break;
                    
                    //  A null reference.
                    // Note that null doesn't match a type. To match a null, you use the following case label:
                    case null:
                        break;
                }
            }
            return sum;
        }

        private static void GenericMethodExample()
        {
            int[] values = { 2, 4, 6, 8, 10 };
            ShowCollectionInformation(values);

            var names = new List<string>();
            names.AddRange(new string[] { "Adam", "Abigail", "Bertrand", "Bridgette" });
            ShowCollectionInformation(names);

            List<int> numbers = null;
            ShowCollectionInformation(numbers);
        }

        private static void ShowCollectionInformation(object coll)
        {
            switch (coll)
            {
                case Array arr:
                    Console.WriteLine($"An array with {arr.Length} elements.");
                    break;
                case IEnumerable<int> ieInt:
                    Console.WriteLine($"Average: {ieInt.Average(s => s)}");
                    break;
                case IList list:
                    Console.WriteLine($"{list.Count} items");
                    break;
                case IEnumerable ie:
                    string result = "";
                    foreach (var e in ie)
                        result += $"{e} ";
                    Console.WriteLine(result);
                    break;
                case null:
                    // Do nothing for a null.
                    break;
                default:
                    Console.WriteLine($"A instance of type {coll.GetType().Name}");
                    break;
            }
        }

        // Instead of object, you could make a generic method, using the type of the collection as the type parameter:
        // The generic version is different than the first sample in two ways. First, you can't use the null case.
        // You can't use any constant case because the compiler can't convert any arbitrary type T to any type other than object.
        // What had been the default case now tests for a non-null object. That means the default case tests only for null.
        private static void ShowCollectionInformation<T>(T coll)
        {
            switch (coll)
            {
                case Array arr:
                    Console.WriteLine($"An array with {arr.Length} elements.");
                    break;
                case IEnumerable<int> ieInt:
                    Console.WriteLine($"Average: {ieInt.Average(s => s)}");
                    break;
                case IList list:
                    Console.WriteLine($"{list.Count} items");
                    break;
                case IEnumerable ie:
                    string result = "";
                    foreach (var e in ie)
                        result += $"{e} ";
                    Console.WriteLine(result);
                    break;
                case object o:
                    Console.WriteLine($"A instance of type {o.GetType().Name}");
                    break;
                default:
                    Console.WriteLine("Null passed to this method.");
                    break;
            }
        }
        
        private static void ShowShapeInfo(Shape sh)
        {
            // Starting with C# 7.0, because case statements need not be mutually exclusive, you can add a when clause to
            // specify an additional condition that must be satisfied for the case statement to evaluate to true.
            // The when clause can be any expression that returns a Boolean value.
            switch (sh)
            {
                // Note that this code never evaluates to true.
                // The correct type pattern to test for a null is case null:.
                case Shape shape when shape == null:
                    Console.WriteLine($"An uninitialized shape (shape == null)");
                    break;
                case null:
                    Console.WriteLine($"An uninitialized shape");
                    break;
                case Shape shape when sh.Area == 0:
                    Console.WriteLine($"The shape: {sh.GetType().Name} with no dimensions");
                    break;
                case Square sq when sh.Area > 0:
                    Console.WriteLine("Information about square:");
                    Console.WriteLine($"   Length of a side: {sq.Side}");
                    Console.WriteLine($"   Area: {sq.Area}");
                    break;
                case Rectangle r when r.Length == r.Width && r.Area > 0:
                    Console.WriteLine("Information about square rectangle:");
                    Console.WriteLine($"   Length of a side: {r.Length}");
                    Console.WriteLine($"   Area: {r.Area}");
                    break;
                case Rectangle r when sh.Area > 0:
                    Console.WriteLine("Information about rectangle:");
                    Console.WriteLine($"   Dimensions: {r.Length} x {r.Width}");
                    Console.WriteLine($"   Area: {r.Area}");
                    break;
                case Shape shape when sh != null:
                    Console.WriteLine($"A {sh.GetType().Name} shape");
                    break;
                default:
                    Console.WriteLine($"The {nameof(sh)} variable does not represent a Shape.");
                    break;
            }
        }
    }
}