using System;

namespace Overview
{
    // Classes and structs are two of the basic constructs of the common type system in the .NET Framework.
    // Each is essentially a data structure that encapsulates a set of data and behaviors that belong together as a logical unit.
    // The data and behaviors are the members of the class or struct, and they include its methods, properties, and events and more.
    
    // A class or struct declaration is like a blueprint that is used to create instances or objects at run time.
    // If you define a class or struct called Person, Person is the name of the type.
    // If you declare and initialize a variable p of type Person, p is said to be an object or instance of Person.
    // Multiple instances of the same Person type can be created, and each instance can have different values in its properties and fields.
    
    // A class is a reference type.
    // When an object of the class is created, the variable to which the object is assigned holds only a reference to that memory.
    // When the object reference is assigned to a new variable, the new variable refers to the original object.
    // Changes made through one variable are reflected in the other variable because they both refer to the same data.
    
    // A struct is a value type.
    // When a struct is created, the variable to which the struct is assigned holds the struct's actual data.
    // When the struct is assigned to a new variable, it is copied. The new variable and the original variable therefore
    // contain two separate copies of the same data. Changes made to one copy do not affect the other copy.
    
    // In general, classes are used to model more complex behavior, or data that is intended to be modified after a class object is created.
    // Structs are best suited for small data structures that contain primarily data that is not intended to be modified after the struct is created.
    
    // A type that is defined as a class is a reference type. At run time, when you declare a variable of a reference type,
    // the variable contains the value null until you explicitly create an instance of the class by using the new operator,
    // or assign it an object of a compatible type that may have been created elsewhere.
    
    
    // CLASS INHERITANCE
    
    // Classes fully support inheritance, a fundamental characteristic of object-oriented programming.
    // When you create a class, you can inherit from any other interface or class that is not defined as sealed,
    // and other classes can inherit from your class and override class virtual methods.

    // Inheritance is accomplished by using a derivation, which means a class is declared by using a base class from which it inherits data and behavior.
    // A base class is specified by appending a colon and the name of the base class following the derived class name, like this:
    public class Base { }
    public class Derived : Base { }
    
    // A class in C# can only directly inherit from one base class.
    // However, because a base class may itself inherit from another class, a class may indirectly inherit multiple base classes.
    // Furthermore, a class can directly implement more than one interface.
    
    // The class whose members are inherited is called the base class, and the class that inherits those members is called the derived class.
    // A derived class can have only one direct base class. However, inheritance is transitive.
    // If ClassC is derived from ClassB, and ClassB is derived from ClassA, ClassC inherits the members declared in ClassB and ClassA.
    
    // Structs do not support inheritance, but they can implement interfaces.
    
    // A class can be declared abstract.
    // An abstract class contains abstract methods that have a signature definition but no implementation.
    // Abstract classes cannot be instantiated. They can only be used through derived classes that implement the abstract methods.
    // By contrast, a sealed class does not allow other classes to derive from it.
    
    // It is possible to split the definition of a class, a struct, an interface or a method over two or more source files.
    // Each source file contains a section of the type or method definition, and all parts are combined when the application is compiled.
    
    // When you define a class to derive from another class, the derived class implicitly gains
    // all the members of the base class, except for its constructors and finalizers.
    
    // When a base class declares a method as virtual, a derived class can override the method with its own implementation.
    // If a base class declares a member as abstract, that method must be overridden in any non-abstract class that directly
    // inherits from that class. If a derived class is itself abstract, it inherits abstract members without implementing them.
    
    // You can declare a class as abstract if you want to prevent direct instantiation by using the new operator.
    // An abstract class can be used only if a new class is derived from it.
    // An abstract class can contain one or more method signatures that themselves are declared as abstract.
    // These signatures specify the parameters and return value but have no implementation (method body).
    // An abstract class doesn't have to contain abstract members; however, if a class does contain an abstract member,
    // the class itself must be declared as abstract. Derived classes that aren't abstract themselves must provide the
    // implementation for any abstract methods from an abstract base class.
    
    // An interface is a reference type that defines a set of members.
    // All classes and structs that implement that interface must implement that set of members.
    // An interface may define a default implementation for any or all of these members.
    // A class can implement multiple interfaces even though it can derive from only a single direct base class.
    
    // A class can prevent other classes from inheriting from it, or from any of its members, by declaring itself or the member as sealed.
    
    // A derived class can hide base class members by declaring members with the same name and signature.
    // The new modifier can be used to explicitly indicate that the member isn't intended to be an override of the base member.
    // The use of new isn't required, but a compiler warning will be generated if new isn't used.
    
    
    // STRUCT INSTANCES VS. CLASS INSTANCES
    
    // Because classes are reference types, a variable of a class object holds a reference to the address of the object on the managed heap.
    // If a second object of the same type is assigned to the first object, then both variables refer to the object at that address.
    // The memory that is allocated for a class instance is automatically reclaimed (garbage collected) by the CLR when all references
    // to the object have gone out of scope. It is not possible to deterministically destroy a class object like you can in C++.
    
    // Because structs are value types, a variable of a struct object holds a copy of the entire object.
    // Instances of structs can also be created by using the new operator, but this is not required.
    
    // A type defined within a class, struct, or interface is called a nested type. 
    public class Container1
    {
        class Nested1
        {
            Nested1() { }
        }
    }
    
    // Regardless of whether the outer type is a class, interface, or struct, nested types default to private;
    // they are accessible only from their containing type.
    
    // You can also specify an access modifier to define the accessibility of a nested type, as follows:
    //     Nested types of a class can be public, protected, internal, protected internal, private or private protected.
    //     However, defining a protected, protected internal or private protected nested class inside a sealed class generates compiler warning CS0628
    //     Nested types of a struct can be public, internal, or private.
    
    // The nested, or inner, type can access the containing, or outer, type.
    // To access the containing type, pass it as an argument to the constructor of the nested type.
    public class Container
    {
        public class Nested
        {
            private Container parent;
            public Nested() { }
            public Nested(Container parent) => this.parent = parent;
        }
    }

    // A nested type has access to all of the members that are accessible to its containing type.
    // It can access private and protected members of the containing type, including any inherited protected members.

    class Program
    {
        static void Main(string[] args)
        {
            Container.Nested nest = new Container.Nested();
        }
    }
}
