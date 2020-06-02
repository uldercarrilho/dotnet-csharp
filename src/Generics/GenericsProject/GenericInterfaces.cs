using System;
using System.Collections;
using System.Collections.Generic;

namespace GenericsProject
{
    // It is often useful to define interfaces either for generic collection classes, or for the generic classes that represent
    // items in the collection. The preference for generic classes is to use generic interfaces, such as IComparable<T> rather
    // than IComparable, in order to avoid boxing and unboxing operations on value types. The .NET Framework class library
    // defines several generic interfaces for use with the collection classes in the System.Collections.Generic namespace.

    // When an interface is specified as a constraint on a type parameter, only types that implement the interface can be used.

    //Type parameter T in angle brackets.
    public class GenericList3<T> : IEnumerable<T>
    {
        protected Node head;
        protected Node current = null;

        // Nested class is also generic on T
        protected class Node
        {
            public Node next;
            private T data;  //T as private member datatype

            // //T used in non-generic constructor
            public Node(T t) => (next, data) = (null, t); 

            public Node Next
            {
                get { return next; }
                set { next = value; }
            }

            public T Data  //T as return type of property
            {
                get { return data; }
                set { data = value; }
            }
        }

        public GenericList3() => head = null;

        public void AddHead(T t)  //T as method parameter type
        {
            Node n = new Node(t);
            n.Next = head;
            head = n;
        }

        // Implementation of the iterator
        public IEnumerator<T> GetEnumerator()
        {
            Node current = head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }

        // IEnumerable<T> inherits from IEnumerable, therefore this class must implement both the generic and non-generic
        // versions of GetEnumerator. In most cases, the non-generic method can simply call the generic method.
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class SortedList<T> : GenericList3<T> where T : System.IComparable<T>
    {
        // A simple, unoptimized sort algorithm that orders list elements from lowest to highest:
        public void BubbleSort()
        {
            if (null == head || null == head.Next)
            {
                return;
            }
            bool swapped;

            do
            {
                Node previous = null;
                Node current = head;
                swapped = false;

                while (current.next != null)
                {
                    //  Because we need to call this method, the SortedList
                    //  class is constrained on IEnumerable<T>
                    if (current.Data.CompareTo(current.next.Data) > 0)
                    {
                        Node tmp = current.next;
                        current.next = current.next.next;
                        tmp.next = current;

                        if (previous == null)
                        {
                            head = tmp;
                        }
                        else
                        {
                            previous.next = tmp;
                        }
                        previous = tmp;
                        swapped = true;
                    }
                    else
                    {
                        previous = current;
                        current = current.next;
                    }
                }
            } while (swapped);
        }
    }

    // A simple class that implements IComparable<T> using itself as the type argument.
    // This is a common design pattern in objects that are stored in generic lists.
    public class Person : System.IComparable<Person>
    {
        string name;
        int age;

        public Person(string s, int i) => (name, age) = (s, i);
        public int CompareTo(Person p) => age - p.age;
        public override string ToString() => name + ":" + age;
        public bool Equals(Person p) => (this.age == p.age);
    }

    public class GenericInterfaces
    {
        public static void Run()
        {
            //Declare and instantiate a new generic SortedList class. Person is the type argument.
            SortedList<Person> list = new SortedList<Person>();

            //Create name and age values to initialize Person objects.
            string[] names = {"Franscoise", "Bill", "Li", "Sandra", "Gunnar", "Alok", "Hiroyuki", "Maria", "Alessandro", "Raul"};
            int[] ages = { 45, 19, 28, 23, 18, 9, 108, 72, 30, 35 };

            //Populate the list.
            for (int x = 0; x < 10; x++)
            {
                list.AddHead(new Person(names[x], ages[x]));
            }

            //Print out unsorted list.
            foreach (Person p in list)
            {
                Console.WriteLine(p.ToString());
            }
            Console.WriteLine("Done with unsorted list");

            //Sort the list.
            list.BubbleSort();

            //Print out sorted list.
            foreach (Person p in list)
            {
                Console.WriteLine(p.ToString());
            }
            Console.WriteLine("Done with sorted list");
        }
    }

    // Multiple interfaces can be specified as constraints on a single type, as follows:
    class Stack<T> where T : System.IComparable<T>, IEnumerable<T> { }
    
    // An interface can define more than one type parameter, as follows:
    interface IDictionary<K, V> { }

    // The rules of inheritance that apply to classes also apply to interfaces:
    interface IMonth<T> { }
    interface IJanuary     : IMonth<int> { }  //No error
    interface IFebruary<T> : IMonth<int> { }  //No error
    interface IMarch<T>    : IMonth<T> { }    //No error
    //interface IApril<T>  : IMonth<T, U> {}  //Error

    // Generic interfaces can inherit from non-generic interfaces if the generic interface is contravariant, which means
    // it only uses its type parameter as a return value. In the .NET Framework class library, IEnumerable<T> inherits from
    // IEnumerable because IEnumerable<T> only uses T in the return value of GetEnumerator and in the Current property getter.
    
    // Concrete classes can implement closed constructed interfaces, as follows:
    interface IBaseInterface<T> { }
    class SampleClass : IBaseInterface<string> { }

    // Generic classes can implement generic interfaces or closed constructed interfaces as long as the class parameter
    // list supplies all arguments required by the interface, as follows:
    interface IBaseInterface1<T> { }
    interface IBaseInterface2<T, U> { }
    class SampleClass1<T> : IBaseInterface1<T> { }          //No error
    class SampleClass2<T> : IBaseInterface2<T, string> { }  //No error
    
    // The rules that control method overloading are the same for methods within generic classes, generic structs, or generic interfaces.
}