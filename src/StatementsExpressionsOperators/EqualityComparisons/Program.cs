using System;

namespace EqualityComparisons
{
    class Program
    { 
        private static void Main(string[] args)
        {
            // It is sometimes necessary to compare two values for equality. In some cases, you are testing for value equality,
            // also known as equivalence, which means that the values that are contained by the two variables are equal.
            // In other cases, you have to determine whether two variables refer to the same underlying object in memory.
            
            // The concept of reference equality applies only to reference types. Value type objects cannot have reference
            // equality because when an instance of a value type is assigned to a variable, a copy of the value is made.
            // Therefore you can never have two unboxed structs that refer to the same location in memory.
            // Furthermore, if you use ReferenceEquals to compare two value types, the result will always be false,
            // even if the values that are contained in the objects are all identical.
            // This is because each variable is boxed into a separate object instance.
        }
        
        private static void ReferenceEquality()
        {
            Test a = new Test() { Num = 1, Str = "Hi" };
            Test b = new Test() { Num = 1, Str = "Hi" };

            // Reference equality means that two object references refer to the same underlying object.
            bool areEqual = ReferenceEquals(a, b); // False:
            Console.WriteLine("ReferenceEquals(a, b) = {0}", areEqual);

            // Assign b to a.
            b = a;

            // Repeat calls with different results.
            areEqual = ReferenceEquals(a, b); // True:
            Console.WriteLine("ReferenceEquals(a, b) = {0}", areEqual);
        }

        private static void ValueEquality()
        {
            // Value equality means that two objects contain the same value or values.
            // For primitive value types such as int or bool, tests for value equality are straightforward.
            // You can use the == operator.
            
            // For most other types, testing for value equality is more complex because it requires that you understand
            // how the type defines it. For classes and structs that have multiple fields or properties, value equality
            // is often defined to mean that all fields or properties have the same value. For example, two Point objects
            // might be defined to be equivalent if pointA.X is equal to pointB.X and pointA.Y is equal to pointB.Y.

            // However, there is no requirement that equivalence be based on all the fields in a type. It can be based
            // on a subset. When you compare types that you do not own, you should make sure to understand specifically
            // how equivalence is defined for that type
            
            // Equality comparisons of floating-point values (double and float) are problematic
            // because of the imprecision of floating-point arithmetic on binary computers.
            
            int a = new Random().Next();  
            int b = new Random().Next();  
  
            // Test for value equality.
            if (b == a)
            {  
                // The two integers are equal.  
            }  
        }
        
        // HOW TO DEFINE VALUE EQUALITY FOR A TYPE
        
        // When you define a class or struct, you decide whether it makes sense to create a custom definition of value
        // equality (or equivalence) for the type. Typically, you implement value equality when objects of the type are
        // expected to be added to a collection of some sort, or when their primary purpose is to store a set of fields
        // or properties. You can base your definition of value equality on a comparison of all the fields and properties
        // in the type, or you can base the definition on a subset.

        // In either case, and in both classes and structs, your implementation should follow the five guarantees of
        // equivalence (For the following rules, assume that x, y and z are not null):
        //
        //     x.Equals(x) returns true. This is called the reflexive property.
        //
        //     x.Equals(y) returns the same value as y.Equals(x). This is called the symmetric property.
        //
        //     if (x.Equals(y) && y.Equals(z)) returns true, then x.Equals(z) returns true. This is called the transitive property.
        //
        //     Successive invocations of x.Equals(y) return the same value as long as the objects referenced by x and y are not modified.
        //
        //     Any non-null value is not equal to null. However, the CLR checks for null on all method calls and throws
        //     a NullReferenceException if the this reference would be null. Therefore, x.Equals(y) throws an exception
        //     when x is null. That breaks rules 1 or 2, depending on the argument to Equals.
        
        // Any struct that you define already has a default implementation of value equality that it inherits from the
        // System.ValueType override of the Object.Equals(Object) method. This implementation uses reflection to examine
        // all the fields and properties in the type. Although this implementation produces correct results, it is
        // relatively slow compared to a custom implementation that you write specifically for the type.
        
        // The implementation details for value equality are different for classes and structs. However, both classes and
        // structs require the same basic steps for implementing equality:
        //
        //     Override the virtual Object.Equals(Object) method. In most cases, your implementation of bool Equals( object obj )
        //     should just call into the type-specific Equals method that is the implementation of the System.IEquatable<T> interface. (See step 2.)
        //
        //     Implement the System.IEquatable<T> interface by providing a type-specific Equals method. This is where the
        //     actual equivalence comparison is performed. For example, you might decide to define equality by comparing
        //     only one or two fields in your type. Do not throw exceptions from Equals. For classes only: This method
        //     should examine only fields that are declared in the class. It should call base.Equals to examine fields
        //     that are in the base class. (Do not do this if the type inherits directly from Object, because the Object
        //     implementation of Object.Equals(Object) performs a reference equality check.)
        //
        //     Optional but recommended: Overload the == and != operators.
        //
        //     Override Object.GetHashCode so that two objects that have value equality produce the same hash code.
        //
        //     Optional: To support definitions for "greater than" or "less than," implement the IComparable<T> interface
        //     for your type, and also overload the <= and >= operators.
        
        // On classes (reference types), the default implementation of both Object.Equals(Object) methods performs a
        // reference equality comparison, not a value equality check. When an implementer overrides the virtual method,
        // the purpose is to give it value equality semantics.

        // The == and != operators can be used with classes even if the class does not overload them.
        // However, the default behavior is to perform a reference equality check.
        // In a class, if you overload the Equals method, you should overload the == and != operators, but it is not required.
        
        // For structs, the default implementation of Object.Equals(Object) (which is the overridden version in System.ValueType)
        // performs a value equality check by using reflection to compare the values of every field in the type. When an
        // implementer overrides the virtual Equals method in a struct, the purpose is to provide a more efficient means
        // of performing the value equality check and optionally to base the comparison on some subset of the struct's field or properties.

        // The == and != operators cannot operate on a struct unless the struct explicitly overloads them.
        
        // The implementation of Equals in the System.Object universal base class also performs a reference equality check,
        // but it is best not to use this because, if a class happens to override the method, the results might not be what you expect.
        // The same is true for the == and != operators. When they are operating on reference types, the default behavior
        // of == and != is to perform a reference equality check. However, derived classes can overload the operator to
        // perform a value equality check. To minimize the potential for error, it is best to always use ReferenceEquals
        // when you have to determine whether two objects have reference equality.
        
        // Constant strings within the same assembly are always interned by the runtime. That is, only one instance of
        // each unique literal string is maintained. However, the runtime does not guarantee that strings created at
        // runtime are interned, nor does it guarantee that two equal constant strings in different assemblies are interned.

        private static void ValueEqualityRefType()
        {
            ThreeDPoint pointA = new ThreeDPoint(3, 4, 5);
            ThreeDPoint pointB = new ThreeDPoint(3, 4, 5);
            ThreeDPoint pointC = null;
            int i = 5;

            Console.WriteLine("pointA.Equals(pointB) = {0}", pointA.Equals(pointB));
            Console.WriteLine("pointA == pointB = {0}", pointA == pointB);
            Console.WriteLine("null comparison = {0}", pointA.Equals(pointC));
            Console.WriteLine("Compare to some other type = {0}", pointA.Equals(i));

            TwoDPoint pointD = null;
            TwoDPoint pointE = null;

            Console.WriteLine("Two null TwoDPoints are equal: {0}", pointD == pointE);

            pointE = new TwoDPoint(3, 4);
            Console.WriteLine("(pointE == pointA) = {0}", pointE == pointA);
            Console.WriteLine("(pointA == pointE) = {0}", pointA == pointE);
            Console.WriteLine("(pointA != pointE) = {0}", pointA != pointE);

            System.Collections.ArrayList list = new System.Collections.ArrayList();
            list.Add(new ThreeDPoint(3, 4, 5));
            Console.WriteLine("pointE.Equals(list[0]): {0}", pointE.Equals(list[0]));
            /* Output:
                pointA.Equals(pointB) = True
                pointA == pointB = True
                null comparison = False
                Compare to some other type = False
                Two null TwoDPoints are equal: True
                (pointE == pointA) = False
                (pointA == pointE) = False
                (pointA != pointE) = True
                pointE.Equals(list[0]): False
            */
        }
    }

    class Test
    {
        public int Num { get; set; }
        public string Str { get; set; }
    }
    
    class TwoDPoint : IEquatable<TwoDPoint>
    {
        // Readonly auto-implemented properties.
        public int X { get; private set; }
        public int Y { get; private set; }

        // Set the properties in the constructor.
        public TwoDPoint(int x, int y)
        {
            if ((x < 1) || (x > 2000) || (y < 1) || (y > 2000))
            {
                throw new System.ArgumentException("Point must be in range 1 - 2000");
            }
            this.X = x;
            this.Y = y;
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as TwoDPoint);
        }

        public bool Equals(TwoDPoint p)
        {
            // If parameter is null, return false.
            if (Object.ReferenceEquals(p, null))
            {
                return false;
            }

            // Optimization for a common success case.
            if (Object.ReferenceEquals(this, p))
            {
                return true;
            }

            // If run-time types are not exactly the same, return false.
            if (this.GetType() != p.GetType())
            {
                return false;
            }

            // Return true if the fields match.
            // Note that the base class is not invoked because it is
            // System.Object, which defines Equals as reference equality.
            return (X == p.X) && (Y == p.Y);
        }

        public override int GetHashCode()
        {
            return X * 0x00010000 + Y;
        }

        public static bool operator ==(TwoDPoint lhs, TwoDPoint rhs)
        {
            // Check for null on left side.
            if (Object.ReferenceEquals(lhs, null))
            {
                if (Object.ReferenceEquals(rhs, null))
                {
                    // null == null = true.
                    return true;
                }

                // Only the left side is null.
                return false;
            }
            // Equals handles case of null on right side.
            return lhs.Equals(rhs);
        }

        public static bool operator !=(TwoDPoint lhs, TwoDPoint rhs)
        {
            return !(lhs == rhs);
        }
    }

    // For the sake of simplicity, assume a ThreeDPoint IS a TwoDPoint.
    class ThreeDPoint : TwoDPoint, IEquatable<ThreeDPoint>
    {
        public int Z { get; private set; }

        public ThreeDPoint(int x, int y, int z)
            : base(x, y)
        {
            if ((z < 1) || (z > 2000))
            {
                throw new System.ArgumentException("Point must be in range 1 - 2000");
            }
            this.Z = z;
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as ThreeDPoint);
        }

        public bool Equals(ThreeDPoint p)
        {
            // If parameter is null, return false.
            if (Object.ReferenceEquals(p, null))
            {
                return false;
            }

            // Optimization for a common success case.
            if (Object.ReferenceEquals(this, p))
            {
                return true;
            }

            // Check properties that this class declares.
            if (Z == p.Z)
            {
                // Let base class check its own fields
                // and do the run-time type comparison.
                return base.Equals((TwoDPoint)p);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return (X * 0x100000) + (Y * 0x1000) + Z;
        }

        public static bool operator ==(ThreeDPoint lhs, ThreeDPoint rhs)
        {
            // Check for null.
            if (Object.ReferenceEquals(lhs, null))
            {
                if (Object.ReferenceEquals(rhs, null))
                {
                    // null == null = true.
                    return true;
                }

                // Only the left side is null.
                return false;
            }
            // Equals handles the case of null on right side.
            return lhs.Equals(rhs);
        }

        public static bool operator !=(ThreeDPoint lhs, ThreeDPoint rhs)
        {
            return !(lhs == rhs);
        }
    }
}