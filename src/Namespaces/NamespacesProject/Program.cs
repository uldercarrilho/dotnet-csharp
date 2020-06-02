
// Most C# applications begin with a section of using directives.
// This section lists the namespaces that the application will be using frequently, and saves the programmer
// from specifying a fully qualified name every time that a method that is contained within is used.
using System;

// You can also use the using directive to create an alias for a namespace.
using generics = System.Collections.Generic;

// Namespaces have the following properties:
//     They organize large code projects.
//     They are delimited by using the . operator.
//     The using directive obviates the requirement to specify the name of the namespace for every class.
//     The global namespace is the "root" namespace: global::System will always refer to the .NET System namespace.

namespace NamespacesProject
{
    class Program
    {
        static void Main(string[] args)
        {
            // Namespaces are heavily used in C# programming in two ways. First, .NET uses namespaces to organize its many classes, as follows:
            System.Console.WriteLine("Hello World!");

            // System is a namespace and Console is a class in that namespace.
            // The using keyword can be used so that the complete name is not required.
            Console.WriteLine("Hello World!");

            // The global namespace is the "root" namespace: global::System will always refer to the .NET System namespace.
            global::System.Console.WriteLine("Hello World");
            
            // Use the namespace alias qualifier :: to access the members of the aliased namespace.
            generics::Dictionary<string, int> dict = new generics::Dictionary<string, int>()
            {
                ["A"] = 1,
                ["B"] = 2,
                ["C"] = 3
            };
        }
    }
}

// Second, declaring your own namespaces can help you control the scope of class and method names in larger programming projects.
// Use the namespace keyword to declare a namespace, as in the following example:
namespace SampleNamespace
{
    class SampleClass
    {
        public void SampleMethod()
        {
            System.Console.WriteLine("SampleMethod inside SampleNamespace");
        }
    }
}

// The namespace keyword is used to declare a scope.
// The ability to create scopes within your project helps organize code and lets you create globally-unique types.
// In the following example, a class titled SampleClass is defined in two namespaces, one nested inside the other.
// The . token is used to differentiate which method gets called.
namespace SampleNamespace2
{
    class SampleClass
    {
        public void SampleMethod() => System.Console.WriteLine("SampleMethod inside SampleNamespace");
    }

    // Create a nested namespace, and define another class.
    namespace NestedNamespace
    {
        class SampleClass
        {
            public void SampleMethod() => System.Console.WriteLine("SampleMethod inside NestedNamespace");
        }
    }

    public class TestNamespace2
    {
        public static void Run(string[] args)
        {
            // Displays "SampleMethod inside SampleNamespace."
            SampleClass outer = new SampleClass();
            outer.SampleMethod();

            // Displays "SampleMethod inside SampleNamespace."
            SampleNamespace2.SampleClass outer2 = new SampleNamespace2.SampleClass();
            outer2.SampleMethod();

            // Displays "SampleMethod inside NestedNamespace."
            NestedNamespace.SampleClass inner = new NestedNamespace.SampleClass();
            inner.SampleMethod();
        }
    }
}

// Namespaces and types have unique titles described by fully qualified names that indicate a logical hierarchy.
// For example, the statement A.B implies that A is the name of the namespace or type, and B is nested inside it.
namespace N1     // N1
{
    class C1      // N1.C1
    {
        class C2   // N1.C1.C2
        {
        }
    }
    namespace N2  // N1.N2
    {
        class C2   // N1.N2.C2
        {
        }
    }
}

// Using the previous code segment, you can add a new class member, C3, to the namespace N1.N2 as follows:
namespace N1.N2
{
    class C3   // N1.N2.C3
    {
    }
}

