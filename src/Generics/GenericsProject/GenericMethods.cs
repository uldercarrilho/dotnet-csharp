namespace GenericsProject
{
    public class GenericMethods
    {
        // A generic method is a method that is declared with type parameters, as follows:
        static void Swap<T>(ref T lhs, ref T rhs)
        {
            T temp;
            temp = lhs;
            lhs = rhs;
            rhs = temp;
        }
        
        public static void TestSwap()
        {
            int a = 1;
            int b = 2;

            // The following code example shows one way to call the method by using int for the type argument:
            Swap<int>(ref a, ref b);
            // You can also omit the type argument and the compiler will infer it.
            Swap(ref a, ref b);
        }
    }
    
    // The same rules for type inference apply to static methods and instance methods.
    // The compiler can infer the type parameters based on the method arguments you pass in;
    // it cannot infer the type parameters only from a constraint or return value.
    // Therefore type inference does not work with methods that have no parameters.
    // Type inference occurs at compile time before the compiler tries to resolve overloaded method signatures.
    // The compiler applies type inference logic to all generic methods that share the same name.
    // In the overload resolution step, the compiler includes only those generic methods on which type inference succeeded.
    
    // Within a generic class, non-generic methods can access the class-level type parameters, as follows:
    class SampleClass<T>
    {
        void Swap(ref T lhs, ref T rhs) { }
        
        // Use constraints to enable more specialized operations on type parameters in methods.
        // This version of Swap<T>, now named SwapIfGreater<T>, can only be used with type arguments that implement IComparable<T>.
        void SwapIfGreater<T>(ref T lhs, ref T rhs) where T : System.IComparable<T>
        {
            T temp;
            if (lhs.CompareTo(rhs) > 0)
            {
                temp = lhs;
                lhs = rhs;
                rhs = temp;
            }
        }
        
        // Generic methods can be overloaded on several type parameters. 
        void DoWork() { }
        void DoWork<T>() { }
        void DoWork<T, U>() { }
    }

    // If you define a generic method that takes the same type parameters as the containing class, the compiler generates
    // warning CS0693 because within the method scope, the argument supplied for the inner T hides the argument supplied
    // for the outer T. If you require the flexibility of calling a generic class method with type arguments other than
    // the ones provided when the class was instantiated, consider providing another identifier for the type parameter
    // of the method, as shown in GenericList5<T> in the following example.
    class GenericList4<T>
    {
        // CS0693
        void SampleMethod<T>() { }
    }

    class GenericList5<T>
    {
        //No warning
        void SampleMethod<U>() { }
    }
}