using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Methods
{
    // A method is a code block that contains a series of statements.
    // A program causes the statements to be executed by calling the method and specifying any required method arguments.
    // In C#, every executed instruction is performed in the context of a method.
    // The Main method is the entry point for every C# application and it's called by the common language runtime (CLR) when the program is started.
    
    // Methods are declared in a class, struct, or interface by specifying the access level such as public or private,
    // optional modifiers such as abstract or sealed, the return value, the name of the method, and any method parameters.
    // These parts together are the signature of the method.
    
    // A return type of a method is not part of the signature of the method for the purposes of method overloading.
    // However, it is part of the signature of the method when determining the compatibility between a delegate and the method that it points to.

    // Method parameters are enclosed in parentheses and are separated by commas. Empty parentheses indicate that the method requires no parameters
    abstract class Motorcycle
    {
        // Anyone can call this.
        public void StartEngine() {/* Method statements here */ }

        // Only derived classes can call this.
        protected void AddGas(int gallons) { /* Method statements here */ }

        // Derived classes can override the base class implementation.
        public virtual int Drive(int miles, int speed) { /* Method statements here */ return 1; }

        // Derived classes must implement this.
        public abstract double GetTopSpeed();
    }
    
    // The method definition specifies the names and types of any parameters that are required.
    // When calling code calls the method, it provides concrete values called arguments for each parameter.
    // The arguments must be compatible with the parameter type but the argument name (if any) used in the calling code
    // doesn't have to be the same as the parameter named defined in the method.
    
    // By default, when an instance of a value type is passed to a method, its copy is passed instead of the instance itself.
    // Therefore, changes to the argument have no effect on the original instance in the calling method.
    // To pass a value-type instance by reference, use the ref keyword.
    
    // When an object of a reference type is passed to a method, a reference to the object is passed.
    // That is, the method receives not the object itself but an argument that indicates the location of the object.
    // If you change a member of the object by using this reference, the change is reflected in the argument
    // in the calling method, even if you pass the object by value.
    
    class Program
    {
        private double _estDistance = 12;
        private VeryLargeStruct veryLargeStruct;
        private VeryLargeStruct anotherVeryLargeStruct;
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
        
        // Methods can return a value to the caller. If the return type, the type listed before the method name, is not void,
        // the method can return the value by using the return keyword. A statement with the return keyword followed by a
        // value that matches the return type will return that value to the method caller.

        // The value can be returned to the caller by value or, starting with C# 7.0, by reference. Values are returned to
        // the caller by reference if the ref keyword is used in the method signature and it follows each return keyword.
        // That variable's scope must include the method. That variable's lifetime must extend beyond the return of the method.
        public ref double GetEstimatedDistance()
        {
            // The return value must have a lifetime that extends beyond the execution of the method.
            // In other words, it cannot be a local variable in the method that returns it.
            // It can be an instance or static field of a class, or it can be an argument passed to the method.
            // The return value cannot be the literal null.
            // A method with a ref return can return an alias to a variable whose value is currently the null (uninstantiated) value or
            // a nullable value type for a value type.
            // The return value cannot be a constant, an enumeration member, the by-value return value from a property, or a method of a class or struct.
            // In addition, reference return values are not allowed on async methods.
            // An asynchronous method may return before it has finished execution, while its return value is still unknown.
            // Each return statement in the method body includes the ref keyword in front of the name of the returned instance.
            return ref _estDistance;
        }
        
        // The return keyword also stops the execution of the method. If the return type is void, a return statement without a value
        // is still useful to stop the execution of the method. Without the return keyword, the method will stop executing when it
        // reaches the end of the code block. Methods with a non-void return type are required to use the return keyword to return a value.
        
        // To use a value returned from a method, the calling method can use the method call itself anywhere a value of
        // the same type would be sufficient. You can also assign the return value to a variable.

        public void Example()
        {
            // To use a value returned by reference from a method, you must declare a ref local variable if you intend to modify its value.
            // The caller can create a new variable that is itself a reference to the returned value, called a ref local.
            // Ref local variables must still be initialized when they are declared.
            ref double distanceAsRef = ref GetEstimatedDistance();
            // The caller can then choose to treat the returned variable as if it were returned by value or by reference.
            double distanceAsValue = GetEstimatedDistance();
            
            // You can access a value by reference in the same way.
            // In some cases, accessing a value by reference increases performance by avoiding a potentially expensive copy operation.
            ref VeryLargeStruct refLocal = ref veryLargeStruct;
            refLocal = ref anotherVeryLargeStruct; // reassigned, refLocal refers to different storage.
        }
        
        
        // ASYNC METHODS
        
        // By using the async feature, you can invoke asynchronous methods without using explicit callbacks or manually
        // splitting your code across multiple methods or lambda expressions.

        // If you mark a method with the async modifier, you can use the await operator in the method. When control
        // reaches an await expression in the async method, control returns to the caller, and progress in the method is
        // suspended until the awaited task completes. When the task is complete, execution can resume in the method.

        // An async method returns to the caller when either it encounters the first awaited object that’s not yet
        // complete or it gets to the end of the async method, whichever occurs first.

        // An async method can have a return type of Task<TResult>, Task, or void. The void return type is used primarily
        // to define event handlers, where a void return type is required. An async method that returns void can't be
        // awaited, and the caller of a void-returning method can't catch exceptions that the method throws.
        
        // An async method can't declare any ref or out parameters, but it can call methods that have such parameters.

        // This Click event is marked with the async modifier.
        private async void startButton_Click(object sender, RoutedEventArgs e)
        {
            await DoSomethingAsync();
        }

        private async Task DoSomethingAsync()
        {
            Task<int> delayTask = DelayAsync();
            int result = await delayTask;

            // The previous two statements may be combined into the following statement.
            // int result = await DelayAsync();
            Debug.WriteLine("Result: " + result);
        }

        private async Task<int> DelayAsync()
        {
            await Task.Delay(100);
            return 5;
        }
        
        
        // LOCAL FUNCTIONS
        
        // Starting with C# 7.0, C# supports local functions.
        // Local functions are private methods of a type that are nested in another member.
        // They can only be called from their containing member. Local functions can be declared in and called from:
        //     Methods, especially iterator methods and async methods
        //     Constructors
        //     Property accessors
        //     Event accessors
        //     Anonymous methods
        //     Lambda expressions
        //     Finalizers
        //     Other local functions
        // However, local functions can't be declared inside an expression-bodied member.
        
        // Local functions make the intent of your code clear. Anyone reading your code can see that the method is not
        // callable except by the containing method. For team projects, they also make it impossible for another
        // developer to mistakenly call the method directly from elsewhere in the class or struct.
        
        // A local function is defined as a nested method inside a containing member. Its definition has the following syntax:
        // <modifiers: async | unsafe> <return-type> <method-name> <parameter-list>

        // Local functions can use the async and unsafe modifiers.

        // Note that all local variables that are defined in the containing member, including its method parameters, are accessible in the local function.

        // Unlike a method definition, a local function definition cannot include the member access modifier. Because all local functions are private

        // In addition, attributes can't be applied to the local function or to its parameters and type parameters.
        
        private static string GetText(string path, string filename)
        {
            StreamReader sr = File.OpenText(AppendPathSeparator(path) + filename);
            string text = sr.ReadToEnd();
            return text;

            // Declare a local function.
            // It's common to declare local functions at the end of the parent method, after any return statements.
            string AppendPathSeparator(string filepath)
            {
                if (! filepath.EndsWith(@"\"))
                    filepath += @"\";

                return filepath;
            }
        }
        
        // One of the useful features of local functions is that they can allow exceptions to surface immediately.
        // For method iterators, exceptions are surfaced only when the returned sequence is enumerated, and not when the iterator is retrieved.
        // For async methods, any exceptions thrown in an async method are observed when the returned task is awaited.
        public static IEnumerable<int> OddSequence(int start, int end)
        {
            if (start < 0 || start > 99)
                throw new ArgumentOutOfRangeException("start must be between 0 and 99.");
            if (end > 100)
                throw new ArgumentOutOfRangeException("end must be less than or equal to 100.");
            if (start >= end)
                throw new ArgumentException("start must be less than end.");

            return GetOddSequenceEnumerator();

            IEnumerable<int> GetOddSequenceEnumerator()
            {
                for (int i = start; i <= end; i++)
                {
                    if (i % 2 == 1)
                        yield return i;
                }
            }
        }

        static void Example2()
        {
            IEnumerable<int> ienum = OddSequence(50, 110); // Exception throws when call this
            Console.WriteLine("Retrieved enumerator...");

            foreach (var i in ienum)
            {
                Console.Write($"{i} ");
            }
        }

        // Local functions can be used in a similar way to handle exceptions outside of the asynchronous operation.
        // Ordinarily, exceptions thrown in async method require that you examine the inner exceptions of an AggregateException.
        // Local functions allow your code to fail fast and allow your exception to be both thrown and observed synchronously.
        static Task<int> GetMultiple(int secondsDelay)
        {
            if (secondsDelay < 0 || secondsDelay > 5)
                throw new ArgumentOutOfRangeException("secondsDelay cannot exceed 5.");

            return GetValueAsync();

            async Task<int> GetValueAsync()
            {
                Console.WriteLine("Executing GetValueAsync...");
                await Task.Delay(secondsDelay * 1000);
                return secondsDelay * new Random().Next(2,10);
            }
        }

        static void Example3()
        {
            int result = GetMultiple(6).Result; // the ArgumentOutOfRangeException is not wrapped in a AggregateException.
            Console.WriteLine($"The returned value is {result:N0}");
        }
        
        
        // PASSING PARAMETERS
        
        // In C#, arguments can be passed to parameters either by value or by reference.
        // Passing by reference enables function members, methods, properties, indexers, operators, and constructors to
        // change the value of the parameters and have that change persist in the calling environment.
        // To pass a parameter by reference with the intent of changing the value, use the ref, or out keyword.
        static void ParametersByRef(ref int refParameter)
        {
            refParameter *= refParameter;
        }
        static void ParametersByOut(out int refParameter)
        {
            refParameter = 24 * 60;
        }
        // To pass by reference with the intent of avoiding copying but not changing the value, use the in modifier.
        static void ParametersByIn(in int refParameter)
        {
            int localParameter = refParameter * refParameter;
        }

        static void Example4()
        {
            int localInt = 5;
            ParametersByRef(ref localInt);
            ParametersByOut(out localInt);
            ParametersByIn(localInt);
        }
        
        static void SwapByRef(ref int x, ref int y)
        {
            int temp = x;
            x = y;
            y = temp;
        }

        // Passing Reference Types by Value
        static void Change(int[] pArray)
        {
            pArray[0] = 888;  // This change affects the original element.
            pArray = new int[5] {-3, -1, -2, -3, -4};   // This change is local.
            System.Console.WriteLine("Inside the method, the first element is: {0}", pArray[0]);
        }
        static void Example5()
        {
            int[] arr = {1, 4, 5};
            Console.WriteLine("Inside Main, before calling the method, the first element is: {0}", arr[0]);
            Change(arr);
            Console.WriteLine("Inside Main, after calling the method, the first element is: {0}", arr[0]);
            // Output
            // Inside Main, before calling the method, the first element is: 1
            // Inside the method, the first element is: -3
            // Inside Main, after calling the method, the first element is: 888
        }
        
        // Passing Reference Types by Reference
        static void ChangeByRef(ref int[] pArray)
        {
            // Both of the following changes will affect the original variables:
            pArray[0] = 888;
            pArray = new int[5] {-3, -1, -2, -3, -4};
            System.Console.WriteLine("Inside the method, the first element is: {0}", pArray[0]);
        }
        static void Example6()
        {
            int[] arr = {1, 4, 5};
            Console.WriteLine("Inside Main, before calling the method, the first element is: {0}", arr[0]);
            ChangeByRef(ref arr);
            Console.WriteLine("Inside Main, after calling the method, the first element is: {0}", arr[0]);
            // Output
            // Inside Main, before calling the method, the first element is: 1
            // Inside the method, the first element is: -3
            // Inside Main, after calling the method, the first element is: -3
        }

        
        // IMPLICITLY TYPED LOCAL VARIABLES
        
        // Local variables can be declared without giving an explicit type. The var keyword instructs the compiler to infer
        // the type of the variable from the expression on the right side of the initialization statement. The inferred type
        // may be a built-in type, an anonymous type, a user-defined type, or a type defined in the .NET Framework class library.
        
        // It is important to understand that the var keyword does not mean "variant" and does not indicate that the variable
        // is loosely typed, or late-bound. It just means that the compiler determines and assigns the most appropriate type.
        
        static void Example7()
        {
            // i is compiled as an int
            var i = 5;

            // s is compiled as a string
            var s = "Hello";

            // a is compiled as int[]
            var a = new[] { 0, 1, 2 };

            IEnumerable<Costumer> customers = new List<Costumer>();
            // expr is compiled as IEnumerable<Customer> or perhaps IQueryable<Customer>
            var expr =
                from c in customers
                where c.City == "London"
                select c;

            // anon is compiled as an anonymous type
            var anon = new { Name = "Terry", Age = 34 };

            // list is compiled as List<int>
            var list = new List<int>(); 
            
            // can used in a for initialization statement.
            for (var x = 1; x < 10; x++) { }
            // can used in a foreach initialization statement.
            foreach (var item in list) { }
            // can used in a using statement.
            using (var file = new StreamReader(@"C:\myfile.txt")) { }
        }
        
        // In many cases the use of var is optional and is just a syntactic convenience. However, when a variable is
        // initialized with an anonymous type you must declare the variable as var if you need to access the properties
        // of the object at a later point. This is a common scenario in LINQ query expressions.
        static void Example8()
        {
            string[] words = { "aPPLE", "BlUeBeRrY", "cHeRry" };

            // If a query produces a sequence of anonymous types, then use var in the foreach statement to access the properties.
            var upperLowerWords =
                from w in words
                select new { Upper = w.ToUpper(), Lower = w.ToLower() };

            // Execute the query
            foreach (var ul in upperLowerWords)
            {
                Console.WriteLine("Uppercase: {0}, Lowercase: {1}", ul.Upper, ul.Lower);
            }
        }
        
        // The following restrictions apply to implicitly-typed variable declarations:
        //     var can only be used when a local variable is declared and initialized in the same statement;
        //     the variable cannot be initialized to null, or to a method group or an anonymous function.
        //
        //     var cannot be used on fields at class scope.
        //
        //     Variables declared by using var cannot be used in the initialization expression.
        //     In other words, this expression is legal: int i = (i = 20); but this expression produces a compile-time error: var i = (i = 20);
        //
        //     Multiple implicitly-typed variables cannot be initialized in the same statement.
        //
        //     If a type named var is in scope, then the var keyword will resolve to that type name and will not be
        //     treated as part of an implicitly typed local variable declaration.
        
        // The use of var helps simplify your code, but its use should be restricted to cases where it is required,
        // or when it makes your code easier to read.
    }

    internal class Costumer
    {
        public string City { get; set; }
    }

    internal struct VeryLargeStruct
    {
    }

    internal class RoutedEventArgs
    {
    }
}

namespace ExtensionMethods
{
    // EXTENSION METHODS
        
    // Extension methods enable you to "add" methods to existing types without creating a new derived type,
    // recompiling, or otherwise modifying the original type. Extension methods are static methods,
    // but they're called as if they were instance methods on the extended type.
        
    // Their first parameter specifies which type the method operates on. The parameter is preceded by the this modifier.
    // Extension methods are only in scope when you explicitly import the namespace into your source code with a using directive.
    
    // The following example shows an extension method defined for the System.String class. It's defined inside a non-nested, non-generic static class:
    public static class MyExtensions
    {
        public static int WordCount(this String str)
        {
            return str.Split(new char[] { ' ', '.', '?' }, StringSplitOptions.RemoveEmptyEntries).Length;
        }
    }
    // And it can be called from an application by using this syntax:
    // string s = "Hello Extension Methods";
    // int i = s.WordCount();
    
    // You invoke the extension method in your code with instance method syntax.
    // The intermediate language (IL) generated by the compiler translates your code into a call on the static method.
    // The principle of encapsulation is not really being violated.
    // Extension methods cannot access private variables in the type they are extending.
    
    // You can use extension methods to extend a class or interface, but not to override them.
    // An extension method with the same name and signature as an interface or class method will never be called.
    // At compile time, extension methods always have lower priority than instance methods defined in the type itself.
    // When the compiler encounters a method invocation, it first looks for a match in the type's instance methods.
    // If no match is found, it will search for any extension methods that are defined for the type,
    // and bind to the first extension method that it finds.
    
    // Common Usage Patterns
    
    // In the past, it was common to create "Collection Classes" that implemented the System.Collections.Generic.IEnumerable<T>
    // interface for a given type and contained functionality that acted on collections of that type. While there's nothing
    // wrong with creating this type of collection object, the same functionality can be achieved by using an extension
    // on the System.Collections.Generic.IEnumerable<T>. Extensions have the advantage of allowing the functionality to
    // be called from any collection such as an System.Array or System.Collections.Generic.List<T> that implements
    // System.Collections.Generic.IEnumerable<T> on that type.
    
    // When using an Onion Architecture or other layered application design, it's common to have a set of Domain Entities
    // or Data Transfer Objects that can be used to communicate across application boundaries. These objects generally
    // contain no functionality, or only minimal functionality that applies to all layers of the application. Extension
    // methods can be used to add functionality that is specific to each application layer without loading the object
    // down with methods not needed or wanted in other layers.
    
    public class DomainEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    static class DomainEntityExtensions
    {
        static string FullName(this DomainEntity value) => $"{value.FirstName} {value.LastName}";
    }
    
    // Rather than creating new objects when reusable functionality needs to be created, we can often extend an existing
    // type, such as a .NET or CLR type. As an example, if we don't use extension methods, we might create an Engine or
    // Query class to do the work of executing a query on a SQL Server that may be called from multiple places in our code.
    // However we can instead extend the System.Data.SqlClient.SqlConnection class using extension methods to perform
    // that query from anywhere we have a connection to a SQL Server. Other examples might be to add common functionality
    // to the System.String class, extend the data processing capabilities of the System.IO.File and System.IO.Stream
    // objects, and System.Exception objects for specific error handling functionality. These types of use-cases are
    // limited only by your imagination and good sense.

    // Extending predefined types can be difficult with struct types because they're passed by value to methods.
    // That means any changes to the struct are made to a copy of the struct.
    // Those changes aren't visible once the extension method exits.
    // Beginning with C# 7.2, you can add the ref modifier to the first argument of an extension method.
    // Adding the ref modifier means the first argument is passed by reference.
    // This enables you to write extension methods that change the state of the struct being extended.
    
    // While it's still considered preferable to add functionality by modifying an object's code or deriving a new type
    // whenever it's reasonable and possible to do so, extension methods have become a crucial option for creating
    // reusable functionality throughout the .NET ecosystem. For those occasions when the original source isn't under
    // your control, when a derived object is inappropriate or impossible, or when the functionality shouldn't be
    // exposed beyond its applicable scope, Extension methods are an excellent choice.
    
    // When using an extension method to extend a type whose source code you aren't in control of, you run the risk that
    // a change in the implementation of the type will cause your extension method to break.

    // If you do implement extension methods for a given type, remember the following points:
    //     An extension method will never be called if it has the same signature as a method defined in the type.
    //     Extension methods are brought into scope at the namespace level. For example, if you have multiple static
    //         classes that contain extension methods in a single namespace named Extensions, they'll all be brought into
    //         scope by the using Extensions; directive.
    
    // For a class library that you implemented, you shouldn't use extension methods to avoid incrementing the version
    // number of an assembly. If you want to add significant functionality to a library for which you own the source code,
    // follow the .NET guidelines for assembly versioning.
}

namespace EnumExtension
{
    // Note that the Extensions class also contains a static variable that is updated dynamically and that the return
    // value of the extension method reflects the current value of that variable. This demonstrates that, behind the
    // scenes, extension methods are invoked directly on the static class in which they are defined.
    public static class Extensions
    {
        public static Grades minPassing = Grades.D;
        public static bool Passing(this Grades grade)
        {
            return grade >= minPassing;
        }
    }

    public enum Grades { F = 0, D=1, C=2, B=3, A=4 };
    
    class Program
    {
        static void Example()
        {
            Grades g1 = Grades.D;
            Grades g2 = Grades.F;
            Console.WriteLine("First {0} a passing grade.", g1.Passing() ? "is" : "is not");
            Console.WriteLine("Second {0} a passing grade.", g2.Passing() ? "is" : "is not");

            Extensions.minPassing = Grades.C;
            Console.WriteLine("\r\nRaising the bar!\r\n");
            Console.WriteLine("First {0} a passing grade.", g1.Passing() ? "is" : "is not");
            Console.WriteLine("Second {0} a passing grade.", g2.Passing() ? "is" : "is not");
        }
    }
}


// NAMED AND OPTIONAL ARGUMENTS

// C# 4 introduces named and optional arguments.

// Named arguments enable you to specify an argument for a particular parameter by associating the argument with the
// parameter's name rather than with the parameter's position in the parameter list. This free you from the need to
// remember or to look up the order of parameters in the parameter lists of called methods.

// Named arguments, when used with positional arguments, are valid as long as:
//    they're not followed by any positional arguments, or
//    starting with C# 7.2, they're used in the correct position.
// Positional arguments that follow any out-of-order named arguments are invalid.

class NamedExample
{
    static void Example()
    {
        // The method can be called in the normal way, by using positional arguments.
        PrintOrderDetails("Gift Shop", 31, "Red Mug");

        // Named arguments can be supplied for the parameters in any order.
        PrintOrderDetails(orderNum: 31, productName: "Red Mug", sellerName: "Gift Shop");
        PrintOrderDetails(productName: "Red Mug", sellerName: "Gift Shop", orderNum: 31);

        // Named arguments mixed with positional arguments are valid
        // as long as they are used in their correct position.
        PrintOrderDetails("Gift Shop", 31, productName: "Red Mug");
        PrintOrderDetails(sellerName: "Gift Shop", 31, productName: "Red Mug");    // C# 7.2 onwards
        PrintOrderDetails("Gift Shop", orderNum: 31, "Red Mug");                   // C# 7.2 onwards

        // However, mixed arguments are invalid if used out-of-order.
        // The following statements will cause a compiler error.
        // PrintOrderDetails(productName: "Red Mug", 31, "Gift Shop");
        // PrintOrderDetails(31, sellerName: "Gift Shop", "Red Mug");
        // PrintOrderDetails(31, "Red Mug", sellerName: "Gift Shop");
    }

    static void PrintOrderDetails(string sellerName, int orderNum, string productName)
    {
        if (string.IsNullOrWhiteSpace(sellerName))
        {
            throw new ArgumentException(message: "Seller name cannot be null or empty.", paramName: nameof(sellerName));
        }

        Console.WriteLine($"Seller: {sellerName}, Order #: {orderNum}, Product: {productName}");
    }
}

// Optional arguments enable you to omit arguments for some parameters.
// Both techniques can be used with methods, indexers, constructors, and delegates.

// Each optional parameter has a default value as part of its definition. If no argument is sent for that parameter,
// the default value is used. A default value must be one of the following types of expressions:
//     a constant expression;
//     an expression of the form new ValType(), where ValType is a value type, such as an enum or a struct;
//     an expression of the form default(ValType), where ValType is a value type.

// Optional parameters are defined at the end of the parameter list, after any required parameters. If the caller provides
// an argument for any one of a succession of optional parameters, it must provide arguments for all preceding optional
// parameters. Comma-separated gaps in the argument list are not supported. However, you can use a named argument to accomplish this.

class ExampleClass
{
    public string Name { get; set; }

    public void ExampleMethod(int required, string optionalstr = "default string", int optionalint = 10)
    {
        Console.WriteLine("{0}: {1}, {2}, and {3}.", Name, required, optionalstr, optionalint);
    }

    public void TestCall()
    {
        ExampleMethod(1);
        ExampleMethod(2, "teste");
        ExampleMethod(3, "teste", 5);
        ExampleMethod(4, optionalint: 5);
    }
}

// When you use named and optional arguments, the arguments are evaluated in the order in
// which they appear in the argument list, not the parameter list.

// Named and optional parameters, when used together, enable you to supply arguments for only a few parameters from a list
// of optional parameters. This capability greatly facilitates calls to COM interfaces such as the Microsoft Office Automation APIs.

// Use of named and optional arguments affects overload resolution in the following ways:
//     A method, indexer, or constructor is a candidate for execution if each of its parameters either is optional or
//     corresponds, by name or by position, to a single argument in the calling statement, and that argument can be
//     converted to the type of the parameter.
//
//     If more than one candidate is found, overload resolution rules for preferred conversions are applied to the
//     arguments that are explicitly specified. Omitted arguments for optional parameters are ignored.
//
//     If two candidates are judged to be equally good, preference goes to a candidate that does not have optional
//     parameters for which arguments were omitted in the call. This is a consequence of a general preference in overload
//     resolution for candidates that have fewer parameters.
