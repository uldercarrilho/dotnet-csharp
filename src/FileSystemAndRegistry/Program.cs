using System;

namespace FileSystemAndRegistry
{
    class Program
    {
        static void Main(string[] args)
        {
            // How to iterate through a directory tree
            RecursiveFileSearch.Run();
            StackBasedIteration.Run(@"C:\SAJ");

            // How to get information about files, folders, and drives
            FileSysInfo.Run();
            // In .NET, you can access file system information by using the following classes:
            // System.IO.FileInfo
            // System.IO.DirectoryInfo
            // System.IO.DriveInfo
            // System.IO.Directory
            // System.IO.File

            // The FileInfo and DirectoryInfo classes represent a file or directory and contain properties that expose 
            // many of the file attributes that are supported by the NTFS file system. They also contain methods for 
            // opening, closing, moving, and deleting files and folders.

            // You can also obtain the names of files, folders, or drives by using calls to DirectoryInfo.GetDirectories, 
            // DirectoryInfo.GetFiles, and DriveInfo.RootDirectory.

            // The System.IO.Directory and System.IO.File classes provide static methods for retrieving information about 
            // directories and files.

            // How to create a file or folder
            CreateFileOrFolder.Run();

            // How to copy, delete, and move files and folders
            // Use System.IO.FileSystemWatcher to provide events that will enable you to calculate the progress when 
            // operating on multiple files. Another approach is to use platform invoke to call the relevant file-related 
            // methods in the Windows Shell. For information about how to perform these file operations asynchronously, 
            // see Asynchronous File I/O.
            SimpleFileCopy.Run();
            SimpleFileMove.Run();
            SimpleFileDelete.Run();

            // How to write to a text file
            WriteTextFile.Run();
            // How to read from a text file
            ReadFromFile.Run();

            // How to create a key in the registry
            RegistryTest.Run();
        }
    }
}
