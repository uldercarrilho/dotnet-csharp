using System;
using System.IO;

public class SimpleFileDelete
{
    private static void Initialize()
    {
        // To run this sample, create the following files on your drive:
        // C:\Users\Public\DeleteTest\test1.txt
        // C:\Users\Public\DeleteTest\test2.txt
        // C:\Users\Public\DeleteTest\SubDir\test2.txt
        Directory.CreateDirectory(@"C:\Users\Public\DeleteTest\SubDir");
        File.Create(@"C:\Users\Public\DeleteTest\test1.txt").Close();
        File.Create(@"C:\Users\Public\DeleteTest\test2.txt").Close();
        File.Create(@"C:\Users\Public\DeleteTest\SubDir\test2.txt").Close();
    }

    public static void Run()
    {
        Console.WriteLine("\nRunning SimpleFileDelete");

        Initialize();

        // Delete a file by using File class static method...
        if(File.Exists(@"C:\Users\Public\DeleteTest\test.txt"))
        {
            // Use a try block to catch IOExceptions, to
            // handle the case of the file already being
            // opened by another process.
            try
            {
                File.Delete(@"C:\Users\Public\DeleteTest\test.txt");
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
                return;
            }
        }

        // ...or by using FileInfo instance method.
        FileInfo fi = new FileInfo(@"C:\Users\Public\DeleteTest\test2.txt");
        try
        {
            fi.Delete();
        }
        catch (IOException e)
        {
            Console.WriteLine(e.Message);
        }

        // Delete a directory. Must be writable or empty.
        try
        {
            Directory.Delete(@"C:\Users\Public\DeleteTest");
        }
        catch (IOException e)
        {
            Console.WriteLine(e.Message);
        }
        // Delete a directory and all subdirectories with Directory static method...
        if(Directory.Exists(@"C:\Users\Public\DeleteTest"))
        {
            try
            {
                Directory.Delete(@"C:\Users\Public\DeleteTest", true);
            }

            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        // ...or with DirectoryInfo instance method.
        DirectoryInfo di = new DirectoryInfo(@"C:\Users\Public\public");
        // Delete this dir and all subdirs.
        try
        {
            di.Delete(true);
        }
        catch (IOException e)
        {
            Console.WriteLine(e.Message);
        }
    }
}