using System;
using System.Collections.Generic;
using System.Text;

namespace GenericsProject
{
    // Generics introduce the concept of type parameters to .NET, which make it possible to design classes and methods that
    // defer the specification of one or more types until the class or method is declared and instantiated by client code.
    // For example, by using a generic type parameter T, you can write a single class that other client code can use without
    // incurring the cost or risk of runtime casts or boxing operations, as shown here:
    public class GenericList<T>
    {
        public void Add(T input) { }
    }
    class TestGenericList
    {
        private class ExampleClass { }
        public static void Run()
        {
            // Declare a list of type int.
            GenericList<int> list1 = new GenericList<int>();
            list1.Add(1);

            // Declare a list of type string.
            GenericList<string> list2 = new GenericList<string>();
            list2.Add("");

            // Declare a list of type ExampleClass.
            GenericList<ExampleClass> list3 = new GenericList<ExampleClass>();
            list3.Add(new ExampleClass());
        }
    }
    
    // In a generic type or method definition, a type parameter is a placeholder for a specific type that a client specifies
    // when they create an instance of the generic type. A generic class, such as GenericList<T> listed in Introduction
    // to Generics, cannot be used as-is because it is not really a type; it is more like a blueprint for a type.
    // To use GenericList<T>, client code must declare and instantiate a constructed type by specifying a type argument
    // inside the angle brackets. The type argument for this particular class can be any type recognized by the compiler.
    // Any number of constructed type instances can be created, each one using a different type argument
    
    // Generic classes and methods combine reusability, type safety, and efficiency in a way that their non-generic
    // counterparts cannot. Generics are most frequently used with collections and the methods that operate on them.
    // The System.Collections.Generic namespace contains several generic-based collection classes. The non-generic
    // collections, such as ArrayList are not recommended and are maintained for compatibility purposes.
    
    // Of course, you can also create custom generic types and methods to provide your own generalized solutions and
    // design patterns that are type-safe and efficient.
    
    // Use generic types to maximize code reuse, type safety, and performance.
    // The most common use of generics is to create collection classes.
    // The .NET class library contains several generic collection classes in the System.Collections.Generic namespace.
    // These should be used whenever possible instead of classes such as ArrayList in the System.Collections namespace.
    // You can create your own generic interfaces, classes, methods, events, and delegates.
    // Generic classes may be constrained to enable access to methods on particular data types.
    // Information on the types that are used in a generic data type may be obtained at run-time by using reflection.
    
    class Program
    {
        // Do name generic type parameters with descriptive names, unless a single letter name is
        // completely self explanatory and a descriptive name would not add value.
        public delegate TOutput Converter<TInput, TOutput>(TInput from);
        public class List2<T> { /*...*/ }
        
        // Consider using T as the type parameter name for types with one single letter type parameter.
        public int IComparer<T>() { return 0; }
        public delegate bool Predicate<T>(T item);
        public struct Nullable<T> where T : struct { /*...*/ }
        
        // Do prefix descriptive type parameter names with "T".
        // Consider indicating constraints placed on a type parameter in the name of parameter.
        // For example, a parameter constrained to ISession may be called TSession.
        public interface ISessionChannel<TSession>
        {
            TSession Session { get; }
        }
        
        static void Main(string[] args)
        {
            TestGenericList.Run();
            DelegateConstraint.Run();
            EnumConstraints.Run();
            GenericInterfaces.Run();
        }
    }
    
    // CONSTRAINTS ON TYPE PARAMETERS
    
    // Constraints inform the compiler about the capabilities a type argument must have.
    // Without any constraints, the type argument could be any type.
    // The compiler can only assume the members of System.Object, which is the ultimate base class for any .NET type.
    // If client code uses a type that doesn't satisfy a constraint, the compiler issues an error.
    // Constraints are specified by using the where contextual keyword.
    // The following table lists the seven types of constraints:
    /*
    where T : struct
    The type argument must be a non-nullable value type. 
    Because all value types have an accessible parameterless constructor, the struct constraint implies the new() constraint 
    and can't be combined with the new() constraint. You can't combine the struct constraint with the unmanaged constraint.
    
    where T : class
    The type argument must be a reference type. 
    This constraint applies also to any class, interface, delegate, or array type. 
    In a nullable context in C# 8.0 or later, T must be a non-nullable reference type.
    
    where T : class?
    The type argument must be a reference type, either nullable or non-nullable. 
    This constraint applies also to any class, interface, delegate, or array type.
    
    where T : notnull
    The type argument must be a non-nullable type. 
    The argument can be a non-nullable reference type in C# 8.0 or later, or a non-nullable value type.
    
    where T : unmanaged	
    The type argument must be a non-nullable unmanaged type. 
    The unmanaged constraint implies the struct constraint and can't be combined with either the struct or new() constraints.
    
    where T : new()	
    The type argument must have a public parameterless constructor. 
    When used together with other constraints, the new() constraint must be specified last. 
    The new() constraint can't be combined with the struct and unmanaged constraints.
    
    where T : <base class name>	
    The type argument must be or derive from the specified base class. 
    In a nullable context in C# 8.0 and later, T must be a non-nullable reference type derived from the specified base class.
    
    where T : <base class name>?	
    The type argument must be or derive from the specified base class. 
    In a nullable context in C# 8.0 and later, T may be either a nullable or non-nullable type derived from the specified base class.
    
    where T : <interface name>	
    The type argument must be or implement the specified interface. 
    Multiple interface constraints can be specified. 
    The constraining interface can also be generic. 
    In a nullable context in C# 8.0 and later, T must be a non-nullable type that implements the specified interface.
    
    where T : <interface name>?	
    The type argument must be or implement the specified interface. 
    Multiple interface constraints can be specified. 
    The constraining interface can also be generic. 
    In a nullable context in C# 8.0, T may be a nullable reference type, a non-nullable reference type, or a value type. 
    T may not be a nullable value type.
    
    where T : U	
    The type argument supplied for T must be or derive from the argument supplied for U. 
    In a nullable context, if U is a non-nullable reference type, T must be non-nullable reference type. 
    If U is a nullable reference type, T may be either nullable or non-nullable.
    */
    
    // Constraints specify the capabilities and expectations of a type parameter. Declaring those constraints means you
    // can use the operations and method calls of the constraining type. If your generic class or method uses any operation
    // on the generic members beyond simple assignment or calling any methods not supported by System.Object, you'll have
    // to apply constraints to the type parameter. For example, the base class constraint tells the compiler that only
    // objects of this type or derived from this type will be used as type arguments. Once the compiler has this guarantee,
    // it can allow methods of that type to be called in the generic class.
    
    public class Employee
    {
        public Employee(string s, int i) => (Name, ID) = (s, i);
        public string Name { get; set; }
        public int ID { get; set; }
    }

    public class GenericList2<T> where T : Employee
    {
        private class Node
        {
            public Node(T t) => (Next, Data) = (null, t);

            public Node Next { get; set; }
            public T Data { get; set; }
        }

        private Node head;

        public void AddHead(T t)
        {
            Node n = new Node(t) { Next = head };
            head = n;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node current = head;

            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }

        public T FindFirstOccurrence(string s)
        {
            Node current = head;
            T t = null;

            while (current != null)
            {
                //The constraint enables access to the Name property.
                if (current.Data.Name == s)
                {
                    t = current.Data;
                    break;
                }
                else
                {
                    current = current.Next;
                }
            }
            return t;
        }
    }
    
    internal interface IEmployee
    {
    }

    // Multiple constraints can be applied to the same type parameter, and the constraints themselves can be generic types, as follows:
    class EmployeeList<T> 
        where T : Employee, IEmployee, IComparable<T>, new()
    {
        // When applying the where T : class constraint, avoid the == and != operators on the type parameter because
        // these operators will test for reference identity only, not for value equality. This behavior occurs even if
        // these operators are overloaded in a type that is used as an argument. The following code illustrates this point;
        // the output is false even though the String class overloads the == operator.
        public static void OpEqualsTest<T>(T s, T t) where T : class
        {
            Console.WriteLine(s == t);
        }
        private static void TestStringEquality()
        {
            string s1 = "target";
            StringBuilder sb = new StringBuilder("target");
            string s2 = sb.ToString();
            OpEqualsTest<string>(s1, s2);
        }
        
        // The compiler only knows that T is a reference type at compile time and must use the default operators that are
        // valid for all reference types. If you must test for value equality, the recommended way is to also apply the
        // where T : IEquatable<T> or where T : IComparable<T> constraint and implement the interface in any class that
        // will be used to construct the generic class.
    }
    
    // You can apply constraints to multiple parameters, and multiple constraints to a single parameter.
    class Base { }
    class Test<T, U>
        where U : struct
        where T : Base, new()
    { }

    // Type parameters that have no constraints, such as T in public class SampleClass<T>{}, are called unbounded type parameters.
    // Unbounded type parameters have the following rules:
    // The != and == operators can't be used because there's no guarantee that the concrete type argument will support these operators.
    // They can be converted to and from System.Object or explicitly converted to any interface type.
    // You can compare them to null.
    // If an unbounded parameter is compared to null, the comparison will always return false if the type argument is a value type.
    
    // TYPE PARAMETERS AS CONSTRAINTS
    
    // The use of a generic type parameter as a constraint is useful when a member function with its own type parameter
    // has to constrain that parameter to the type parameter of the containing type.
    public class List3<T>
    {
        public void Add<U>(List<U> items) where U : T {/*...*/}
    }
    
    // In the previous example, T is a type constraint in the context of the Add method, and an unbounded type parameter
    // in the context of the List class. Type parameters can also be used as constraints in generic class definitions.
    // The type parameter must be declared within the angle brackets together with any other type parameters:
    //Type parameter V is used as a type constraint.
    public class SampleClass<T, U, V> where T : V { }

    // The usefulness of type parameters as constraints with generic classes is limited because the compiler can assume
    // nothing about the type parameter except that it derives from System.Object. Use type parameters as constraints on
    // generic classes in scenarios in which you want to enforce an inheritance relationship between two type parameters.
    
    // NOTNULL CONSTRAINT
    
    // Beginning with C# 8.0 in a nullable context, you can use the notnull constraint to specify that the type argument
    // must be a non-nullable value type or non-nullable reference type. The notnull constraint can only be used in a nullable
    // enable context. The compiler generates a warning if you add the notnull constraint in a nullable oblivious context.
    
    // Unlike other constraints, when a type argument violates the notnull constraint, the compiler generates a warning
    // when that code is compiled in a nullable enable context. If the code is compiled in a nullable oblivious context,
    // the compiler doesn't generate any warnings or errors.
    
    // Beginning with C# 8.0 in a nullable context, the class constraint specifies that the type argument must be a non-nullable
    // reference type. In a nullable context, when a type parameter is a nullable reference type, the compiler generates a warning.

    public static class UnmanagedConstraint
    {
        // Beginning with C# 7.3, you can use the unmanaged constraint to specify that the type parameter must be a non-nullable unmanaged type.
        // The unmanaged constraint enables you to write reusable routines to work with types that can be manipulated as blocks of memory.
        public static unsafe byte[] ToByteArray<T>(this T argument) where T : unmanaged
        {
            var size = sizeof(T);
            var result = new Byte[size];
            Byte* p = (byte*)&argument;
            for (var i = 0; i < size; i++)
                result[i] = *p++;
            return result;
        }
        
        // The preceding method must be compiled in an unsafe context because it uses the sizeof operator on a type not
        // known to be a built-in type. Without the unmanaged constraint, the sizeof operator is unavailable.

        // The unmanaged constraint implies the struct constraint and can't be combined with it. Because the struct constraint
        // implies the new() constraint, the unmanaged constraint can't be combined with the new() constraint as well.
    }

    public static class DelegateConstraint
    {
        // Also beginning with C# 7.3, you can use System.Delegate or System.MulticastDelegate as a base class constraint.
        // The CLR always allowed this constraint, but the C# language disallowed it.
        // The System.Delegate constraint enables you to write code that works with delegates in a type-safe manner.
        // The following code defines an extension method that combines two delegates provided they're the same type:
        public static TDelegate TypeSafeCombine<TDelegate>(this TDelegate source, TDelegate target)
            where TDelegate : System.Delegate
            => Delegate.Combine(source, target) as TDelegate;

        public static void Run()
        {
            // You can use the above method to combine delegates that are the same type:
            Action first = () => Console.WriteLine("this");
            Action second = () => Console.WriteLine("that");

            var combined = first.TypeSafeCombine(second);
            combined();

            Func<bool> test = () => true;
            // Combine signature ensures combined delegates must have the same type.
            // var badCombined = first.TypeSafeCombine(test);
            // If you uncomment the last line, it won't compile. Both first and test are delegate types, but they're different delegate types.
        }
    }

    public class EnumConstraints
    {
        enum Rainbow { Red, Orange, Yellow, Green, Blue, Indigo, Violet }
        
        // Beginning in C# 7.3, you can also specify the System.Enum type as a base class constraint.
        // The CLR always allowed this constraint, but the C# language disallowed it.
        // Generics using System.Enum provide type-safe programming to cache results from using the static methods in System.Enum.
        public static Dictionary<int, string> EnumNamedValues<T>() where T : System.Enum
        {
            var result = new Dictionary<int, string>();
            var values = Enum.GetValues(typeof(T));

            foreach (int item in values)
                result.Add(item, Enum.GetName(typeof(T), item));
            return result;
        }

        public static void Run()
        {
            var map = EnumNamedValues<Rainbow>();

            foreach (var pair in map)
                Console.WriteLine($"{pair.Key}:\t{pair.Value}");            
        }
    }
    
    
    // GENERIC CLASSES
    
    // Generic classes encapsulate operations that are not specific to a particular data type. The most common use for generic
    // classes is with collections like linked lists, hash tables, stacks, queues, trees, and so on. Operations such as adding
    // and removing items from the collection are performed in basically the same way regardless of the type of data being stored.

    // For most scenarios that require collection classes, the recommended approach is to use the ones provided in the .NET class library.
    
    // Typically, you create generic classes by starting with an existing concrete class, and changing types into type
    // parameters one at a time until you reach the optimal balance of generalization and usability.
    
    // The rules for type parameters and constraints have several implications for generic class behavior, especially
    // regarding inheritance and member accessibility. Before proceeding, you should understand some terms.
    // For a generic class Node<T>, client code can reference the class either by specifying a type argument,
    // to create a closed constructed type (Node<int>). Alternatively, it can leave the type parameter unspecified,
    // for example when you specify a generic base class, to create an open constructed type (Node<T>).
    // Generic classes can inherit from concrete, closed constructed, or open constructed base classes:
    class BaseNode { }
    class BaseNodeGeneric<T> { }
    class NodeConcrete<T> : BaseNode { }
    class NodeClosed<T> : BaseNodeGeneric<int> { }
    class NodeOpen<T> : BaseNodeGeneric<T> { }
    
    // Non-generic, in other words, concrete, classes can inherit from closed constructed base classes, but not from open
    // constructed classes or from type parameters because there is no way at run time for client code to supply the type
    // argument required to instantiate the base class.
    class Node1 : BaseNodeGeneric<int> { }

    //Generates an error
    //class Node2 : BaseNodeGeneric<T> {}

    //Generates an error
    //class Node3 : T {}

    
    // Generic classes that inherit from open constructed types must supply type arguments for any base class type
    // parameters that are not shared by the inheriting class, as demonstrated in the following code:
    class BaseNodeMultiple<T, U> { }
    class Node4<T> : BaseNodeMultiple<T, int> { }
    class Node5<T, U> : BaseNodeMultiple<T, U> { }
    //Generates an error
    //class Node6<T> : BaseNodeMultiple<T, U> {}
    
    // Generic classes that inherit from open constructed types must specify constraints
    // that are a superset of, or imply, the constraints on the base type:
    class NodeItem<T> where T : System.IComparable<T>, new() { }
    class SpecialNodeItem<T> : NodeItem<T> where T : System.IComparable<T>, new() { }
    
    // Generic types can use multiple type parameters and constraints, as follows:
    class SuperKeyType<K, V, U>
        where U : System.IComparable<U>
        where V : new()
    {
        // Open constructed and closed constructed types can be used as method parameters:
        void Swap<T>(List<T> list1, List<T> list2)
        {
            //code to swap items
        }

        void Swap(List<int> list1, List<int> list2)
        {
            //code to swap items
        }
    }
    
    // If a generic class implements an interface, all instances of that class can be cast to that interface.

    // Generic classes are invariant. In other words, if an input parameter specifies a List<BaseClass>,
    // you will get a compile-time error if you try to provide a List<DerivedClass>.
    
    
    // GENERICS IN THE RUN TIME
    
    // When a generic type or method is compiled into Microsoft intermediate language (MSIL), it contains metadata that
    // identifies it as having type parameters. How the MSIL for a generic type is used differs based on whether the
    // supplied type parameter is a value type or reference type.

    // When a generic type is first constructed with a value type as a parameter, the runtime creates a specialized
    // generic type with the supplied parameter or parameters substituted in the appropriate locations in the MSIL.
    // Specialized generic types are created one time for each unique value type that is used as a parameter.
    
    // For example, suppose your program code declared a stack that is constructed of integers:
    // Stack<int> stack;
    
    // At this point, the runtime generates a specialized version of the Stack<T> class that has the integer substituted
    // appropriately for its parameter. Now, whenever your program code uses a stack of integers, the runtime reuses the
    // generated specialized Stack<T> class.
    
    // However, suppose that another Stack<T> class with a different value type such as a long or a user-defined structure
    // as its parameter is created at another point in your code. As a result, the runtime generates another version of
    // the generic type and substitutes a long in the appropriate locations in MSIL. Conversions are no longer necessary
    // because each specialized generic class natively contains the value type.

    // Generics work somewhat differently for reference types. The first time a generic type is constructed with any reference
    // type, the runtime creates a specialized generic type with object references substituted for the parameters in the MSIL.
    // Then, every time that a constructed type is instantiated with a reference type as its parameter, regardless of what
    // type it is, the runtime reuses the previously created specialized version of the generic type. This is possible
    // because all references are the same size.
    
    // For example, suppose you had two reference types, a Customer class and an Order class,
    // and also suppose that you created a stack of Customer types:
    class Customer { }
    class Order { }
    // Stack<Customer> customers;

    // At this point, the runtime generates a specialized version of the Stack<T> class
    // that stores object references that will be filled in later instead of storing data.
    // Suppose the next line of code creates a stack of another reference type, which is named Order:
    // Stack<Order> orders = new Stack<Order>();

    // Unlike with value types, another specialized version of the Stack<T> class is not created for the Order type.
    // Instead, an instance of the specialized version of the Stack<T> class is created and the orders variable is set to reference it.
    // Suppose that you then encountered a line of code to create a stack of a Customer type:
    // customers = new Stack<Customer>();

    // As with the previous use of the Stack<T> class created by using the Order type, another instance of the specialized
    // Stack<T> class is created. The pointers that are contained therein are set to reference an area of memory the size
    // of a Customer type. Because the number of reference types can vary wildly from program to program, the C# implementation
    // of generics greatly reduces the amount of code by reducing to one the number of specialized classes created by the
    // compiler for generic classes of reference types.
    
    // Moreover, when a generic C# class is instantiated by using a value type or reference type parameter,
    // reflection can query it at runtime and both its actual type and its type parameter can be ascertained.
    
    
    // GENERICS AND REFLECTION
    
    // Because the Common Language Runtime (CLR) has access to generic type information at run time,
    // you can use reflection to obtain information about generic types in the same way as for non-generic types.

    // In .NET Framework 2.0, several new members were added to the Type class to enable run-time information for generic types.
    
    // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/generics/generics-and-reflection
    
    
    // GENERICS AND ATTRIBUTES
    
    // Attributes can be applied to generic types in the same way as non-generic types.
    
    // Custom attributes are only permitted to reference open generic types, which are generic types for which no type
    // arguments are supplied, and closed constructed generic types, which supply arguments for all type parameters.
    
    class CustomAttribute : Attribute
    {
        public Object info;
    }
    
    public class GenericClass1<T> { }
    public class GenericClass2<T, U> { }
    public class GenericClass3<T, U, V> { }

    // An attribute can reference an open generic type:
    [CustomAttribute(info = typeof(GenericClass1<>))]
    class ClassA { }
    
    // Specify multiple type parameters using the appropriate number of commas.
    [CustomAttribute(info = typeof(GenericClass2<,>))]
    class ClassB { }
    
    // An attribute can reference a closed constructed generic type:
    [CustomAttribute(info = typeof(GenericClass3<int, double, string>))]
    class ClassC { }

    // An attribute that references a generic type parameter will cause a compile-time error:
    // [CustomAttribute(info = typeof(GenericClass3<int, T, string>))]  //Error
    class ClassD<T> { }

    // To obtain information about a generic type or type parameter at run time, you can use the methods of System.Reflection. 
}