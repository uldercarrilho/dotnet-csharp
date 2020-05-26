using System;
using System.IO;

namespace Statements.ExceptionHandling
{
    public class ExceptionHandlingStatements_try_catch_finally
    {
        // A common usage of catch and finally together is to obtain and use resources in a try block,
        // deal with exceptional circumstances in a catch block, and release the resources in the finally block.

        public static void ReadFile(int index)
        {
            // To run this code, substitute a valid path from your local machine
            string path = @"c:\users\public\test.txt";
            StreamReader file = new StreamReader(path);
            char[] buffer = new char[10];
            try
            {
                file.ReadBlock(buffer, index, buffer.Length);
            }
            catch (IOException e)
            {
                Console.WriteLine("Error reading from {0}. Message = {1}", path, e.Message);
            }
            finally
            {
                if (file != null)
                {
                    file.Close();
                }
            }
            // Do something with buffer...
        }
    }
}