using System;

namespace DelegatesProject
{
    // A delegate is a type that represents references to methods with a particular parameter list and return type.
    // When you instantiate a delegate, you can associate its instance with any method with a compatible signature and return type.
    // You can invoke (or call) the method through the delegate instance.

    // Delegates are used to pass methods as arguments to other methods. Event handlers are nothing more than methods
    // that are invoked through delegates. You create a custom method, and a class such as a windows control can call
    // your method when a certain event occurs.
    
    // Any method from any accessible class or struct that matches the delegate type can be assigned to the delegate.
    // The method can be either static or an instance method. This makes it possible to programmatically change method
    // calls, and also plug new code into existing classes.
    
    // In the context of method overloading, the signature of a method does not include the return value.
    // But in the context of delegates, the signature does include the return value.
    // In other words, a method must have the same return type as the delegate.
    
    // This ability to refer to a method as a parameter makes delegates ideal for defining callback methods.
    // For example, a reference to a method that compares two objects could be passed as an argument to a sort algorithm.
    // Because the comparison code is in a separate procedure, the sort algorithm can be written in a more general way.
    
    // Delegates are similar to C++ function pointers, but delegates are fully object-oriented, and unlike C++ pointers
    // to member functions, delegates encapsulate both an object instance and a method.

    // Delegates allow methods to be passed as parameters.

    // Delegates can be used to define callback methods.

    // Delegates can be chained together; for example, multiple methods can be called on a single event.

    // Methods do not have to match the delegate type exactly.
    
    // A delegate object is normally constructed by providing the name of the method the delegate will wrap, or with an
    // anonymous function. Once a delegate is instantiated, a method call made to the delegate will be passed by the
    // delegate to that method. The parameters passed to the delegate by the caller are passed to the method, and the
    // return value, if any, from the method is returned to the caller by the delegate. This is known as invoking the delegate.
    // An instantiated delegate can be invoked as if it were the wrapped method itself.
    
    class Program
    {
        public delegate int PerformCalculation(int x, int y);
        public delegate void Del(string message);
        
        // Create a method for a delegate.
        public static void DelegateMethod(string message)
        {
            Console.WriteLine(message);
        }
        
        // Delegate types are derived from the Delegate class in .NET. Delegate types are sealed—they cannot be derived from—
        // and it is not possible to derive custom classes from Delegate. Because the instantiated delegate is an object,
        // it can be passed as a parameter, or assigned to a property. This allows a method to accept a delegate as a parameter,
        // and call the delegate at some later time. This is known as an asynchronous callback, and is a common method of
        // notifying a caller when a long process has completed. When a delegate is used in this fashion, the code using
        // the delegate does not need any knowledge of the implementation of the method being used. The functionality is
        // similar to the encapsulation interfaces provide.
        public static void MethodWithCallback(int param1, int param2, Del callback)
        {
            callback("The number is: " + (param1 + param2).ToString());
        }
        
        static void Main(string[] args)
        {
            // Instantiate the delegate.
            Del handler = DelegateMethod;
            // Call the delegate.
            handler("Hello World");
            MethodWithCallback(1, 2, handler);
            
            // A delegate can call more than one method when invoked. This is referred to as multicasting.
            // To add an extra method to the delegate's list of methods—the invocation list—simply requires
            // adding two delegates using the addition or addition assignment operators ('+' or '+=').
            var obj = new MethodClass();
            Del d1 = obj.Method1;
            Del d2 = obj.Method2;
            Del d3 = DelegateMethod;

            //Both types of assignment are valid.
            Del allMethodsDelegate = d1 + d2;
            allMethodsDelegate += d3;
            allMethodsDelegate("multicasting message");
            
            // At this point allMethodsDelegate contains three methods in its invocation list—Method1, Method2, and DelegateMethod.
            // The original three delegates, d1, d2, and d3, remain unchanged. When allMethodsDelegate is invoked, all
            // three methods are called in order. If the delegate uses reference parameters, the reference is passed
            // sequentially to each of the three methods in turn, and any changes by one method are visible to the next method.
            // When any of the methods throws an exception that is not caught within the method, that exception is passed
            // to the caller of the delegate and no subsequent methods in the invocation list are called. If the delegate
            // has a return value and/or out parameters, it returns the return value and parameters of the last method invoked.
            // To remove a method from the invocation list, use the subtraction or subtraction assignment operators (- or -=).
            
            // remove Method1
            allMethodsDelegate -= d1;
            // copy AllMethodsDelegate while removing d2
            Del oneMethodDelegate = allMethodsDelegate - d2;
            
            // Because delegate types are derived from System.Delegate, the methods and properties defined by that class
            // can be called on the delegate. For example, to find the number of methods in a delegate's invocation list, you may write:
            int invocationCount = d1.GetInvocationList().GetLength(0);
    
            // Delegates with more than one method in their invocation list derive from MulticastDelegate, which is a subclass of System.Delegate.
            
            // Multicast delegates are used extensively in event handling. Event source objects send event notifications to
            // recipient/ objects that have registered to receive that event. To register for an event, the recipient creates
            // a method designed to handle the event, then creates a delegate for that method and passes the delegate to the
            // event source. The source calls the delegate when the event occurs. The delegate then calls the event handling
            // method on the recipient, delivering the event data. The delegate type for a given event is defined by the event source.
            
        }
        
        delegate void Delegate1();
        delegate void Delegate2();

        // Comparing delegates of two different types assigned at compile-time will result in a compilation error. If the delegate
        // instances are statically of the type System.Delegate, then the comparison is allowed, but will return false at run time.
        static void method(Delegate1 d, Delegate2 e, System.Delegate f)
        {
            // Compile-time error.
            //Console.WriteLine(d == e);

            // OK at compile-time. False if the run-time type of f
            // is not the same as that of d.
            Console.WriteLine(d == f);
        }
    }
    
    public class MethodClass
    {
        public void Method1(string message) { }
        public void Method2(string message) { }
    }
    
    // Delegates constructed with a named method can encapsulate either a static method or an instance method. Named methods
    // are the only way to instantiate a delegate in earlier versions of C#.// However, in a situation where creating a
    // new method is unwanted overhead, C# enables you to instantiate a delegate and immediately specify a code block that
    // the delegate will process when it is called. The block can contain either a lambda expression or an anonymous method.
    
    // Although the delegate can use an out parameter, we do not recommend its use with multicast event delegates because
    // you cannot know which delegate will be called.


}