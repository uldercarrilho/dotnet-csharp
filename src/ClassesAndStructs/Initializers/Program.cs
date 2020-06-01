using System;
using System.Collections.Generic;

namespace Initializers
{
    // C# lets you instantiate an object or collection and perform member assignments in a single statement.

    public class Cat
    {
        // Auto-implemented properties.
        public int Age { get; set; }
        public string Name { get; set; }

        public Cat() { }
        public Cat(string name) => Name = name;
    }
    
    public class Matrix
    {
        private double[,] storage = new double[3, 3];

        public double this[int row, int column]
        {
            // The embedded array will throw out of range exceptions as appropriate.
            get { return storage[row, column]; }
            set { storage[row, column] = value; }
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            // Object initializers let you assign values to any accessible fields or properties of an object at creation
            // time without having to invoke a constructor followed by lines of assignment statements. The object initializer
            // syntax enables you to specify arguments for a constructor or omit the arguments (and parentheses syntax).
            Cat cat = new Cat { Age = 10, Name = "Fluffy" };
            Cat sameCat = new Cat("Fluffy"){ Age = 10 };
            
            // Starting with C# 6, object initializers can set indexers, in addition to assigning fields and properties.
            var identity = new Matrix
            {
                [0, 0] = 1.0,
                [0, 1] = 0.0,
                [0, 2] = 0.0,

                [1, 0] = 0.0,
                [1, 1] = 1.0,
                [1, 2] = 0.0,

                [2, 0] = 0.0,
                [2, 1] = 0.0,
                [2, 2] = 1.0,
            };
            
            // Any accessible indexer that contains an accessible setter can be used as one of the expressions in an
            // object initializer, regardless of the number or types of arguments. The index arguments form the left
            // side of the assignment, and the value is the right side of the expression.
            /*
            var thing = new IndexersExample {
                name = "object one",
                [1] = '1',
                [2] = '4',
                [3] = '9',
                Size = Math.PI,
                ['C',4] = "Middle C"
            }
            */
            
            // Although object initializers can be used in any context, they are especially useful in LINQ query expressions.
            // Query expressions make frequent use of anonymous types, which can only be initialized by using an object
            // initializer, as shown in the following declaration.
            var pet = new { Age = 10, Name = "Fluffy" };  

            // Collection initializers let you specify one or more element initializers when you initialize a collection
            // type that implements IEnumerable and has Add with the appropriate signature as an instance method or an
            // extension method. The element initializers can be a simple value, an expression, or an object initializer.
            // By using a collection initializer, you do not have to specify multiple calls; the compiler adds the calls automatically.
            List<int> digits = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };  
            List<int> digits2 = new List<int> { 0 + 1, 12 % 3, MakeInt() };
            List<Cat> cats = new List<Cat>
            {
                // Note that the individual object initializers are enclosed in braces and separated by commas.
                new Cat{ Name = "Sylvester", Age=8 },
                new Cat{ Name = "Whiskers", Age=2 },
                new Cat{ Name = "Sasha", Age=14 },
                // You can specify null as an element in a collection initializer if the collection's Add method allows it.
                null
            };
            
            // You can specify indexed elements if the collection supports read / write indexing.
            var numbers = new Dictionary<int, string>
            {
                [7] = "seven",
                [9] = "nine",
                [13] = "thirteen"
            };
        }

        private static int MakeInt()
        {
            return 0;
        }
    }
}