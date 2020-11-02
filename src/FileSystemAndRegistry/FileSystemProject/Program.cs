using System;

namespace FileSystemProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            
            // How to iterate through a directory tree
            RecursiveFileSearch.Run();
            StackBasedIteration.Run(@"C:\SAJ");
            
            FileSysInfo.Run();
            CreateFileOrFolder.Run();
            SimpleFileCopy.Run();
            SimpleFileMove.Run();
            SimpleFileDelete.Run();
            WriteTextFile.Run();
            ReadFromFile.Run();
            // How to create a key in the registry
            RegistryTest.Run();
        }
    }
}