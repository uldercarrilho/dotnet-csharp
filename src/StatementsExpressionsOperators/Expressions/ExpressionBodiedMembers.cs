using System;

namespace Expressions
{
    public class ExpressionBodiedMembers
    {
        // Expression body definitions let you provide a member's implementation in a very concise, readable form.
        // You can use an expression body definition whenever the logic for any supported member, such as a method or property,
        // consists of a single expression. An expression body definition has the following general syntax:
        // member => expression;

        public static void Example1()
        {
            Person p = new Person("Mandy", "Dejesus");
            Console.WriteLine(p);
            p.DisplayName();
        }
    }
    
    public class Person
    {
        // An expression body definition for a constructor typically consists of a single assignment expression or
        // a method call that handles the constructor's arguments or initializes instance state.
        public Person() => _fname = "Anonymous";

        public Person(string firstName, string lastName)
        {
            _fname = firstName;
            _lname = lastName;
        }
        
        // An expression body definition for a finalizer typically contains cleanup statements, such as statements that release unmanaged resources.
        ~Person() => Console.WriteLine($"The {ToString()} destructor is executing.");

        private string _fname;
        private string _lname;

        // An expression-bodied method consists of a single expression that returns a value whose type matches the
        // method's return type, or, for methods that return void, that performs some operation.
        public override string ToString() => $"{_fname} {_lname}".Trim();
        public void DisplayName() => Console.WriteLine(ToString());

        // expression body definition to implement a read-only property
        public string FirstName => _fname;

        // expression body definitions to implement property get and set accessors
        public string LastName
        {
            get => _lname;
            set => _lname = value;
        }
    }
    
    public class Sports
    {
        private readonly string[] _types = { "Baseball", "Basketball", "Football", "Hockey", "Soccer", "Tennis", "Volleyball" };

        // Like with properties, indexer get and set accessors consist of expression body definitions if the get accessor
        // consists of a single expression that returns a value or the set accessor performs a simple assignment.
        public string this[int i]
        {
            get => _types[i];
            set => _types[i] = value;
        }
    }
}