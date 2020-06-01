using System;

namespace Finalizers
{
    // Finalizers (which are also called destructors) are used to perform any necessary final clean-up when a
    // class instance is being collected by the garbage collector
    
    // Finalizers cannot be defined in structs. They are only used with classes.
    // A class can only have one finalizer.
    // Finalizers cannot be inherited or overloaded.
    // Finalizers cannot be called. They are invoked automatically.
    // A finalizer does not take modifiers or have parameters.
    
    // The finalizer implicitly calls Finalize on the base class of the object.
    
    // Empty finalizers should not be used. When a class contains a finalizer, an entry is created in the Finalize queue.
    // When the finalizer is called, the garbage collector is invoked to process the queue.
    // An empty finalizer just causes a needless loss of performance.
    
    // The programmer has no control over when the finalizer is called because this is determined by the garbage collector.
    // The garbage collector checks for objects that are no longer being used by the application. If it considers an
    // object eligible for finalization, it calls the finalizer (if any) and reclaims the memory used to store the object.
    
    // In .NET Framework applications (but not in .NET Core applications), finalizers are also called when the program exits.

    // However, when your application encapsulates unmanaged resources, such as windows, files, and network connections,
    // you should use finalizers to free those resources. When the object is eligible for finalization,
    // the garbage collector runs the Finalize method of the object.
    
    // If your application is using an expensive external resource, we also recommend that you provide a way to explicitly
    // release the resource before the garbage collector frees the object. You do this by implementing a Dispose method
    // from the IDisposable interface that performs the necessary cleanup for the object. This can considerably improve
    // the performance of the application. Even with this explicit control over resources, the finalizer becomes a
    // safeguard to clean up resources if the call to the Dispose method failed.
    
    class Car
    {
        ~Car()  // finalizer
        {
            // cleanup statements...
        }
    }
    
    // A finalizer can also be implemented as an expression body definition, as the following example shows.
    public class Destroyer
    {
        public override string ToString() => GetType().Name;

        ~Destroyer() => Console.WriteLine($"The {ToString()} destructor is executing.");
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}