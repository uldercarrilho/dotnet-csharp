using System;

namespace Polymorphism
{
    // When a derived class inherits from a base class, it gains all the methods, fields, properties, and events of the base class.
    // The designer of the derived class can different choices for the behavior of virtual methods:
    //     The derived class may override virtual members in the base class, defining new behavior.
    //     The derived class inherit the closest base class method without overriding it, preserving the existing behavior
    //         but enabling further derived classes to override the method.
    //     The derived class may define new non-virtual implementation of those members that hide the base class implementations.
    
    // A derived class can override a base class member only if the base class member is declared as virtual or abstract.
    // The derived member must use the override keyword to explicitly indicate that the method is intended to participate in virtual invocation.
    
    // Fields cannot be virtual; only methods, properties, events, and indexers can be virtual. When a derived class overrides
    // a virtual member, that member is called even when an instance of that class is being accessed as an instance of the base class.
    
    public class BaseClass
    {
        public virtual void DoWork(int value) { }
        public virtual void AnotherMethod() => Console.Out.WriteLine("Base class");
        public virtual int WorkProperty
        {
            get { return 0; }
        }
    }
    public class DerivedClass : BaseClass
    {
        public void DoWork(double param) { }
        public override void DoWork(int value) { }
        public virtual void AnotherMethod()
        {
            // A derived class that has replaced or overridden a method or property can still access the
            // method or property on the base class using the base keyword.
            base.AnotherMethod();
            Console.Out.WriteLine("Derived class");
            
            // It is recommended that virtual members use base to call the base class implementation of that member in
            // their own implementation. Letting the base class behavior occur enables the derived class to concentrate
            // on implementing behavior specific to the derived class. If the base class implementation is not called,
            // it is up to the derived class to make their behavior compatible with the behavior of the base class.
        }
        public override int WorkProperty
        {
            get { return 0; }
        }
    }
    
    
    // HIDE BASE CLASS MEMBERS WITH NEW MEMBERS
    
    // If you want your derived class to have a member with the same name as a member in a base class, you can use the
    // new keyword to hide the base class member. The new keyword is put before the return type of a class member that is being replaced.
    
    public class Base
    {
        public void DoWork() { WorkField++; }
        public int WorkField;
        public int WorkProperty
        {
            get { return 0; }
        }
    }

    public class Derived : Base
    {
        public new void DoWork() { WorkField++; }
        public new int WorkField;
        public new int WorkProperty
        {
            get { return 0; }
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            // Hidden base class members may be accessed from client code by casting the instance of the derived class to an instance of the base class.
            Derived derived = new Derived();
            derived.DoWork();  // Calls the new method.

            Base basic = (Base)derived;
            basic.DoWork();  // Calls the old method.
            
            int val = 5;
            DerivedClass d = new DerivedClass();
            d.DoWork(val);  // Calls DoWork(double).
            ((BaseClass) d).DoWork(val);  // Calls DoWork(int) on Derived.
        }
    }
    
    // Virtual members remain virtual, regardless of how many classes have been declared between the virtual member and
    // the class that originally declared it. If class A declares a virtual member, and class B derives from A, and
    // class C derives from B, class C inherits the virtual member, and may override it,
    // regardless of whether class B declared an override for that member.
    public class A
    {
        public virtual void DoWork() { }
    }
    public class B : A
    {
        public override void DoWork() { }
    }
    // A derived class can stop virtual inheritance by declaring an override as sealed.
    // Stopping inheritance requires putting the sealed keyword before the override keyword in the class member declaration.
    public class C : B
    {
        public sealed override void DoWork() { }
    }
    // Sealed methods can be replaced by derived classes by using the new keyword
    public class D : C
    {
        public new void DoWork() { }
    }
    // In this case, if DoWork is called on D using a variable of type D, the new DoWork is called.
    // If a variable of type C, B, or A is used to access an instance of D, a call to DoWork will follow
    // the rules of virtual inheritance, routing those calls to the implementation of DoWork on class C.
}