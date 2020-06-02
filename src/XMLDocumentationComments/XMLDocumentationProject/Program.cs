using System;

// Documentation comments cannot be applied to a namespace.
namespace XMLDocumentationProject
{
    // In C#, you can create documentation for your code by including XML elements in special comment fields
    // (indicated by triple slashes) in the source code directly before the code block to which the comments refer.
    
    // If you want angle brackets to appear in the text of a documentation comment, use the HTML encoding of < and >
    // which is &lt; and &gt; respectively. This encoding is shown in the following example.

    /// <summary>
    ///  This class performs an important function.
    /// This property always returns a value &lt; 1.
    /// </summary>
    public class MyClass {}
    
    // When you compile with the -doc option, the compiler will search for all XML tags in the source code and create an
    // XML documentation file. To create the final documentation based on the compiler-generated file, you can create a
    // custom tool or use a tool such as DocFX or Sandcastle.

    // To refer to XML elements (for example, your function processes specific XML elements that you want to describe in
    // an XML documentation comment), you can use the standard quoting mechanism (< and >). To refer to generic identifiers
    // in code reference (cref) elements, you can use either the escape characters (for example, cref="List&lt;T&gt;") or
    // braces (cref="List{T}"). As a special case, the compiler parses the braces as angle brackets to make the documentation
    // comment less cumbersome to author when referring to generic identifiers.
    
    // The XML documentation comments are not metadata;
    // they are not included in the compiled assembly and therefore they are not accessible through reflection.
    
    class Program
    {
        static void Main(string[] args)
        {
            // mouse hover the class name
            var m = new MyClass();
        }
    }
    
    // ROBUST PROGRAMMING
    
    // XML documentation starts with ///. When you create a new project, the wizards put some starter /// lines in for you.
    // The processing of these comments has some restrictions:
    
    // The documentation must be well-formed XML. If the XML is not well-formed, a warning is generated and
    // the documentation file will contain a comment that says that an error was encountered.
    
    // Developers are free to create their own set of tags.
    // There is a recommended set of tags. Some of the recommended tags have special meanings:
    
    // The <param> tag is used to describe parameters.
    // If used, the compiler verifies that the parameter exists and that all parameters are described in the documentation.
    // If the verification fails, the compiler issues a warning.
    
    // The cref attribute can be attached to any tag to reference a code element.
    // The compiler verifies that this code element exists. If the verification fails, the compiler issues a warning.
    // The compiler respects any using statements when it looks for a type described in the cref attribute.
    
    // The <summary> tag is used by IntelliSense inside Visual Studio to display additional information about a type or member.
    
    
    // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/xmldoc/recommended-tags-for-documentation-comments
    

    /// <summary>
    /// Class level summary documentation goes here.
    /// </summary>
    /// <remarks>
    /// Longer comments can be associated with a type or member through
    /// the remarks tag.
    /// </remarks>
    public class TestClass : TestInterface
    {
        /// <summary>
        /// Store for the Name property.
        /// </summary>
        private string _name = null;

        /// <summary>
        /// The class constructor.
        /// </summary>
        public TestClass()
        {
            // TODO: Add Constructor Logic here.
        }

        /// <summary>
        /// Name property.
        /// </summary>
        /// <value>
        /// A value tag is used to describe the property value.
        /// </value>
        public string Name
        {
            get
            {
                if (_name == null)
                {
                    throw new System.Exception("Name is null");
                }
                return _name;
            }
        }

        /// <summary>
        /// Description for SomeMethod.
        /// </summary>
        /// <param name="s"> Parameter description for s goes here.</param>
        /// <seealso cref="System.String">
        /// You can use the cref attribute on any tag to reference a type or member
        /// and the compiler will check that the reference exists.
        /// </seealso>
        public void SomeMethod(string s)
        {
        }

        /// <summary>
        /// Some other method.
        /// </summary>
        /// <returns>
        /// Return values are described through the returns tag.
        /// </returns>
        /// <seealso cref="SomeMethod(string)">
        /// Notice the use of the cref attribute to reference a specific method.
        /// </seealso>
        public int SomeOtherMethod()
        {
            return 0;
        }

        public int InterfaceMethod(int n)
        {
            return n * n;
        }

        /// <summary>
        /// The entry point for the application.
        /// </summary>
        /// <param name="args"> A list of command line arguments.</param>
        static int Run(System.String[] args)
        {
            // TODO: Add code to start application here.
            return 0;
        }
    }

    /// <summary>
    /// Documentation that describes the interface goes here.
    /// </summary>
    /// <remarks>
    /// Details about the interface go here.
    /// </remarks>
    interface TestInterface
    {
        /// <summary>
        /// Documentation that describes the method goes here.
        /// </summary>
        /// <param name="n">
        /// Parameter n requires an integer argument.
        /// </param>
        /// <returns>
        /// The method returns an integer.
        /// </returns>
        int InterfaceMethod(int n);
    }
}

// The following examples show how the ID strings for a class and its members are generated:

namespace N
{
    /// <summary>
    /// Enter description here for class X.
    /// ID string generated is "T:N.X".
    /// </summary>
    public unsafe class X
    {
        /// <summary>
        /// Enter description here for the first constructor.
        /// ID string generated is "M:N.X.#ctor".
        /// </summary>
        public X() { }

        /// <summary>
        /// Enter description here for the second constructor.
        /// ID string generated is "M:N.X.#ctor(System.Int32)".
        /// </summary>
        /// <param name="i">Describe parameter.</param>
        public X(int i) { }

        /// <summary>
        /// Enter description here for field q.
        /// ID string generated is "F:N.X.q".
        /// </summary>
        public string q;

        /// <summary>
        /// Enter description for constant PI.
        /// ID string generated is "F:N.X.PI".
        /// </summary>
        public const double PI = 3.14;

        /// <summary>
        /// Enter description for method f.
        /// ID string generated is "M:N.X.f".
        /// </summary>
        /// <returns>Describe return value.</returns>
        public int f() { return 1; }

        /// <summary>
        /// Enter description for method bb.
        /// ID string generated is "M:N.X.bb(System.String,System.Int32@,System.Void*)".
        /// </summary>
        /// <param name="s">Describe parameter.</param>
        /// <param name="y">Describe parameter.</param>
        /// <param name="z">Describe parameter.</param>
        /// <returns>Describe return value.</returns>
        public int bb(string s, ref int y, void* z) { return 1; }

        /// <summary>
        /// Enter description for method gg.
        /// ID string generated is "M:N.X.gg(System.Int16[],System.Int32[0:,0:])".
        /// </summary>
        /// <param name="array1">Describe parameter.</param>
        /// <param name="array">Describe parameter.</param>
        /// <returns>Describe return value.</returns>
        public int gg(short[] array1, int[,] array) { return 0; }

        /// <summary>
        /// Enter description for operator.
        /// ID string generated is "M:N.X.op_Addition(N.X,N.X)".
        /// </summary>
        /// <param name="x">Describe parameter.</param>
        /// <param name="xx">Describe parameter.</param>
        /// <returns>Describe return value.</returns>
        public static X operator +(X x, X xx) { return x; }

        /// <summary>
        /// Enter description for property.
        /// ID string generated is "P:N.X.prop".
        /// </summary>
        public int prop { get { return 1; } set { } }

        /// <summary>
        /// Enter description for event.
        /// ID string generated is "E:N.X.d".
        /// </summary>
        public event D d;

        /// <summary>
        /// Enter description for property.
        /// ID string generated is "P:N.X.Item(System.String)".
        /// </summary>
        /// <param name="s">Describe parameter.</param>
        /// <returns></returns>
        public int this[string s] { get { return 1; } }

        /// <summary>
        /// Enter description for class Nested.
        /// ID string generated is "T:N.X.Nested".
        /// </summary>
        public class Nested { }

        /// <summary>
        /// Enter description for delegate.
        /// ID string generated is "T:N.X.D".
        /// </summary>
        /// <param name="i">Describe parameter.</param>
        public delegate void D(int i);

        /// <summary>
        /// Enter description for operator.
        /// ID string generated is "M:N.X.op_Explicit(N.X)~System.Int32".
        /// </summary>
        /// <param name="x">Describe parameter.</param>
        /// <returns>Describe return value.</returns>
        public static explicit operator int(X x) { return 1; }
    }
}