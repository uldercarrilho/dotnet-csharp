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
                throw new ArgumentOutOfRangeException("end must be less than or equal to 100."); //Line 22
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
                throw new ArgumentOutOfRangeException("secondsDelay cannot exceed 5."); // Line 15

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
            Console.WriteLine("Inside Main, before calling the method, the first element is: {0}", arr [0]);
            Change(arr);
            Console.WriteLine("Inside Main, after calling the method, the first element is: {0}", arr [0]);
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
        
        
        // EXTENSION METHODS
        
        
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