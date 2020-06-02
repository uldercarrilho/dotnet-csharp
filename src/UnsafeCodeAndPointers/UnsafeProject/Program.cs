using System;

namespace UnsafeProject
{
    // To maintain type safety and security, C# does not support pointer arithmetic, by default.
    // However, by using the unsafe keyword, you can define an unsafe context in which pointers can be used.
    
    // In the common language runtime (CLR), unsafe code is referred to as unverifiable code.
    // Unsafe code in C# is not necessarily dangerous; it is just code whose safety cannot be verified by the CLR.
    // The CLR will therefore only execute unsafe code if it is in a fully trusted assembly.
    // If you use unsafe code, it is your responsibility to ensure that your code does not introduce security risks or pointer errors.
    
    // Unsafe code has the following properties:
    //     Methods, types, and code blocks can be defined as unsafe.
    //     In some cases, unsafe code may increase an application's performance by removing array bounds checks.
    //     Unsafe code is required when you call native functions that require pointers.
    //     Using unsafe code introduces security and stability risks.
    //     The code that contains unsafe blocks must be compiled with the -unsafe compiler option.

    // In safe code, a C# struct that contains an array does not contain the array elements. Instead, the struct contains
    // a reference to the elements. You can embed an array of fixed size in a struct when it is used in an unsafe code block.
    public unsafe struct Record
    {
        // In C#, you can use the fixed statement to create a buffer with a fixed size array in a data structure.
        // Fixed size buffers are useful when you write methods that interop with data sources from other languages or platforms.
        // The fixed array can take any attributes or modifiers that are allowed for regular struct members.
        // The only restriction is that the array type must be bool, byte, char, short, int, long, sbyte, ushort, uint, ulong, float, or double.
        private fixed char name[30];
    }
    
    // Size of the following struct doesn't depend on the number of elements in the array, since pathName is a reference:
    public struct PathArray
    {
        public char[] pathName;
        private int reserved;
    }
    
    internal unsafe struct Buffer
    {
        // The size of the 128 element char array is 256 bytes.
        // Fixed size char buffers always take two bytes per character, regardless of the encoding.
        // This is true even when char buffers are marshaled to API methods or structs with CharSet = CharSet.Auto or CharSet = CharSet.Ansi
        public fixed char fixedBuffer[128];
        
        // Another common fixed-size array is the bool array.
        // The elements in a bool array are always one byte in size. bool arrays are not appropriate for creating bit arrays or buffers.
        public fixed bool bitBuffer[128];
        
        // Fixed size buffers are compiled with the System.Runtime.CompilerServices.UnsafeValueTypeAttribute, which instructs
        // the common language runtime (CLR) that a type contains an unmanaged array that can potentially overflow. This is
        // similar to memory created using stackalloc, which automatically enables buffer overrun detection features in the CLR.
    }

    internal unsafe class Example
    {
        public Buffer buffer = default;
    }
   
    class Program
    {
        static void Main(string[] args)
        {
            AccessEmbeddedArray();
        }
        
        private static void AccessEmbeddedArray()
        {
            var example = new Example();

            unsafe
            {
                // Pin the buffer to a fixed location in memory.
                fixed (char* charPtr = example.buffer.fixedBuffer)
                {
                    *charPtr = 'A';
                }
                // Access safely through the index:
                char c = example.buffer.fixedBuffer[0];
                Console.WriteLine(c);

                // Modify through the index:
                example.buffer.fixedBuffer[0] = 'B';
                Console.WriteLine(example.buffer.fixedBuffer[0]);
            }
        }
    }
    
    // Fixed size buffers differ from regular arrays in the following ways:
    //     May only be used in an unsafe context.
    //     May only be instance fields of structs.
    //     They're always vectors, or one-dimensional arrays.
    //     The declaration should include the length, such as fixed char id[8]. You cannot use fixed char id[].
    
    // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/unsafe-code-pointers/
    // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/unsafe-code-pointers/pointer-types
}