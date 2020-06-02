using System;
using System.Linq;

namespace IndexersProject
{
    // Indexers allow instances of a class or struct to be indexed just like arrays.
    // The indexed value can be set or retrieved without explicitly specifying a type or instance member.
    // Indexers resemble properties except that their accessors take parameters.
    
    // Indexers are a syntactic convenience that enable you to create a class, struct, or interface that
    // client applications can access just as an array. Indexers are most frequently implemented in types
    // whose primary purpose is to encapsulate an internal collection or array.
    
    // It is common for an indexer's get or set accessor to consist of a single statement that either returns or sets a value.
    // Expression-bodied members provide a simplified syntax to support this scenario.

    // Indexers enable objects to be indexed in a similar manner to arrays.
    // A get accessor returns a value. A set accessor assigns a value.
    // The this keyword is used to define the indexer.
    // The value keyword is used to define the value being assigned by the set indexer.
    // Indexers do not have to be indexed by an integer value; it is up to you how to define the specific look-up mechanism.
    // Indexers can be overloaded.
    // Indexers can have more than one formal parameter, for example, when accessing a two-dimensional array.
    // The type of an indexer and the type of its parameters must be at least as accessible as the indexer itself. 
    
    // The signature of an indexer consists of the number and types of its formal parameters. It doesn't include the indexer type
    // or the names of the formal parameters. If you declare more than one indexer in the same class, they must have different signatures.

    // An indexer value is not classified as a variable; therefore, you cannot pass an indexer value as a ref or out parameter.

    class SampleCollection<T>
    {
        // Declare an array to store the data elements.
        private T[] arr = new T[100];

        // Starting with C# 6, a read-only indexer can be implemented as an expression-bodied member.
        public T this[string name] => arr[Array.FindIndex(arr, 0, arr.Length, obj => obj.Equals(name))];

        // Define the indexer to allow client code to use [] notation.
        public T this[int i]
        {
            get => arr[i];
            set => arr[i] = value;
        }
    }

    class SampleCollection2<T>
    {
        // Declare an array to store the data elements.
        private T[] arr = new T[100];

        // To provide the indexer with a name that other languages can use, use System.Runtime.CompilerServices.IndexerNameAttribute
        // This indexer will have the name TheItem. Not providing the name attribute would make Item the default name.
        // Indicates the name by which an indexer is known in programming languages that do not support indexers directly.
        [System.Runtime.CompilerServices.IndexerName("TheItem")]  
        public T this[int i]
        {
            get => arr[i];
            set => arr[i] = value;
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            var stringCollection = new SampleCollection<string>();
            stringCollection[0] = "Hello, World";
            Console.WriteLine(stringCollection[0]);
        }
    }
    
    
    // INDEXERS IN INTERFACES
    
    // Indexers can be declared on an interface. Accessors of interface indexers differ from the accessors of class indexers in the following ways:
    //     Interface accessors do not use modifiers.
    //     An interface accessor typically does not have a body.
    
    // The purpose of the accessor is to indicate whether the indexer is read-write, read-only, or write-only.
    // You may provide an implementation for an indexer defined in an interface, but this is rare.
    // Indexers typically define an API to access data fields, and data fields cannot be defined in an interface.
    // Indexer on an interface:
    public interface IIndexInterface
    {
        int this[int index] { get; set; }
        string this[string name] { get; }
    }
    class IndexerClass : IIndexInterface
    {
        private int[] arr = new int[100];
        public int this[int index]   // indexer declaration
        {
            get => arr[index];
            set => arr[index] = value;
        }

        // However, the fully qualified name is only needed to avoid ambiguity when
        // the class is implementing more than one interface with the same indexer signature.
        string IIndexInterface.this[string name] => throw new NotImplementedException();
    }
    
    // Indexers are like properties. Except for the differences shown in the following table,
    // all the rules that are defined for property accessors apply to indexer accessors also.
/*
PROPERTY	                                                INDEXER

Allows methods to be called as if they were public data     Allows elements of an internal collection of an object to be  
members.	                                                accessed by using array notation on the object itself.

Accessed through a simple name.	                            Accessed through an index.

Can be a static or an instance member.	                    Must be an instance member.

A get accessor of a property has no parameters.	            A get accessor of an indexer has the same formal parameter
                                                            list as the indexer. 

A set accessor of a property contains the implicit          A set accessor of an indexer has the same formal parameter  
value parameter.	                                        list as the indexer, and also to the value parameter.

Supports shortened syntax with Auto-Implemented             Supports expression bodied members for get only indexers. 
Properties.
*/
}