using System;
using System.Collections.Specialized;

// In the simplest case, in which you know for certain that you have access permissions for all directories under a 
// specified root, you can use the System.IO.SearchOption.AllDirectories flag. This flag returns all the nested 
// subdirectories that match the specified pattern. The following example shows how to use this flag.

// root.GetDirectories("*.*", System.IO.SearchOption.AllDirectories);  

// The weakness in this approach is that if any one of the subdirectories under the specified root causes a 
// DirectoryNotFoundException or UnauthorizedAccessException, the whole method fails and returns no directories. 
// The same is true when you use the GetFiles method. If you have to handle these exceptions on specific subfolders, 
// you must manually walk the directory tree, as shown in the following examples.

// The recursive approach is elegant but has the potential to cause a 
// stack overflow exception if the directory tree is large and deeply nested.
public class RecursiveFileSearch
{
    static StringCollection log = new StringCollection();

    public static void Run()
    {
        Console.WriteLine("\nRunning RecursiveFileSearch");
        // Start with drives if you have to search the entire computer.
        string[] drives = System.Environment.GetLogicalDrives();

        foreach (string dr in drives)
        {
            System.IO.DriveInfo di = new System.IO.DriveInfo(dr);

            // Here we skip the drive if it is not ready to be read. This
            // is not necessarily the appropriate action in all scenarios.
            if (!di.IsReady)
            {
                System.Console.WriteLine("The drive {0} could not be read", di.Name);
                continue;
            }
            System.IO.DirectoryInfo rootDir = di.RootDirectory;
            WalkDirectoryTree(rootDir);
        }

        // Write out all the files that could not be processed.
        Console.WriteLine("Files with restricted access:");
        foreach (string s in log)
        {
            Console.WriteLine(s);
        }
    }

    static void WalkDirectoryTree(System.IO.DirectoryInfo root)
    {
        // If you must store the contents of a directory tree, either in memory or on disk, the best option is to store 
        // only the FullName property (of type string) for each file. You can then use this string to create a new FileInfo 
        // or DirectoryInfo object as necessary, or open any file that requires additional processing.
        System.IO.FileInfo[] files = null;
        System.IO.DirectoryInfo[] subDirs = null;

        // First, process all the files directly under this folder
        try
        {
            files = root.GetFiles("*.*");
        }
        // This is thrown if even one of the files requires permissions greater
        // than the application provides.
        catch (UnauthorizedAccessException e)
        {
            // This code just writes out the message and continues to recurse.
            // You may decide to do something different here. For example, you
            // can try to elevate your privileges and access the file again.
            log.Add(e.Message);

            // It is generally too time-consuming to test every folder to determine whether your application has permission 
            // to open it. Therefore, the code example just encloses that part of the operation in a try/catch block. You 
            // can modify the catch block so that when you are denied access to a folder, you try to elevate your permissions 
            // and then access it again. As a rule, only catch those exceptions that you can handle without leaving your 
            // application in an unknown state.
        }
        catch (System.IO.DirectoryNotFoundException e)
        {
            Console.WriteLine(e.Message);
        }

        if (files != null)
        {
            foreach (System.IO.FileInfo fi in files)
            { 
                // In this example, we only access the existing FileInfo object. If we
                // want to open, delete or modify the file, then
                // a try-catch block is required here to handle the case
                // where the file has been deleted since the call to TraverseTree().
                Console.WriteLine(fi.FullName);
            }

            // Now find all the subdirectories under this directory.
            subDirs = root.GetDirectories();

            foreach (System.IO.DirectoryInfo dirInfo in subDirs)
            {
                // Resursive call for each subdirectory.
                // WalkDirectoryTree(dirInfo);
            }
        }
    }
}
