using System;

namespace FileSystemAndRegistry
{
    class Program
    {
        static void Main(string[] args)
        {
            RecursiveFileSearch.Run();
            StackBasedIteration.Run("C:/SAC");

            // It is generally too time-consuming to test every folder to determine whether your application has permission 
            // to open it. Therefore, the code example just encloses that part of the operation in a try/catch block. You 
            // can modify the catch block so that when you are denied access to a folder, you try to elevate your permissions 
            // and then access it again. As a rule, only catch those exceptions that you can handle without leaving your 
            // application in an unknown state.

            // If you must store the contents of a directory tree, either in memory or on disk, the best option is to store 
            // only the FullName property (of type string) for each file. You can then use this string to create a new FileInfo 
            // or DirectoryInfo object as necessary, or open any file that requires additional processing.
        }
    }
}
