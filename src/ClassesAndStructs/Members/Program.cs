using System;

namespace Members
{
    // ABSTRACT CLASSES AND CLASS MEMBERS
    
    // An abstract class cannot be instantiated.
    // The purpose of an abstract class is to provide a common definition of a base class that multiple derived classes can share.
    
    // Abstract classes may also define abstract methods.
    // This is accomplished by adding the keyword abstract before the return type of the method.
    public abstract class A
    {
        public abstract void DoWork(int i);
    }
    
    // Abstract methods have no implementation, so the method definition is followed by a semicolon instead of a normal method block.
    // Derived classes of the abstract class must implement all abstract methods.
    // When an abstract class inherits a virtual method from a base class, the abstract class can override the virtual method with an abstract method.
    public class D
    {
        public virtual void DoWork(int i)
        {
            // Original implementation.
        }
    }

    public abstract class E : D
    {
        public abstract override void DoWork(int i);
    }

    public class F : E
    {
        public override void DoWork(int i)
        {
            // New implementation.
        }
    }
    // If a virtual method is declared abstract, it is still virtual to any class inheriting from the abstract class.
    // A class inheriting an abstract method cannot access the original implementation of the method—in the previous example,
    // DoWork on class F cannot call DoWork on class D. In this way, an abstract class can force derived classes to
    // provide new method implementations for virtual methods.
    
    
    // SEALED CLASSES AND CLASS MEMBERS
    
    // A sealed class cannot be used as a base class. For this reason, it cannot also be an abstract class. Sealed classes prevent derivation.
    // Because they can never be used as a base class, some run-time optimizations can make calling sealed class members slightly faster.
    public sealed class C
    {
        // Class members here.
    }
    
    // A method, indexer, property, or event, on a derived class that is overriding a virtual member of the base class
    // can declare that member as sealed. This negates the virtual aspect of the member for any further derived class.
    // This is accomplished by putting the sealed keyword before the override keyword in the class member declaration.
    public class H : D
    {
        public sealed override void DoWork(int i)
        {
        }
    }


    // STATIC CLASSES AND STATIC CLASS MEMBERS

    // A static class is basically the same as a non-static class, but there is one difference: a static class cannot be instantiated.
    // In other words, you cannot use the new operator to create a variable of the class type.
    // Because there is no instance variable, you access the members of a static class by using the class name itself.

    // As is the case with all class types, the type information for a static class is loaded by the .NET Framework common
    // language runtime (CLR) when the program that references the class is loaded. The program cannot specify exactly when
    // the class is loaded. However, it is guaranteed to be loaded and to have its fields initialized and its static
    // constructor called before the class is referenced for the first time in your program. A static constructor is only
    // called one time, and a static class remains in memory for the lifetime of the application domain in which your program resides.

    // The following list provides the main features of a static class:
    //     Contains only static members.
    //     Cannot be instantiated.
    //     Is sealed.
    //     Cannot contain Instance Constructors.

    // Static classes are sealed and therefore cannot be inherited. They cannot inherit from any class except Object.
    // Static classes cannot contain an instance constructor; however, they can contain a static constructor.
    // Non-static classes should also define a static constructor if the class contains static members that require non-trivial initialization.

    // A non-static class can contain static methods, fields, properties, or events. The static member is callable on a
    // class even when no instance of the class has been created. The static member is always accessed by the class name,
    // not the instance name. Only one copy of a static member exists, regardless of how many instances of the class are created.
    // Static methods and properties cannot access non-static fields and events in their containing type, and they cannot
    // access an instance variable of any object unless it is explicitly passed in a method parameter.
    
    // Static methods can be overloaded but not overridden, because they belong to the class, and not to any instance of the class.

    // Although a field cannot be declared as static const, a const field is essentially static in its behavior.
    // It belongs to the type, not to instances of the type. Therefore, const fields can be accessed by using the
    // same ClassName.MemberName notation that is used for static fields. No object instance is required.
    
    // C# does not support static local variables (variables that are declared in method scope).

    // Static members are initialized before the static member is accessed for the first time and before the static constructor, if there is one, is called.
    
    // If your class contains static fields, provide a static constructor that initializes them when the class is loaded.

    
    // ACCESS MODIFIERS
    
    // public:             The type or member can be accessed by any other code in the same assembly or another assembly that references it.
    // private:            The type or member can be accessed only by code in the same class or struct.
    // protected:          The type or member can be accessed only by code in the same class, or in a class that is derived from that class.
    // internal:           The type or member can be accessed by any code in the same assembly, but not from another assembly.
    // protected internal: The type or member can be accessed by any code in the assembly in which it's declared,
    //                         or from within a derived class in another assembly.
    // private protected:  The type or member can be accessed only within its declaring assembly, by code in the same
    //                         class or in a type that is derived from that class.
    
    // Not all access modifiers are valid for all types or members in all contexts.
    // In some cases, the accessibility of a type member is constrained by the accessibility of its containing type.
    
    // Classes and structs declared directly within a namespace (in other words, that aren't nested within other classes
    // or structs) can be either public or internal. Internal is the default if no access modifier is specified.
    
    // Struct members, including nested classes and structs, can be declared public, internal, or private.
    // Class members, including nested classes and structs, can be public, protected internal, protected, internal, private protected, or private.
    // Class and struct members, including nested classes and structs, have private access by default.
    // Private nested types aren't accessible from outside the containing type.

    // Derived classes can't have greater accessibility than their base types. You can't declare a public class B that derives from an internal class A.

    // Class members (including nested classes and structs) can be declared with any of the six types of access.
    // Struct members can't be declared as protected, protected internal, or private protected because structs don't support inheritance.

    // Normally, the accessibility of a member isn't greater than the accessibility of the type that contains it.
    // However, a public member of an internal class might be accessible from outside the assembly if the
    // member implements interface methods or overrides virtual methods that are defined in a public base class.
    
    // The type of any member field, property, or event must be at least as accessible as the member itself. Similarly, the
    // return type and the parameter types of any method, indexer, or delegate must be at least as accessible as the member itself.
    // For example, you can't have a public method M that returns a class C unless C is also public.
    // Likewise, you can't have a protected property of type A if A is declared as private.
    
    // User-defined operators must always be declared as public and static
    
    // Finalizers can't have accessibility modifiers.

    // Interfaces declared directly within a namespace can be public or internal and, just like classes and structs,
    // interfaces default to internal access. Interface members are public by default because the purpose of an interface
    // is to enable other types to access a class or struct. Interface member declarations may include any access modifier.
    // This is most useful for static methods to provide common implementations needed by all implementors of a class.
    
    // Enumeration members are always public, and no access modifiers can be applied.
    
    // Delegates behave like classes and structs.
    // By default, they have internal access when declared directly within a namespace, and private access when nested.
    
    
    // FIELDS
    
    // A field is a variable of any type that is declared directly in a class or struct. Fields are members of their containing type.

    // A class or struct may have instance fields, static fields, or both. 
    
    // Generally, you should use fields only for variables that have private or protected accessibility.
    // Data that your class exposes to client code should be provided through methods, properties, and indexers.
    // By using these constructs for indirect access to internal fields, you can guard against invalid input values.
    // A private field that stores the data exposed by a public property is called a backing store or backing field.
    
    // Fields are initialized immediately before the constructor for the object instance is called.
    // If the constructor assigns the value of a field, it will overwrite any value given during field declaration.
    
    // A field initializer cannot refer to other instance fields.

    // Fields can be marked as public, private, protected, internal, protected internal, or private protected.
    // These access modifiers define how users of the class can access the fields.
    
    // A field can optionally be declared static. This makes the field available to callers at any time, even if no instance of the class exists.
    
    // A field can be declared readonly. A read-only field can only be assigned a value during initialization or in a constructor.
    // A static readonly field is very similar to a constant, except that the C# compiler does not have access to
    // the value of a static read-only field at compile time, only at run time.
    
    
    // CONSTANTS
    
    // Constants are immutable values which are known at compile time and do not change for the life of the program.
    // Constants are declared with the const modifier. Only the C# built-in types (excluding System.Object) may be declared as const.
    // User-defined types, including classes, structs, and arrays, cannot be const.
    // Use the readonly modifier to create a class, struct, or array that is initialized one time at runtime and thereafter cannot be changed.

    // C# does not support const methods, properties, or events.

    // The enum type enables you to define named constants for integral built-in types (for example int, uint, long, and so on).

    // Constants must be initialized as they are declared.
    
    // In fact, when the compiler encounters a constant identifier in C# source code, it substitutes the literal value
    // directly into the intermediate language (IL) code that it produces. Because there is no variable address associated
    // with a constant at run time, const fields cannot be passed by reference and cannot appear as an l-value in an expression.
    
    // Use caution when you refer to constant values defined in other code such as DLLs. If a new version of the DLL defines
    // a new value for the constant, your program will still hold the old literal value until it is recompiled against the new version.
    
    // Use constants to provide meaningful names instead of numeric literals ("magic numbers") for special values.
    
    // To define non-integral constants, one approach is to group them in a single static class named Constants.
    // This will require that all references to the constants be prefaced with the class name.

    
    // PROPERTIES
    
    // An abstract property declaration does not provide an implementation of the property accessors.
    // It declares that the class supports properties, but leaves the accessor implementation to derived classes.
    public abstract class Shape
    {
        // Area is a read-only property - only a get accessor is needed:
        public abstract double Area { get; }
    }
    
    public class Square : Shape
    {
        private int _side;

        public Square(int side)
        {
            _side = side;
        }

        public override double Area
        {
            get => _side * _side;
        }
    }
    
    
    class Program
    {
        // Multiple constants of the same type can be declared at the same time.
        public const int Months = 12, Weeks = 52, Days = 365;

        // The expression that is used to initialize a constant can refer to another constant if it does not create a circular reference.
        public const double DaysPerWeek = (double) Days / (double) Weeks;
        public const double DaysPerMonth = (double) Days / (double) Months;        
        
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}