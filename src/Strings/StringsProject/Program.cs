using System;
using System.Collections.Generic;
using System.Text;

namespace StringsProject
{
    // A string is an object of type String whose value is text.
    // Internally, the text is stored as a sequential read-only collection of Char objects. There is no null-terminating
    // character at the end of a C# string; therefore a C# string can contain any number of embedded null characters ('\0').
    // The Length property of a string represents the number of Char objects it contains, not the number of Unicode characters.
    // To access the individual Unicode code points in a string, use the StringInfo object.
    
    // In C#, the string keyword is an alias for String. Therefore, String and string are equivalent, and you can use
    // whichever naming convention you prefer. The String class provides many methods for safely creating, manipulating,
    // and comparing strings. In addition, the C# language overloads some operators to simplify common string operations.
    
    class Program
    {
        static void Main(string[] args)
        {
            // You can declare and initialize strings in various ways, as shown in the following example:
            // Declare without initializing.
            string message1;

            // Initialize to null.
            string message2 = null;

            // Initialize as an empty string.
            // Use the Empty constant instead of the literal "".
            string message3 = String.Empty;

            // Initialize with a regular string literal.
            string oldPath = "c:\\Program Files\\Microsoft Visual Studio 8.0";

            // Initialize with a verbatim string literal.
            string newPath = @"c:\Program Files\Microsoft Visual Studio 9.0";

            // Use System.String if you prefer.
            System.String greeting = "Hello World!";

            // In local variables (i.e. within a method body) you can use implicit typing.
            var temp = "I'm still a strongly-typed System.String!";

            // Use a const string to prevent 'message4' from being used to store another string value.
            const string message4 = "You can't get rid of me!";

            // Use the String constructor only when creating a string from a char*, char[], or sbyte*
            char[] letters = { 'A', 'B', 'C' };
            string alphabet = new string(letters);
            // Note that you do not use the new operator to create a string object except when initializing the string with an array of chars.

            // Initialize a string with the Empty constant value to create a new String object whose string is of zero length.
            // The string literal representation of a zero-length string is "". By initializing strings with
            // the Empty value instead of null, you can reduce the chances of a NullReferenceException occurring.
            // Use the static IsNullOrEmpty(String) method to verify the value of a string before you try to access it.
            
            // String objects are immutable: they cannot be changed after they have been created. All of the String methods
            // and C# operators that appear to modify a string actually return the results in a new string object.
            string s1 = "A string is more ";
            string s2 = "than the sum of its chars.";
            // Concatenate s1 and s2.
            // This actually creates a new string object and stores it in s1, releasing the reference to the original object.
            s1 += s2;
            Console.WriteLine(s1); // Output: A string is more than the sum of its chars.
            
            // Because a string "modification" is actually a new string creation, you must use caution when you create references
            // to strings. If you create a reference to a string, and then "modify" the original string, the reference will
            // continue to point to the original object instead of the new object that was created when the string was modified.
            string t1 = "Hello ";
            string t2 = t1;
            t1 += "World";
            Console.WriteLine(t2); //Output: Hello
            
            // Use regular string literals when you must embed escape characters provided by C#
            string columns = "Column 1\tColumn 2\tColumn 3";
            //Output: Column 1        Column 2        Column 3

            string rows = "Row 1\r\nRow 2\r\nRow 3";
            /* Output:
              Row 1
              Row 2
              Row 3
            */

            string title = "\"The \u00C6olean Harp\", by Samuel Taylor Coleridge";
            //Output: "The Æolean Harp", by Samuel Taylor Coleridge
            
            // Use verbatim strings for convenience and better readability when the string text contains backslash characters 
            string filePath = @"C:\Users\scoleridge\Documents\";
            //Output: C:\Users\scoleridge\Documents\
            
            // At compile time, verbatim strings are converted to ordinary strings with all the same escape sequences.
            // Therefore, if you view a verbatim string in the debugger watch window, you will see the escape characters
            // that were added by the compiler, not the verbatim version from your source code.
            // For example, the verbatim string @"C:\files.txt" will appear in the watch window as "C:\\files.txt".

            // Because verbatim strings preserve new line characters as part of
            // the string text, they can be used to initialize multiline strings.
            string text = @"My pensive SARA ! thy soft cheek reclined
                Thus on mine arm, most soothing sweet it is
                To sit beside our Cot,...";
            /* Output:
            My pensive SARA ! thy soft cheek reclined
               Thus on mine arm, most soothing sweet it is
               To sit beside our Cot,...
            */

            //  Use double quotation marks to embed a quotation mark inside a verbatim string.
            string quote = @"Her name was ""Sara.""";
            //Output: Her name was "Sara."


            // A format string is a string whose contents are determined dynamically at runtime.
            // Format strings are created by embedding interpolated expressions or placeholders inside of braces within a string.
            // Everything inside the braces ({...}) will be resolved to a value and output as a formatted string at runtime.
            // There are two methods to create format strings: string interpolation and composite formatting.
            
            // Interpolated strings are identified by the $ special character and include interpolated expressions in braces.
            // Use string interpolation to improve the readability and maintainability of your code.
            // String interpolation achieves the same results as the String.Format method, but improves ease of use and inline clarity.
            // A string literal that begins with the $ character before its opening quotation mark character.
            // There can't be any spaces between the $ symbol and the quotation mark character.
            // You can put any C# expression that returns a value (including null) inside the braces.
            // To include a brace, "{" or "}", in the text produced by an interpolated string, use two braces, "{{" or "}}".
            var jh = (firstName: "Jupiter", lastName: "Hammon", born: 1711, published: 1761);
            Console.WriteLine($"{jh.firstName} {jh.lastName} was an African American poet born in {jh.born}.");
            Console.WriteLine($"He was first published in {jh.published} at the age of {jh.published - jh.born}.");
            Console.WriteLine($"He'd be over {Math.Round((2018d - jh.born) / 100d) * 100d} years old today.");
            // Output:
            // Jupiter Hammon was an African American poet born in 1711.
            // He was first published in 1761 at the age of 50.
            // He'd be over 300 years old today.
            
            // String interpolation lets you specify format strings that control the formatting of particular types.
            // You specify a format string by following the interpolation expression with a colon (":") and the format string.
            var date = DateTime.Now;
            var price = 10.5m;
            Console.WriteLine($"Today is {date:D}");
            Console.WriteLine($"Now is {date:hh:mm:ss t z}");
            Console.WriteLine($"My price is {price:C2}");
            // https://docs.microsoft.com/en-us/dotnet/standard/base-types/formatting-types#format-strings-and-net-types
            // https://docs.microsoft.com/en-us/dotnet/standard/base-types/formatting-types
            
            // In addition to controlling formatting, you can also control the field width and alignment of the formatted
            // strings that are included in the result string. Ordinarily, when the result of an interpolated string expression
            // is formatted to string, that string is included in a result string without leading or trailing spaces.
            // Particularly when you work with a set of data, being able to control a field width and text alignment
            // helps to produce a more readable output.
            
            // The item names are left-aligned, and their quantities are right-aligned. You specify the alignment by adding
            // a comma (",") after an interpolation expression and designating the minimum field width. If the specified
            // value is a positive number, the field is right-aligned. If it is a negative number, the field is left-aligned.
            var inventory = new Dictionary<string, int>()
            {
                ["hammer, ball pein"] = 18,
                ["hammer, cross pein"] = 5,
                ["screwdriver, Phillips #2"] = 14
            };

            Console.WriteLine($"Inventory on {DateTime.Now:d}");
            Console.WriteLine();
            Console.WriteLine($"|{"Item",-25}|{"Quantity",10}|");
            foreach (var item in inventory)
                Console.WriteLine($"|{item.Key,-25}|{item.Value,10}|");
            /*
            Output
                Inventory on 06/01/2020
 
                |Item                     |  Quantity|
                |hammer, ball pein        |        18|
                |hammer, cross pein       |         5|
                |screwdriver, Phillips #2 |        14|
            */
            
            // You can combine an alignment specifier and a format string for a single interpolation expression.
            // To do that, specify the alignment first, followed by a colon and the format string.
            Console.WriteLine($"[{DateTime.Now,-20:d}] Hour [{DateTime.Now,-10:HH}] [{1063.342,15:N2}] feet");
            // [06/01/2020          ] Hour [21        ] [       1,063.34] feet
            
            
            // Composite Formatting
            // The String.Format utilizes placeholders in braces to create a format string.
            var pw = (firstName: "Phillis", lastName: "Wheatley", born: 1753, published: 1773);
            Console.WriteLine("{0} {1} was an African American poet born in {2}.", pw.firstName, pw.lastName, pw.born);
            Console.WriteLine("She was first published in {0} at the age of {1}.", pw.published, pw.published - pw.born);
            Console.WriteLine("She'd be over {0} years old today.", Math.Round((2018d - pw.born) / 100d) * 100d);
            // Output:
            // Phillis Wheatley was an African American poet born in 1753.
            // She was first published in 1773 at the age of 20.
            // She'd be over 300 years old today.
            
            // A substring is any sequence of characters that is contained in a string.
            // Use the Substring method to create a new string from a part of the original string.
            // You can search for one or more occurrences of a substring by using the IndexOf method.
            // Use the Replace method to replace all occurrences of a specified substring with a new string.
            // Like the Substring method, Replace actually returns a new string and does not modify the original string.
            string s3 = "Visual C# Express";
            Console.WriteLine(s3.Substring(7, 2));
            // Output: "C#"
            Console.WriteLine(s3.Replace("C#", "Basic"));
            // Output: "Visual Basic Express"
            // Index values are zero-based
            int index = s3.IndexOf("C");
            // index = 7
            
            // You can use array notation with an index value to acquire read-only access to individual characters
            Console.WriteLine(s3[1]);
        }

        // If the String methods do not provide the functionality that you must have to modify individual characters in
        // a string, you can use a StringBuilder object to modify the individual chars "in-place", and then create a new
        // string to store the results by using the StringBuilder methods.
        static void Example1()
        {
            string question = "hOW DOES mICROSOFT wORD DEAL WITH THE cAPS lOCK KEY?";
            StringBuilder sb = new StringBuilder(question);

            for (int j = 0; j < sb.Length; j++)
            {
                if (Char.IsLower(sb[j]) == true)
                    sb[j] = Char.ToUpper(sb[j]);
                else if (Char.IsUpper(sb[j]) == true)
                    sb[j] = Char.ToLower(sb[j]);
            }
            // Store the new string.
            string corrected = sb.ToString();
            Console.WriteLine(corrected);
            // Output: How does Microsoft Word deal with the Caps Lock key? 
        }

        static void Example2()
        {
            // An empty string is an instance of a System.String object that contains zero characters.
            // Empty strings are used often in various programming scenarios to represent a blank text field.
            // You can call methods on empty strings because they are valid System.String objects.
            
            // By contrast, a null string does not refer to an instance of a System.String object and any attempt to
            // call a method on a null string causes a NullReferenceException. However, you can use null strings in
            // concatenation and comparison operations with other strings.

            string str = "hello";
            string nullStr = null;
            string emptyStr = String.Empty;

            string tempStr = str + nullStr;
            // Output of the following line: hello
            Console.WriteLine(tempStr);

            bool b = (emptyStr == nullStr);
            // Output of the following line: False
            Console.WriteLine(b);

            // The following line creates a new empty string.
            string newStr = emptyStr + nullStr;

            // Null strings and empty strings behave differently. The following
            // two lines display 0.
            Console.WriteLine(emptyStr.Length);
            Console.WriteLine(newStr.Length);
            // The following line raises a NullReferenceException.
            //Console.WriteLine(nullStr.Length);

            // The null character can be displayed and counted, like other chars.
            string s1 = "\x0" + "abc";
            string s2 = "abc" + "\x0";
            // Output of the following line: * abc*
            Console.WriteLine("*" + s1 + "*");
            // Output of the following line: *abc *
            Console.WriteLine("*" + s2 + "*");
            // Output of the following line: 4
            Console.WriteLine(s2.Length);
        }

        // String operations in .NET are highly optimized and in most cases do not significantly impact performance.
        // However, in some scenarios such as tight loops that are executing many hundreds or thousands of times, string
        // operations can affect performance. The StringBuilder class creates a string buffer that offers better
        // performance if your program performs many string manipulations. The StringBuilder string also enables you to
        // reassign individual characters, something the built-in string data type does not support.
        static void Example3()
        {
            StringBuilder sb = new StringBuilder("Rat: the ideal pet");
            sb[0] = 'C';
            Console.WriteLine(sb.ToString());
            Console.ReadLine();
            //Outputs Cat: the ideal pet
            
            var sbNumbers = new StringBuilder();
            // Create a string composed of numbers 0 - 9
            for (int i = 0; i < 10; i++)
            {
                sbNumbers.Append(i.ToString());
            }
            Console.WriteLine(sbNumbers);  // displays 0123456789

            // Copy one character of the string (not possible with a System.String)
            sbNumbers[0] = sbNumbers[9];
            Console.WriteLine(sb);  // displays 9123456789
        }
        
        // Because the String type implements IEnumerable<T>, you can use the extension methods defined in the Enumerable class on strings.
        // To avoid visual clutter, these methods are excluded from IntelliSense for the String type, but they are available nevertheless.
        // You can also use LINQ query expressions on strings.
        
        // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/strings/#related-topics
    }
}