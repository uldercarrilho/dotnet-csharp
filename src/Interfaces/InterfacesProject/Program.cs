using System;

namespace InterfacesProject
{
    // An interface contains definitions for a group of related functionalities that a non-abstract class or a struct
    // must implement. An interface may define static methods, which must have an implementation. Beginning with C# 8.0,
    // an interface may define a default implementation for members. An interface may not declare instance data such as
    // fields, auto-implemented properties, or property-like events.

    // By using interfaces, you can, for example, include behavior from multiple sources in a class. That capability is
    // important in C# because the language doesn't support multiple inheritance of classes. In addition, you must use an
    // interface if you want to simulate inheritance for structs, because they can't actually inherit from another struct or class.

    interface IEquatable<T>
    {
        bool Equals(T obj);
    }

    // The name of an interface must be a valid C# identifier name. By convention, interface names begin with a capital I.

    // A class or struct can implement multiple interfaces, but a class can only inherit from a single class.
    
    // Interfaces can contain instance methods, properties, events, indexers, or any combination of those four member types.
    // Interfaces may contain static constructors, fields, constants, or operators.
    
    // Interface members are public by default.
    
    // To implement an interface member, the corresponding member of the implementing class must be public, non-static,
    // and have the same name and signature as the interface member.
    
    // When a class or struct implements an interface, the class or struct must provide an implementation for all of the
    // members that the interface declares but doesn't provide a default implementation for. However, if a base class
    // implements an interface, any class that's derived from the base class inherits that implementation.
    
    public class Car : IEquatable<Car>
    {
        public string Make {get; set;}
        public string Model { get; set; }
        public string Year { get; set; }

        // Implementation of IEquatable<T> interface
        public bool Equals(Car car)
        {
            return (this.Make, this.Model, this.Year) ==
                   (car.Make, car.Model, car.Year);
        }
    }
    
    // Properties and indexers of a class can define extra accessors for a property or indexer that's defined in an interface.
    // For example, an interface might declare a property that has a get accessor.
    // The class that implements the interface can declare the same property with both a get and set accessor.
    // However, if the property or indexer uses explicit implementation, the accessors must match.
    
    // Interfaces can inherit from one or more interfaces. The derived interface inherits the members from its base interfaces.
    // A class that implements a derived interface must implement all members in the derived interface, including all members
    // of the derived interface's base interfaces. That class may be implicitly converted to the derived interface or any
    // of its base interfaces. A class might include an interface multiple times through base classes that it inherits or
    // through interfaces that other interfaces inherit. However, the class can provide an implementation of an interface
    // only one time and only if the class declares the interface as part of the definition of the class
    // (class ClassName : InterfaceName). If the interface is inherited because you inherited a base class that implements
    // the interface, the base class provides the implementation of the members of the interface. However, the derived
    // class can reimplement any virtual interface members instead of using the inherited implementation. When interfaces
    // declare a default implementation of a method, any class implementing that interface inherits that implementation.
    // Implementations defined in interfaces are virtual and the implementing class may override that implementation.

    // A base class can also implement interface members by using virtual members. In that case, a derived class can
    // change the interface behavior by overriding the virtual members.
    
    // An interface is typically like an abstract base class with only abstract members.
    // Any class or struct that implements the interface must implement all its members.
    // Optionally, an interface may define default implementations for some or all of its members.
    // An interface can't be instantiated directly. Its members are implemented by any class or struct that implements the interface.
    // A class or struct can implement multiple interfaces. A class can inherit a base class and also implement one or more interfaces.
    
    // If a class implements two interfaces that contain a member with the same signature, then implementing that member
    // on the class will cause both interfaces to use that member as their implementation.
    public interface IControl
    {
        void Paint();
    }
    public interface ISurface
    {
        void Paint();
    }
    public class SampleClass : IControl, ISurface
    {
        // Both ISurface.Paint and IControl.Paint call this method.
        public void Paint() => Console.WriteLine("Paint method in SampleClass");
    }
    
    // When two interface members don't perform the same function, it leads to an incorrect implementation of one or both of
    // the interfaces. It's possible to implement an interface member explicitly—creating a class member that is only called
    // through the interface, and is specific to that interface. Name the class member with the name of the interface and a period.
    public class SampleClass2 : IControl, ISurface
    {
        void IControl.Paint() => Console.WriteLine("IControl.Paint");
        void ISurface.Paint() => Console.WriteLine("ISurface.Paint");
    }
    
    class Program
    {
        static void Example1()
        {
            // The class member IControl.Paint is only available through the IControl interface, and ISurface.Paint is only
            // available through ISurface. Both method implementations are separate, and neither are available directly on the class.
            SampleClass2 obj = new SampleClass2();
            //obj.Paint();  // Compiler error.

            IControl c = obj;
            c.Paint();  // Calls IControl.Paint on SampleClass.

            ISurface s = obj;
            s.Paint(); // Calls ISurface.Paint on SampleClass.
        }

        static void Example2()
        {
            var sample = new SampleClass3();
            // Notice that the following lines are commented out because they would produce compilation errors.
            // An interface member that is explicitly implemented cannot be accessed from a class instance
            //sample.Paint();// "Paint" isn't accessible.

            // Notice also that the following lines successfully execute because the methods are being called from an instance of the interface
            var control = sample as IControl3;
            control.Paint();
        }
    }

    // Beginning with C# 8.0, you can define an implementation for members declared in an interface. If a class inherits
    // a method implementation from an interface, that method is only accessible through a reference of the interface type.
    // The inherited member doesn't appear as part of the public interface.
    public interface IControl3
    {
        void Paint() => Console.WriteLine("Default Paint method");
    }
    // Any class that implements the IControl interface can override the default Paint method, either as a public method,
    // or as an explicit interface implementation.
    public class SampleClass3 : IControl3
    {
        // Paint() is inherited from IControl.
    }
    
    // Explicit implementation is also used to resolve cases where two interfaces each declare different members of
    // the same name such as a property and a method.
    interface ILeft
    {
        int P { get; }
    }
    interface IRight
    {
        int P();
    }
    class Middle : ILeft, IRight
    {
        public int P() { return 0; }
        int ILeft.P { get { return 0; } }
    }
}