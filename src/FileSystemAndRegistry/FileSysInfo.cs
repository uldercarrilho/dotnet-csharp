using System;
using System.IO;

class FileSysInfo
{
    public static void Run()
    {
        Console.WriteLine("\nRunning FileSysInfo");

        // You can also use System.Environment.GetLogicalDrives to
        // obtain names of all logical drives on the computer.
        DriveInfo di = new DriveInfo(@"C:\");
        Console.WriteLine($"TotalFreeSpace={di.TotalFreeSpace}");
        Console.WriteLine($"VolumeLabel={di.VolumeLabel}");

        // Get the root directory and print out some information about it.
        DirectoryInfo dirInfo = di.RootDirectory;
        Console.WriteLine(dirInfo.Attributes.ToString());

        // Get the files in the directory and print out some information about them.
        FileInfo[] fileNames = dirInfo.GetFiles("*.*");

        foreach (FileInfo fi in fileNames)
        {
            Console.WriteLine("{0}: {1}: {2}", fi.Name, fi.LastAccessTime, fi.Length);
        }

        // Get the subdirectories directly that is under the root.
        // See "How to: Iterate Through a Directory Tree" for an example of how to
        // iterate through an entire tree.
        DirectoryInfo[] dirInfos = dirInfo.GetDirectories("*.*");

        foreach (DirectoryInfo d in dirInfos)
        {
            Console.WriteLine(d.Name);
        }

        // The Directory and File classes provide several static methods
        // for accessing files and directories.

        // Get the current application directory.
        string currentDirName = Directory.GetCurrentDirectory();
        Console.WriteLine(currentDirName);

        // Get an array of file names as strings rather than FileInfo objects.
        // Use this method when storage space is an issue, and when you might
        // hold on to the file name reference for a while before you try to access
        // the file.
        string[] files = Directory.GetFiles(currentDirName, "*.txt");

        foreach (string s in files)
        {
            // Create the FileInfo object only when needed to ensure
            // the information is as current as possible.
            FileInfo fi = null;
            try
            {
                fi = new FileInfo(s);
            }
            catch (FileNotFoundException e)
            {
                // To inform the user and continue is
                // sufficient for this demonstration.
                // Your application may require different behavior.
                Console.WriteLine(e.Message);
                continue;
            }
            Console.WriteLine("{0} : {1}", fi.Name, fi.Directory);
        }

        // Change the directory. In this case, first check to see
        // whether it already exists, and create it if it does not.
        // If this is not appropriate for your application, you can
        // handle the IOException that will be raised if the
        // directory cannot be found.
        if (!Directory.Exists(@"C:\Users\Public\TestFolder\"))
        {
            Directory.CreateDirectory(@"C:\Users\Public\TestFolder\");
        }

        Directory.SetCurrentDirectory(@"C:\Users\Public\TestFolder\");

        currentDirName = Directory.GetCurrentDirectory();
        Console.WriteLine(currentDirName);
    }

    // When you process user-specified path strings, you should also handle exceptions for the following conditions:
    // The file name is malformed. For example, it contains invalid characters or only white space.
    // The file name is null.
    // The file name is longer than the system-defined maximum length.
    // The file name contains a colon (:).

    // If the application does not have sufficient permissions to read the specified file, the Exists method returns 
    // false regardless of whether a path exists; the method does not throw an exception.
}
