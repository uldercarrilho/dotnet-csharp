﻿using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace ExceptionsProject
{
    // The C# language's exception handling features help you deal with any unexpected or exceptional situations that
    // occur when a program is running. Exception handling uses the try, catch, and finally keywords to try actions that
    // may not succeed, to handle failures when you decide that it is reasonable to do so, and to clean up resources afterward.
    // Exceptions can be generated by the common language runtime (CLR), by .NET or third-party libraries, or by application code.
    // Exceptions are created by using the throw keyword.

    // In many cases, an exception may be thrown not by a method that your code has called directly, but by another method
    // further down in the call stack. When this happens, the CLR will unwind the stack, looking for a method with a catch
    // block for the specific exception type, and it will execute the first such catch block that if finds. If it finds no
    // appropriate catch block anywhere in the call stack, it will terminate the process and display a message to the user.
    
    // EXCEPTIONS OVERVIEW

    // Exceptions are types that all ultimately derive from System.Exception.
    // Use a try block around the statements that might throw exceptions.
    // Once an exception occurs in the try block, the flow of control jumps to the first associated exception handler
    //     that is present anywhere in the call stack. In C#, the catch keyword is used to define an exception handler.
    // If no exception handler for a given exception is present, the program stops executing with an error message.
    // Do not catch an exception unless you can handle it and leave the application in a known state.
    //     If you catch System.Exception, re-throw it using the throw keyword at the end of the catch block.
    // If a catch block defines an exception variable, you can use it to obtain more information about the type of exception that occurred.
    // Exceptions can be explicitly generated by a program by using the throw keyword.
    // Exception objects contain detailed information about the error, such as the state of the call stack and a text description of the error.
    // Code in a finally block is executed even if an exception is thrown. Use a finally block to release resources,
    //     for example to close any streams or files that were opened in the try block.
    // Managed exceptions in .NET are implemented on top of the Win32 structured exception handling mechanism.

    class SomeSpecificException : Exception
    {
    }

    class Program
    {
        static void Example1()
        {
            try
            {
                // Code to try goes here.
            }
            catch (SomeSpecificException ex)
            {
                // Code to handle the exception goes here.
                // Only catch exceptions that you know how to handle.
                // Never catch base class System.Exception without
                // rethrowing it at the end of the catch block.
            }
        }

        static void Example2()
        {
            try
            {
                // Code to try goes here.
            }
            finally
            {
                // Code to execute after the try block goes here.
            }
        }

        static void Example3()
        {
            try
            {
                // Code to try goes here.
            }
            catch (SomeSpecificException ex)
            {
                // Code to handle the exception goes here.
            }
            finally
            {
                // Code to execute after the try (and possibly catch) blocks goes here.
            }
        }
        
        static double SafeDivision(double x, double y)
        {
            if (y == 0)
                throw new System.DivideByZeroException();
            return x / y;
        }
        
        static void Example4()
        {
            // Input for test purposes. Change the values to see exception handling behavior.
            double a = 98, b = 0;
            double result = 0;

            try
            {
                result = SafeDivision(a, b);
                Console.WriteLine("{0} divided by {1} = {2}", a, b, result);
            }
            catch (DivideByZeroException e)
            {
                Console.WriteLine("Attempted divide by zero.");
            }
            // After the catch block is found and executed, control is passed to the next statement after that catch block.
        }
        
        class CustomException : Exception
        {
            public CustomException(string message) { }
        }
        
        private static void TestThrow()
        {
            throw new CustomException("Custom exception in TestThrow()");
        }
        
        static void TestCatch()
        {
            try
            {
                TestThrow();
            }
            // A catch block can specify the type of exception to catch. The type specification is called an exception filter.
            // The exception type should be derived from Exception. In general, do not specify Exception as the exception
            // filter unless either you know how to handle all exceptions that might be thrown in the try block, or you
            // have included a throw statement at the end of your catch block.
            catch (CustomException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        static void LogError(Exception e)
        {
            // All exceptions contain a property named Message.
            // This string should be set to explain the reason for the exception.
            // Note that information that is sensitive to security should not be put in the message text.
            Console.WriteLine(e.Message);
            
            // Exceptions contain a property named StackTrace. This string contains the name of the methods on the current
            // call stack, together with the file name and line number where the exception was thrown for each method.
            // A StackTrace object is created automatically by the common language runtime (CLR) from the point of the
            // throw statement, so that exceptions must be thrown from the point where the stack trace should begin.
            Console.WriteLine(e.StackTrace);
        }

        static void Example5()
        {
            try
            {
                // Try to access a resource.
            }
            catch (UnauthorizedAccessException e)
            {
                // Call a custom error logging procedure.
                LogError(e);
                // Re-throw the error.
                throw;
            }
        }

        static void Example6()
        {
        }
        
        // Programmers should throw exceptions when one or more of the following conditions are true:
        // The method cannot complete its defined functionality.
        static void CopyObject(Object original)
        {
            if (original == null)
            {
                //  ArgumentException contains a property named ParamName that should be set to the name of the argument
                // that caused the exception to be thrown. In the case of a property setter, ParamName should be set to value.
                throw new ArgumentException("Parameter cannot be null", nameof(original));
            }
        }
        
        static void Main(string[] args)
        {
            Example4();
            TestCatch();
        }
    }
    
    // Programmers should throw exceptions when one or more of the following conditions are true:
    // An inappropriate call to an object is made, based on the object state.
    class ProgramLog
    {
        FileStream logFile = null;
        void OpenLog(FileInfo fileName, FileMode mode) {}

        void WriteLog()
        {
            if (!this.logFile.CanWrite)
            {
                throw new System.InvalidOperationException("Logfile cannot be read-only");
            }
            // Else write data to the log and return.
        }
    }


    public class ExceptionExample
    {
        static void Run()
        {
            // A try statement can contain more than one catch block. The first catch statement that can handle
            // the exception is executed; any following catch statements, even if they are compatible, are ignored.
            // Therefore, catch blocks should always be ordered from most specific (or most-derived) to least specific.
            
            // Multiple catch blocks with different exception filters can be chained together. The catch blocks are evaluated
            // from top to bottom in your code, but only one catch block is executed for each exception that is thrown.
            // The first catch block that specifies the exact type or a base class of the thrown exception is executed.
            // If no catch block specifies a matching exception filter, a catch block that does not have a filter is selected,
            // if one is present in the statement. It is important to position catch blocks with the most specific
            // (that is, the most derived) exception types first.
            try
            {
                using (var sw = new StreamWriter(@"C:\test\test.txt"))
                {
                    sw.WriteLine("Hello");
                }
            }
            // Put the more specific exceptions first.
            catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine(ex);
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex);
            }
            // Put the least specific exception last.
            catch (IOException ex)
            {
                Console.WriteLine(ex);
            }

            Console.WriteLine("Done");
        }

        // Programmers should throw exceptions when one or more of the following conditions are true:
        // When an argument to a method causes an exception.
        int GetInt(int[] array, int index)
        {
            try
            {
                return array[index];
            }
            catch(IndexOutOfRangeException e)
            {
                // You can create and throw a new, more specific exception.
                throw new ArgumentOutOfRangeException("Parameter index is out of range.", e);
            }
        }
        
        static void TestFinally()
        {
            // Before the catch block is executed, the runtime checks for finally blocks. Finally blocks enable the programmer
            // to clean up any ambiguous state that could be left over from an aborted try block, or to release any external
            // resources (such as graphics handles, database connections or file streams) without waiting for the garbage
            // collector in the runtime to finalize the objects.
            
            // A finally block enables you to clean up actions that are performed in a try block. If present, the finally
            // block executes last, after the try block and any matched catch block. A finally block always runs,
            // regardless of whether an exception is thrown or a catch block matching the exception type is found.
            
            FileStream file = null;
            //Change the path to something that works on your machine.
            FileInfo fileInfo = new FileInfo(@"C:\file.txt");
            try
            {
                file = fileInfo.OpenWrite();
                file.WriteByte(0xF);
            }
            finally
            {
                // Closing the file allows you to reopen it immediately - otherwise IOException is thrown.
                if (file != null)
                {
                    file.Close();
                }
            }

            try
            {
                file = fileInfo.OpenWrite();
                Console.WriteLine("OpenWrite() succeeded");
            }
            catch (IOException)
            {
                Console.WriteLine("OpenWrite() failed");
            }
        }


        // Some .NET languages, including C++/CLI, allow objects to throw exceptions that do not derive from Exception.
        // Such exceptions are called non-CLS exceptions or non-Exceptions.
        // In C# you cannot throw non-CLS exceptions, but you can catch them in two ways:
        static void Example7()
        {
            // Class library written in C++/CLI.
            var myClass = new ThrowNonCLS.Class1();

            try
            {
                // throws gcnew System::String("I do not derive from System.Exception!");  
                myClass.TestThrow();
            }
            // 1º Within a catch (RuntimeWrappedException e) block.
            catch (RuntimeWrappedException e)
            {
                // By default, a Visual C# assembly catches non-CLS exceptions as wrapped exceptions. Use this method if you need
                // access to the original exception, which can be accessed through the RuntimeWrappedException.WrappedException property.
                String s = e.WrappedException as String;
                if (s != null)
                {
                    Console.WriteLine(s);
                }
            }
            // 2º Within a general catch block (a catch block without an exception type specified) that is put after all other catch blocks.
            catch
            {
                // Use this method when you want to perform some action (such as writing to a log file) in response to non-CLS
                // exceptions, and you do not need access to the exception information. By default the common language runtime
                // wraps all exceptions. To disable this behavior, add this assembly-level attribute to your code, typically
                // in the AssemblyInfo.cs file: [assembly: RuntimeCompatibilityAttribute(WrapNonExceptionThrows = false)].
            }

        }

   }
    
    // If no compatible catch block is found on the call stack after an exception is thrown, one of three things occurs:
    //     If the exception is within a finalizer, the finalizer is aborted and the base finalizer, if any, is called.
    //     If the call stack contains a static constructor, or a static field initializer, a TypeInitializationException
    //         is thrown, with the original exception assigned to the InnerException property of the new exception.
    //     If the start of the thread is reached, the thread is terminated.

    // Public and protected methods should throw exceptions whenever they cannot complete their intended functions.
    // The exception class that is thrown should be the most specific exception available that fits the error conditions.
    // These exceptions should be documented as part of the class functionality, and derived classes or updates to the
    // original class should retain the same behavior for backward compatibility.


    // THINGS TO AVOID WHEN THROWING EXCEPTIONS

    // Exceptions should not be used to change the flow of a program as part of ordinary execution.
    // Exceptions should only be used to report and handle error conditions.

    // Exceptions should not be returned as a return value or parameter instead of being thrown.

    // Do not throw System.Exception, System.SystemException, System.NullReferenceException, or
    // System.IndexOutOfRangeException intentionally from your own source code.

    // Do not create exceptions that can be thrown in debug mode but not release mode.
    // To identify run-time errors during the development phase, use Debug Assert instead.

    
    // DEFINING EXCEPTION CLASSES
    
    // Programs can throw a predefined exception class in the System namespace (except where previously noted), or
    // create their own exception classes by deriving from Exception. The derived classes should define at least four constructors:
    // one parameterless constructor,
    // one that sets the message property, and
    // one that sets both the Message and InnerException properties.
    // The fourth constructor is used to serialize the exception. New exception classes should be serializable.
    
    [Serializable()]
    public class InvalidDepartmentException : System.Exception
    {
        public InvalidDepartmentException() : base() { }
        public InvalidDepartmentException(string message) : base(message) { }
        public InvalidDepartmentException(string message, System.Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client.
        protected InvalidDepartmentException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) 
            : base(info, context) { }
        
        // New properties should only be added to the exception class when the data they provide is useful to resolving the exception.
        public string DepartmentName { get; set; }
        
        // If new properties are added to the derived exception class, ToString() should be overridden to return the added information.
        public override string ToString()
        {
            return $"{base.ToString()}\nDepartmentName: {DepartmentName}";
        }
    }
    
    
    // COMPILER-GENERATED EXCEPTIONS
    
    // Some exceptions are thrown automatically by the .NET runtime when basic operations fail.
    // These exceptions and their error conditions are listed in the following table.
    
    /*
    EXCEPTION	                DESCRIPTION
    
    ArithmeticException	        A base class for exceptions that occur during arithmetic operations, 
                                such as DivideByZeroException and OverflowException.
                                
    ArrayTypeMismatchException	Thrown when an array cannot store a given element because the actual type of the element 
                                is incompatible with the actual type of the array.
                                
    DivideByZeroException	    Thrown when an attempt is made to divide an integral value by zero.
    
    IndexOutOfRangeException	Thrown when an attempt is made to index an array when the index is less than zero or 
                                outside the bounds of the array.
                                
    InvalidCastException	    Thrown when an explicit conversion from a base type to an interface or to a derived type 
                                fails at runtime.
                                
    NullReferenceException	    Thrown when an attempt is made to reference an object whose value is null.
    
    OutOfMemoryException	    Thrown when an attempt to allocate memory using the new operator fails. 
                                This indicates that the memory available to the common language runtime has been exhausted.
                                
    OverflowException	        Thrown when an arithmetic operation in a checked context overflows.
    
    StackOverflowException	    Thrown when the execution stack is exhausted by having too many pending method calls; 
                                usually indicates a very deep or infinite recursion.
                                
    TypeInitializationException	Thrown when a static constructor throws an exception and no compatible catch clause exists to catch it.
    */
}

namespace ThrowNonCLS
{

    public class Class1
    {
        public void TestThrow()
        {
            // throw new RuntimeWrappedException(obj)
        }
    }
}
