using System;

namespace Properties
{
    // A property is a member that provides a flexible mechanism to read, write, or compute the value of a private field.
    // Properties can be used as if they are public data members, but they are actually special methods called accessors.
    // This enables data to be accessed easily and still helps promote the safety and flexibility of methods.
    
    // Properties enable a class to expose a public way of getting and setting values, while hiding implementation or verification code.

    // A get property accessor is used to return the property value, and a set property accessor is used to assign a new value.
    // These accessors can have different access levels. For more information, see Restricting Accessor Accessibility.

    // The value keyword is used to define the value being assigned by the set accessor.

    // Properties can be read-write (they have both a get and a set accessor),
    // read-only (they have a get accessor but no set accessor), or
    // write-only (they have a set accessor, but no get accessor).
    // Write-only properties are rare and are most commonly used to restrict access to sensitive data.

    // Simple properties that require no custom accessor code can be implemented either as expression body definitions or as auto-implemented properties.
    
    // Unlike fields, properties are not classified as variables. Therefore, you cannot pass a property as a ref or out parameter.
    
    class TimePeriod
    {
        private double _seconds;

        public double Hours
        {
            // The get accessor can be used to return the field value or to compute it and return it.
            get => _seconds / 3600;
            // The set accessor resembles a method whose return type is void.
            // It uses an implicit parameter called value, whose type is the type of the property.
            // It is an error to use the implicit parameter name, value, for a local variable declaration in a set accessor.
            set {
                if (value < 0 || value > 24)
                    throw new ArgumentOutOfRangeException($"{nameof(value)} must be between 0 and 24.");

                _seconds = value * 3600;
            }
        }
    }
    
    public class SaleItem
    {
        string _name;
        decimal _cost;

        public SaleItem(string name, decimal cost)
        {
            _name = name;
            _cost = cost;
        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public decimal Price
        {
            get => _cost;
            set => _cost = value;
        }
    }
    
    // If a property has both a get and a set accessor, both must be auto-implemented.
    // You define an auto-implemented property by using the get and set keywords without providing any implementation.
    // You can't declare auto-implemented properties in interfaces.
    // Auto-implemented properties declare a private instance backing field, and interfaces may not declare instance fields. 
    public class SaleItem2
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        // In C# 6 and later, you can initialize auto-implemented properties similarly to fields:
        public string Productor { get; set; } = "ACME";  
    }

    
    class Program
    {
        static void Main(string[] args)
        {
            var item = new SaleItem("Shoes", 19.95m);
            Console.WriteLine($"{item.Name}: sells for {item.Price:C2}");
            
            // Note that the example also removes the parameterized constructor, so that SaleItem objects are
            // now initialized with a call to the parameterless constructor and an object initializer.
            var item2 = new SaleItem2{ Name = "Shoes", Price = 19.95m };
            Console.WriteLine($"{item.Name}: sells for {item.Price:C2}");
        }
    }
    
    // When you are returning the private variable from the get accessor and optimizations are enabled, the call to the
    // get accessor method is inlined by the compiler so there is no method-call overhead. However, a virtual get accessor
    // method cannot be inlined because the compiler does not know at compile-time which method may actually be called at run time.
    class Person
    {
        // The get accessor must end in a return or throw statement, and control cannot flow off the accessor body.
        private string _name;  // the name field
        public string Name => _name;     // the Name property
        
        // It is a bad programming style to change the state of the object by using the get accessor.
        private int _number;
        public int Number => _number++;	// Don't do this

        public int Positive
        {
            // The get accessor must end in a return or throw statement, and control cannot flow off the accessor body.
            get
            {
                if (_number > 0)
                {
                    return _number;
                }
                throw new ArgumentOutOfRangeException();
            }
        }
    }
    
    // The get and set accessors for the same property may have different access modifiers.
    
    // A property may be declared as a static property by using the static keyword.
    // This makes the property available to callers at any time, even if no instance of the class exists.
    
    // A property may be marked as a virtual property by using the virtual keyword.
    // This enables derived classes to override the property behavior by using the override keyword.
    
    // A property overriding a virtual property can also be sealed, specifying that for derived classes it is no longer virtual.
    // Lastly, a property can be declared abstract.
    // This means that there is no implementation in the class, and derived classes must write their own implementation.
    
    // It is an error to use a virtual, abstract, or override modifier on an accessor of a static property.

    public class Employee
    {
        public static int NumberOfEmployees;
        private static int _counter;
        private string _name;

        // A read-write instance property:
        public string Name
        {
            get => _name;
            set => _name = value;
        }

        // A read-only static property:
        public static int Counter => _counter;

        // A Constructor:
        public Employee() => _counter = ++NumberOfEmployees; // Calculate the employee's number:
    }
    
    public class Manager : Employee
    {
        private string _name;

        // Notice the use of the new modifier:
        public new string Name
        {
            get => _name;
            set => _name = value + ", Manager";
        }
    }
    
    abstract class Shape
    {
        public abstract double Area { get; set; }
    }

    class Square : Shape
    {
        public double side;

        //constructor
        public Square(double s) => side = s;

        public override double Area
        {
            get => side * side;
            set => side = System.Math.Sqrt(value);
        }
    }
    
    // Properties can be declared on an interface. The following example declares an interface property accessor:
    public interface ISampleInterface
    {
        // Property declaration:
        string Name { get; set; }
    }

    // Interface properties typically don't have a body.
    // The accessors indicate whether the property is read-write, read-only, or write-only.
    // Unlike in classes and structs, declaring the accessors without a body doesn't declare an auto-implemented property.
    // Beginning with C# 8.0, an interface may define a default implementation for members, including properties.
    // Defining a default implementation for a property in an interface is rare because interfaces may not define instance data fields.
    
    interface IEmployee2
    {
        string Name { get; set; }
        int Counter { get; }
    }

    public class Employee2 : IEmployee2
    {
        public static int numberOfEmployees;

        private string _name;
        string IEmployee2.Name  // read-write instance property
        {
            get => _name;
            set => _name = value;
        }

        private int _counter;
        public int Counter  // read-only instance property
        {
            get => _counter;
        }

        // constructor
        public Employee2() => _counter = ++numberOfEmployees;
    }

    
    // RESTRICTING ACCESSOR ACCESSIBILITY
    
    // Using the accessor modifiers on properties or indexers is subject to these conditions:
    //     You cannot use accessor modifiers on an interface or an explicit interface member implementation.
    //     You can use accessor modifiers only if the property or indexer has both set and get accessors.
    //         In this case, the modifier is permitted on only one of the two accessors.
    //     If the property or indexer has an override modifier, the accessor modifier must match the accessor of the overridden accessor, if any.
    //     The accessibility level on the accessor must be more restrictive than the accessibility level on the property or indexer itself.
    public class Example1
    {
        private string _name = "Hello";
        public string Name
        {
            get => _name;
            protected set => _name = value;
        }
    }
    
    // When you override a property or indexer, the overridden accessors must be accessible to the overriding code. Also,
    // the accessibility of both the property/indexer and its accessors must match the corresponding overridden property/indexer and its accessors.
    public class Parent
    {
        public virtual int TestProperty
        {
            // Notice the accessor accessibility level.
            protected set { }
            // No access modifier is used here.
            get { return 0; }
        }
    }
    public class Kid : Parent
    {
        public override int TestProperty
        {
            // Use the same accessibility level as in the overridden accessor.
            protected set { }
            // Cannot use access modifier here.
            get { return 0; }
        }
    }
    
    // When you use an accessor to implement an interface, the accessor may not have an access modifier. However,
    // if you implement the interface using one accessor, such as get, the other accessor can have an access modifier
    public interface ISomeInterface
    {
        // No access modifier allowed here because this is an interface.
        int TestProperty { get; }
    }
    public class TestClass : ISomeInterface
    {
        public int TestProperty
        {
            // Cannot use access modifier here because this is an interface implementation.
            get { return 10; }
            // Interface property does not have set accessor, so access modifier is allowed.
            protected set { }
        }
    }
    
    // If you use an access modifier on the accessor, the accessibility domain of the accessor is determined by this modifier.
    // If you did not use an access modifier on the accessor, the accessibility domain of the accessor
    // is determined by the accessibility level of the property or indexer.
    
    // You can make an immutable property in two ways:
    //     You can declare the set accessor to be private. The property is only settable within the type, but it is immutable to consumers.
    //         When you declare a private set accessor, you cannot use an object initializer to initialize the property.
    //         You must use a constructor or a factory method.
    //     You can declare only the get accessor, which makes the property immutable everywhere except in the type's constructor.
    class Contact
    {
        public string Name { get; }
        public string Address { get; private set; }

        public Contact(string contactName, string contactAddress)
        {
            // Both properties are accessible in the constructor.
            Name = contactName;
            Address = contactAddress;
        }

        // Name isn't assignable here. This will generate a compile error.
        //public void ChangeName(string newName) => Name = newName;

        // Address is assignable here.
        public void ChangeAddress(string newAddress) => Address = newAddress;
    }
}