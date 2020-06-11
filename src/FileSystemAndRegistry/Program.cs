using System;

namespace FileSystemAndRegistry
{
    class Program
    {
        static void Main(string[] args)
        {
            // How to iterate through a directory tree
            RecursiveFileSearch.Run();
            StackBasedIteration.Run("C:/SAC");

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
        }
    }
}
