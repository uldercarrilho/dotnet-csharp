using System;

namespace Expressions
{
    // An expression is a sequence of one or more operands and zero or more operators that
    // can be evaluated to a single value, object, method, or namespace.
    // Expressions can consist of a literal value, a method invocation, an operator and its operands, or a simple name.
    // Simple names can be the name of a variable, type member, method parameter, namespace or type.
    
    // Examples:
    // ((x < 10) && ( x > 5)) || ((x > 20) && (x < 25));
    // System.Convert.ToInt32("35");
    
    // However, although a namespace name is classified as an expression, it does not evaluate to a value and therefore
    // can never be the final result of any expression. You cannot pass a namespace name to a method parameter, or use
    // it in a new expression, or assign it to a variable. You can only use it as a sub-expression in a larger expression.
    // The same is true for types (as distinct from System.Type objects), method group names (as distinct from specific methods),
    // and event add and remove accessors.
    
    // Numeric expressions may cause overflows if the value is larger than the maximum value of the value's type.
    
    // The manner in which an expression is evaluated is governed by the rules of associativity and operator precedence.
    
    // Most expressions, except assignment expressions and method invocation expressions, must be embedded in a statement.
    
    // The same rules for expressions in general apply to query expressions. For more information, see LINQ.

    // Lambda expressions represent "inline methods" that have no name but can have input parameters and multiple statements.
    // They are used extensively in LINQ to pass arguments to methods.
    // Lambda expressions are compiled to either delegates or expression trees depending on the context in which they are used.
    
    // Expression trees enable expressions to be represented as data structures. They are used extensively by LINQ providers
    // to translate query expressions into code that is meaningful in some other context, such as a SQL database.
    
    // C# supports expression-bodied members, which allow you to supply a concise expression body definition for
    // methods, constructors, finalizers, properties, and indexers.

    
    class Program
    {
        static void Main(string[] args)
        {
            // Invocation expressions
            // A method invocation requires the name of the method, either as a name as in this example, or
            // as the result of another expression, followed by parenthesis and any method parameters.
            // Method invocations and delegate invocations evaluate to the return value of the method,
            // if the method returns a value. Methods that return void cannot be used in place of a value in an expression.
            Example1();
        }

        private static void Example1()
        {
            // Both i and s are simple names that identify local variables.
            // Both 5 and "Hello World" are literal values.
            int i = 5;
            string s = "Hello World";
        }
    }
}