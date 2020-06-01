using System;

namespace Constructors
{
    // Whenever a class or struct is created, its constructor is called.
    // A class or struct may have multiple constructors that take different arguments.
    // Constructors enable the programmer to set default values, limit instantiation, and write code that is flexible and easy to read.
    
    // If you don't provide a constructor for your class, C# creates one by default that instantiates the object and sets member
    // variables to the default values as listed in the Default values of C# types article. If you don't provide a constructor
    // for your struct, C# relies on an implicit parameterless constructor to automatically initialize each field to its default value.
    
    // Unless the class is static, classes without constructors are given a public parameterless constructor by
    // the C# compiler in order to enable class instantiation.
    
    class Program
    {
        public static int Counter;
        private string _name;
        
        // A constructor is a method whose name is the same as the name of its type.
        // Its method signature includes only the method name and its parameter list; it does not include a return type.
        // Instance constructors are used to create and initialize any instance member variables when
        // you use the new expression to create an object of a class.
        public Program() { }
        public Program(string name) => _name = name;
        
        // C# doesn't provide a copy constructor for objects, but you can write one yourself.
        public Program(Program anotherProgram)
        {
            _name = anotherProgram._name;
        }
        
        // A class or struct can also have a static constructor, which initializes static members of the type.
        // Static constructors are parameterless. If you don't provide a static constructor to initialize static fields,
        // the C# compiler initializes static fields to their default value.
        // Static constructors are called automatically, immediately before any static fields are accessed.
        static Program() => Counter = 0;

        // Constructors for struct types resemble class constructors, but structs cannot contain an explicit parameterless
        // constructor because one is provided automatically by the compiler. This constructor initializes each field in the
        // struct to the default value. However, this parameterless constructor is only invoked if the struct is instantiated with new.
        
        static void Main(string[] args)
        {
            // this code uses the parameterless constructor for Int32, so that you are assured that the integer is initialized:
            int i = new int();  
            Console.WriteLine(i);  
            
            // The following code, however, causes a compiler error because it does not use new, and because
            // it tries to use an object that has not been initialized:
            // int i;  
            // Console.WriteLine(i);
            
            // So calling the parameterless constructor for a value type is not required.
        }
    }
    
    // You can prevent a class from being instantiated by making the constructor private
    // A private constructor is a special instance constructor. It is generally used in classes that contain static members only.
    // If a class has one or more private constructors and no public constructors, other classes (except nested classes)
    // cannot create instances of this class.
    class NLog
    {
        // The declaration of the empty constructor prevents the automatic generation of a parameterless constructor.
        // Note that if you do not use an access modifier with the constructor it will still be private by default.
        // However, the private modifier is usually used explicitly to make it clear that the class cannot be instantiated.
        private NLog() { }
        public static double e = Math.E;  //2.71828...
    }
    
    public class Employee
    {
        private string _name;
        public int Salary;

        public Employee(int annualSalary) => Salary = annualSalary;
        public Employee(int weeklySalary, int numberOfWeeks) => Salary = weeklySalary * numberOfWeeks;
        
        // A constructor can invoke another constructor in the same object by using the this keyword.
        // Like base, this can be used with or without parameters, and any parameters in the constructor
        // are available as parameters to this, or as part of an expression.
        public Employee(string name, int weeklySalary, int numberOfWeeks)
            : this(weeklySalary * numberOfWeeks)
        {
            _name = name;
        }
    }
    
    public class Manager : Employee
    {
        // A constructor can use the base keyword to call the constructor of a base class.
        public Manager(int annualSalary) 
            : base(annualSalary)
        {
            //Add further instructions here.
        }
        
        // In a derived class, if a base-class constructor is not called explicitly by using the base keyword,
        // the parameterless constructor, if there is one, is called implicitly. If a base class does not offer a
        // parameterless constructor, the derived class must make an explicit call to a base constructor by using base.
    }
    
    // Static constructors have the following properties:
    //     A static constructor does not take access modifiers or have parameters.
    //
    //     A class or struct can only have one static constructor.
    //
    //     Static constructors cannot be inherited or overloaded.
    //
    //     A static constructor cannot be called directly and is only meant to be called by the common language runtime (CLR).
    //     It is invoked automatically.
    //
    //     The user has no control on when the static constructor is executed in the program.
    //
    //     A static constructor is called automatically to initialize the class before the first instance is created or
    //     any static members are referenced. A static constructor will run before an instance constructor. A type's
    //     static constructor is called when a static method assigned to an event or a delegate is invoked and not when
    //     it is assigned. If static field variable initializers are present in the class of the static constructor, they
    //     will be executed in the textual order in which they appear in the class declaration immediately prior to the
    //     execution of the static constructor.
    //
    //     If you don't provide a static constructor to initialize static fields, all static fields are initialized to
    //     their default value as listed in Default values of C# types.
    //
    //     If a static constructor throws an exception, the runtime will not invoke it a second time, and the type will
    //     remain uninitialized for the lifetime of the application domain in which your program is running. Most commonly,
    //     a TypeInitializationException exception is thrown when a static constructor is unable to instantiate a type or
    //     for an unhandled exception occurring within a static constructor. For implicit static constructors that are
    //     not explicitly defined in source code, troubleshooting may require inspection of the intermediate language (IL) code.
    //
    //     The presence of a static constructor prevents the addition of the BeforeFieldInit type attribute.
    //     This limits runtime optimization.
    //
    //     A field declared as static readonly may only be assigned as part of its declaration or in a static constructor.
    //     When an explicit static constructor is not required, initialize static fields at declaration, rather than
    //     through a static constructor for better runtime optimization.
}