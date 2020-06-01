using System;

namespace Overview
{
    // It is possible to split the definition of a class, a struct, an interface or a method over two or more source files.
    // Each source file contains a section of the type or method definition, and all parts are combined when the application is compiled.
    
    // There are several situations when splitting a class definition is desirable:
    //     When working on large projects, spreading a class over separate files enables multiple programmers to work on it at the same time.
    //     When working with automatically generated source, code can be added to the class without having to recreate
    //         the source file. Visual Studio uses this approach when it creates Windows Forms, Web service wrapper code,
    //         and so on. You can create code that uses these classes without having to modify the file created by Visual Studio.

    // To split a class definition, use the partial keyword modifier, as shown here:
    public partial class Employee
    {
        public void DoWork()
        {
        }
    }

    public partial class Employee
    {
        public void GoToLunch()
        {
        }
    }
    
    // The partial keyword indicates that other parts of the class, struct, or interface can be defined in the namespace.
    // All the parts must use the partial keyword. All the parts must be available at compile time to form the final type.
    // All the parts must have the same accessibility, such as public, private, and so on.

    // If any part is declared abstract, then the whole type is considered abstract.
    // If any part is declared sealed, then the whole type is considered sealed.
    // If any part declares a base type, then the whole type inherits that class.

    // All the parts that specify a base class must agree, but parts that omit a base class still inherit the base type.
    // Parts can specify different base interfaces, and the final type implements all the interfaces listed by all the partial declarations.
    // Any class, struct, or interface members declared in a partial definition are available to all the other parts.
    // The final type is the combination of all the parts at compile time.

    // The partial modifier is not available on delegate or enumeration declarations.

    class Container2
    {
        partial class Nested
        {
            void Test() { }
        }
        partial class Nested
        {
            void Test2() { }
        }
    }
    
    // At compile time, attributes of partial-type definitions are merged.
    [Serializable]
    partial class Moon { }

    [ObsoleteAttribute]
    partial class Moon { }
    
    // The following are merged from all the partial-type definitions:
    //     XML comments
    //     interfaces
    //     generic-type parameter attributes
    //     class attributes
    //     members
    
    // There are several rules to follow when you are working with partial class definitions:
    //     All partial-type definitions meant to be parts of the same type must be modified with partial.
    //     The partial modifier can only appear immediately before the keywords class, struct, or interface.
    //     All partial-type definitions meant to be parts of the same type must be defined in the same assembly and the
    //         same module (.exe or .dll file). Partial definitions cannot span multiple modules.
    //     The class name and generic-type parameters must match on all partial-type definitions. Generic types can be
    //         partial. Each partial declaration must use the same parameter names in the same order.
    //     The following keywords on a partial-type definition are optional, but if present on one partial-type definition,
    //         cannot conflict with the keywords specified on another partial definition for the same type:
    //         public, private, protected, internal, abstract, sealed, base class, new modifier (nested parts), generic constraints


    public partial class Coords
    {
        private int x;
        private int y;

        public Coords(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    public partial class Coords
    {
        public void PrintCoords()
        {
            Console.WriteLine("Coords: {0},{1}", x, y);
        }
    }

    class TestCoords
    {
        static void Example()
        {
            Coords myCoords = new Coords(10, 15);
            myCoords.PrintCoords();
        }
    }
    
    partial interface ITest
    {
        void Interface_Test();
    }

    partial interface ITest
    {
        void Interface_Test2();
    }

    partial struct S1
    {
        void Struct_Test() { }
    }

    partial struct S1
    {
        void Struct_Test2() { }
    }
    
    
    // PARTIAL METHODS
    
    // A partial class or struct may contain a partial method. One part of the class contains the signature of the method.
    // An optional implementation may be defined in the same part or another part. If the implementation is not supplied,
    // then the method and all calls to the method are removed at compile time.

    // Partial methods enable the implementer of one part of a class to define a method, similar to an event.
    // The implementer of the other part of the class can decide whether to implement the method or not.
    // If the method is not implemented, then the compiler removes the method signature and all calls to the method.
    // The calls to the method, including any results that would occur from evaluation of arguments in the calls, have
    // no effect at run time. Therefore, any code in the partial class can freely use a partial method, even if the
    // implementation is not supplied. No compile-time or run-time errors will result if the method is called but not implemented.

    // Partial methods are especially useful as a way to customize generated code. They allow for a method name and
    // signature to be reserved, so that generated code can call the method but the developer can decide whether to
    // implement the method. Much like partial classes, partial methods enable code created by a code generator and
    // code created by a human developer to work together without run-time costs.

    // A partial method declaration consists of two parts: the definition, and the implementation. These may be in
    // separate parts of a partial class, or in the same part. If there is no implementation declaration, then the
    // compiler optimizes away both the defining declaration and all calls to the method.
    
    public partial class PartialClassesAndMethods
    {
        // Definition in file1.cs
        partial void onNameChanged();

        // Implementation in file2.cs
        partial void onNameChanged()
        {
            // method body
        } 
    }
    
    // Partial method declarations must begin with the contextual keyword partial and the method must return void.

    // Partial methods can have in or ref but not out parameters.

    // Partial methods are implicitly private, and therefore they cannot be virtual.

    // Partial methods cannot be extern, because the presence of the body determines whether they are defining or implementing.

    // Partial methods can have static and unsafe modifiers.

    // Partial methods can be generic. Constraints are put on the defining partial method declaration, and may optionally
    // be repeated on the implementing one. Parameter and type parameter names do not have to be the same in the implementing
    // declaration as in the defining one.

    // You can make a delegate to a partial method that has been defined and implemented, but not to a partial method that has only been defined.
}