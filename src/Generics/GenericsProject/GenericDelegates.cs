using System;

namespace GenericsProject
{
    public class GenericDelegates
    {
        // A delegate can define its own type parameters. Code that references the generic delegate can specify the type
        // argument to create a closed constructed type, just like when instantiating a generic class or calling a generic method
        public delegate void Del<T>(T item);
        public static void Notify(int i) { }

        public static void Run()
        {
            Del<int> m1 = new Del<int>(Notify);
            // C# version 2.0 has a new feature called method group conversion, which applies to concrete as well as
            // generic delegate types, and enables you to write the previous line with this simplified syntax:
            Del<int> m2 = Notify;
        }
        
        private static void DoWork(float[] items) { }
        public static void TestStack()
        {
            // Code that references the delegate must specify the type argument of the containing class, as follows:
            Stack2<float> s = new Stack2<float>();
            Stack2<float>.StackDelegate d = DoWork;
        }
    }
    
    // Delegates defined within a generic class can use the generic class type parameters in the same way that class methods do.
    class Stack2<T>
    {
        T[] items;
        int index;

        public delegate void StackDelegate(T[] items);
    }

    // Generic delegates are especially useful in defining events based on the typical design pattern because the sender
    // argument can be strongly typed and no longer has to be cast to and from Object.
    delegate void StackEventHandler<T, U>(T sender, U eventArgs);

    class Stack3<T>
    {
        public class StackEventArgs : EventArgs { }
        public event StackEventHandler<Stack3<T>, StackEventArgs> stackEvent;

        protected virtual void OnStackChanged(StackEventArgs a)
        {
            stackEvent(this, a);
        }
    }

    class SampleClassStack3
    {
        public void HandleStackChange<T>(Stack3<T> stack, Stack3<T>.StackEventArgs args) { }
    }

    public class TestStack3
    {
        public static void Test()
        {
            Stack3<double> s = new Stack3<double>();
            SampleClassStack3 o = new SampleClassStack3();
            s.stackEvent += o.HandleStackChange;
        }
    }
}