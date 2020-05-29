using System;
using System.Collections.Generic;
using System.Linq;

namespace TypesProject
{
    class Program
    {
        static void Main(string[] args)
        {
            // When you declare a variable or constant in a program, you must either specify its type or
            // use the var keyword to let the compiler infer the type.
            int number = 5;
            var value = 5;
            
            // Declaration only:
            float temperature;
            string name;
            Program myClass;

            // Declaration with initializers (four examples):
            char firstLetter = 'C';
            var limit = 3;
            int[] source = { 0, 1, 2, 3, 4, 5 };
            var query = from item in source
                                      where item <= limit
                                      select item;
            
            // After a variable is declared, it cannot be re-declared with a new type, and it cannot be assigned a value
            // that is not compatible with its declared type. However, values can be converted to other types. A type
            // conversion that does not cause data loss is performed automatically by the compiler. A conversion that
            // might cause data loss requires a cast in the source code.
            
            // BUILT-IN TYPES
            
            // C# type keyword	    .NET type
            // bool	                System.Boolean
            // byte	                System.Byte
            // sbyte	            System.SByte
            // char	                System.Char
            // decimal              System.Decimal
            // double	            System.Double
            // float	            System.Single
            // int	                System.Int32
            // uint	                System.UInt32
            // long	                System.Int64
            // ulong	            System.UInt64
            // short	            System.Int16
            // ushort	            System.UInt16
            
            // The following table lists the C# built-in reference types:
            // C# type keyword	    .NET type
            // object	            System.Object
            // string	            System.String
            
            // The void keyword represents the absence of a type. You use it as the return type of a method that doesn't return a value.

            bool valueBool = true;
            Console.Out.WriteLine("valueBool: " + valueBool.ToString());
            Console.Out.WriteLine(bool.TrueString);
            Console.Out.WriteLine(bool.FalseString);
            
            // You use the struct, class, interface, and enum constructs to create your own custom types.
            
            
            // THE COMMON TYPE SYSTEM
            
            // It supports the principle of inheritance. Types can derive from other types, called base types.
            // The derived type inherits (with some restrictions) the methods, properties, and other members of the base type.
            // The base type can in turn derive from some other type, in which case the derived type inherits the members
            // of both base types in its inheritance hierarchy. All types, including built-in numeric types such as
            // System.Int32 (C# keyword: int), derive ultimately from a single base type, which is System.Object (C# keyword: object).
            // This unified type hierarchy is called the Common Type System (CTS).
            //
            // Each type in the CTS is defined as either a value type or a reference type. This includes all custom types in the
            // .NET class library and also your own user-defined types. Types that you define by using the struct keyword are value types;
            // all the built-in numeric types are structs. Types that you define by using the class keyword are reference types.
            // Reference types and value types have different compile-time rules, and different run-time behavior.
            
            // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/types/media/index/value-reference-types-common-type-system.png
            
            // Value types derive from System.ValueType, which derives from System.Object. Types that derive from
            // System.ValueType have special behavior in the CLR. Value type variables directly contain their values,
            // which means that the memory is allocated inline in whatever context the variable is declared.
            // There is no separate heap allocation or garbage collection overhead for value-type variables.
            // There are two categories of value types: struct and enum.
            // The built-in numeric types are structs, and they have fields and methods that you can access:

            // constant field on type byte.
            byte b = byte.MaxValue;
            
            // But you declare and assign values to them as if they were simple non-aggregate types:
            byte num = 0xA;
            int i = 5;
            char c = 'Z';

        }
        
        // You use the struct keyword to create your own custom value types.
        // Typically, a struct is used as a container for a small set of related variables.
        public struct Coords
        {
            public int x, y;

            public Coords(int p1, int p2)
            {
                x = p1;
                y = p2;
            }
        }
        
        // The other category of value types is enum. An enum defines a set of named integral constants.
        // All enums inherit from System.Enum, which inherits from System.ValueType. All the rules that apply to structs also apply to enums.
        public enum FileMode
        {
            CreateNew = 1,
            Create = 2,
            Open = 3,
            OpenOrCreate = 4,
            Truncate = 5,
            Append = 6,
        }
        
        // A type that is defined as a class, delegate, array, or interface is a reference type. At run time, when you
        // declare a variable of a reference type, the variable contains the value null until you explicitly create an
        // object by using the new operator, or assign it an object that has been created elsewhere by using new.
        
        // An interface must be initialized together with a class object that implements it.
        
        // When the object is created, the memory is allocated on the managed heap, and the variable holds only a reference to
        // the location of the object. Types on the managed heap require overhead both when they are allocated and when they are
        // reclaimed by the automatic memory management functionality of the CLR, which is known as garbage collection.
        // However, garbage collection is also highly optimized, and in most scenarios it does not create a performance issue.
        
        // Reference types fully support inheritance. When you create a class, you can inherit from any other interface
        // or class that is not defined as sealed, and other classes can inherit from your class and override your virtual methods.
        
        // In C#, literal values receive a type from the compiler.
        // You can specify how a numeric literal should be typed by appending a letter to the end of the number.
        // For example, to specify that the value 4.56 should be treated as a float, append an "f" or "F" after the number: 4.56f.
        // If no letter is appended, the compiler will infer a type for the literal.
        public static void Example1()
        {
            // Because literals are typed, and all types derive ultimately from System.Object, you can write and compile code such as the following:
            string s = "The answer is " + 5.ToString();
            // Outputs: "The answer is 5"
            Console.WriteLine(s);

            Type type = 12345.GetType();
            // Outputs: "System.Int32"
            Console.WriteLine(type);
            
            // A type can be declared with one or more type parameters that serve as a placeholder for the actual type (the concrete type)
            // that client code will provide when it creates an instance of the type. Such types are called generic types.
            List<string> stringList = new List<string>();
            stringList.Add("String example");
            // compile time error adding a type
            // stringList.Add(4);
            
            
            // As stated previously, you can implicitly type a local variable (but not class members) by using the var keyword.
            // The variable still receives a type at compile time, but the type is provided by the compiler.
            
            // In some cases, it is inconvenient to create a named type for simple sets of related values that you do not
            // intend to store or pass outside method boundaries. You can create anonymous types for this purpose.
            
            // Ordinary value types cannot have a value of null.
            // However, you can create nullable value types by affixing a ? after the type.
            // For example, int? is an int type that can also have the value null.
            // Nullable value types are instances of the generic struct type System.Nullable<T>.
            // Nullable value types are especially useful when you are passing data to and from databases in which numeric values might be null.
            int? nullableInt = null;
            nullableInt = 0;
        }
        
        // CASTING AND TYPE CONVERSIONS
        
        // IMPLICIT CONVERSIONS: No special syntax is required because the conversion is type safe and no data will be lost.
        // Examples include conversions from smaller to larger integral types, and conversions from derived classes to base classes.
        // For reference types, an implicit conversion always exists from a class to any one of its direct or indirect base classes or interfaces.
        // No special syntax is necessary because a derived class always contains all the members of a base class.
        private static void Example2()
        {
            // Implicit conversion. A long can hold any value an int can hold, and more!
            int num = 2147483647;
            long bigNum = num;
            
            Derived d = new Derived();  
            Base b = d; // Always OK.  
        }
        
        // EXPLICIT CONVERSIONS (CASTS): Explicit conversions require a cast expression.
        // Casting is required when information might be lost in the conversion, or when the conversion might not succeed for other reasons.
        // Typical examples include numeric conversion to a type that has less precision or a smaller range,
        // and conversion of a base-class instance to a derived class.
        private static void Example3()
        {
            double x = 1234.7;
            int a;
            // Cast double to int.
            a = (int)x;
            Console.WriteLine(a);
            // Output: 1234
            
            // For reference types, an explicit cast is required if you need to convert from a base type to a derived type:
            // Create a new derived type.  
            Giraffe giraffe = new Giraffe();  
  
            // Implicit conversion to base type is safe.  
            Animal animal = giraffe;  
  
            // Explicit conversion is required to cast back  
            // to derived type. Note: This will compile but will  
            // throw an exception at run time if the right-side  
            // object is not in fact a Giraffe.  
            Giraffe g2 = (Giraffe) animal;  
            
            // In some reference type conversions, the compiler cannot determine whether a cast will be valid.
            // It is possible for a cast operation that compiles correctly to fail at run time.
            Reptile snake = (Reptile) animal;
        
            // C# provides the is operator to enable you to test for compatibility before actually performing a cast.
            if (animal is Giraffe) return;
        }
        
        // USER-DEFINED CONVERSIONS: User-defined conversions are performed by special methods that you can define to enable
        // explicit and implicit conversions between custom types that do not have a base class–derived class relationship.
        // Predefined C# implicit conversions always succeed and never throw an exception.
        // User-defined implicit conversions should behave in that way as well.
        // If a custom conversion can throw an exception or lose information, define it as an explicit conversion.
        public static void Example4()
        {
            var d = new Digit(7);

            byte number = d;
            Console.WriteLine(number);  // output: 7

            Digit digit = (Digit)number;
            Console.WriteLine(digit);  // output: 7
        }
        
        public readonly struct Digit
        {
            private readonly byte _digit;

            public Digit(byte digit)
            {
                if (digit > 9)
                {
                    throw new ArgumentOutOfRangeException(nameof(digit), "Digit cannot be greater than nine.");
                }
                this._digit = digit;
            }

            public static implicit operator byte(Digit d) => d._digit;
            public static explicit operator Digit(byte b) => new Digit(b);

            public override string ToString() => $"{_digit}";
        }
     
        // CONVERSIONS WITH HELPER CLASSES: To convert between non-compatible types, such as integers and System.DateTime objects,
        // or hexadecimal strings and byte arrays, you can use the System.BitConverter class, the System.Convert class,
        // and the Parse methods of the built-in numeric types, such as Int32.Parse.

        
        // BOXING AND UNBOXING
        public static void Example5()
        {
            // Boxing is used to store value types in the garbage-collected heap.
            // Boxing is an implicit conversion of a value type to the type object or to any interface type implemented by this value type.
            // Boxing a value type allocates an object instance on the heap and copies the value into the new object.
            int i = 123;
            // Boxing copies the value of i into object o.
            object obj = i;
            obj = (object)i;  // explicit boxing
            
            // Change the value of i.
            i = 456;

            // The change in i doesn't affect the value stored in o.
            Console.WriteLine("The value-type value = {0}", i); // 456
            Console.WriteLine("The object-type value = {0}", obj); // 123
            
            // The result of this statement is creating an object reference o, on the stack, that references a value of the type int, on the heap.
            // This value is a copy of the value-type value assigned to the variable i.
            // The difference between the two variables, i and o, is illustrated in the following image of boxing conversion:
            // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/types/media/boxing-and-unboxing/unboxing-conversion-operation.gif
            
            // For the unboxing of value types to succeed at run time, the item being unboxed must be
            // a reference to an object that was previously created by boxing an instance of that value type.
            // Attempting to unbox null causes a NullReferenceException.
            // Attempting to unbox a reference to an incompatible value type causes an InvalidCastException.
            int j = (int) obj;   // unboxing
            int k = (short) obj; // Specified cast is not valid.
        }

        public static void Example6()
        {
            // BitConverter Class:  Converts base data types to an array of bytes, and an array of bytes to base data types.
            
            byte[] bytes = { 0, 0, 0, 25 };
            // If the system architecture is little-endian (that is, little end first), reverse the byte array.
            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);

            int i = BitConverter.ToInt32(bytes, 0);
            Console.WriteLine("int: {0}", i);
            // Output: int: 25
            
            byte[] bytes2 = BitConverter.GetBytes(201805978);
            Console.WriteLine("byte array: " + BitConverter.ToString(bytes2));
            // Output: byte array: 9A-50-07-0C
        }
    }

    public class Animal { }
    public class Giraffe : Animal { }
    public class Reptile : Animal { }
    public class Base { }
    public class Derived : Base { }
}