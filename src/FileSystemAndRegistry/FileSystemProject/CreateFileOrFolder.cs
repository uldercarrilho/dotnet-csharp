using System;
using System.IO;

public class CreateFileOrFolder
{
    public static void Run()
    {
        Console.WriteLine("\nRunning CreateFileOrFolder");

        // Specify a name for your top-level folder.
        string folderName = @"c:\Temp";

        // To create a string that specifies the path to a subfolder under your
        // top-level folder, add a name for the subfolder to folderName.
        string pathString = Path.Combine(folderName, "SubFolder");

        // You can write out the path name directly instead of using the Combine
        // method. Combine just makes the process easier.
        string pathString2 = @"c:\Temp\SubFolder2";

        // You can extend the depth of your path if you want to.
        //pathString = System.IO.Path.Combine(pathString, "SubSubFolder");

        // Create the subfolder. You can verify in File Explorer that you have this
        // structure in the C: drive.
        //    Local Disk (C:)
        //        Top-Level Folder
        //            SubFolder
        // If the folder already exists, CreateDirectory does nothing, and no exception is thrown.
        Directory.CreateDirectory(pathString);

        // Create a file name for the file you want to create.
        string fileName = Path.GetRandomFileName();

        // This example uses a random string for the name, but you also can specify
        // a particular name.
        //string fileName = "MyNewFile.txt";

        // Use Combine again to add the file name to the path.
        pathString = Path.Combine(pathString, fileName);

        // Verify the path that you have constructed.
        Console.WriteLine("Path to my file: {0}\n", pathString);

        // Check that the file doesn't already exist. If it doesn't exist, create
        // the file and write integers 0 - 99 to it.
        // DANGER: System.IO.File.Create will overwrite the file if it already exists.
        // This could happen even with random file names, although it is unlikely.
        if (File.Exists(pathString) == false)
        {
            // File.Create replaces an existing file with a new file
            using (FileStream fs = File.Create(pathString))
            // another way to create a file
            // using (System.IO.FileStream fs = new System.IO.FileStream(pathString, FileMode.Append))
            {
                for (byte i = 0; i < 100; i++)
                {
                    fs.WriteByte(i);
                }
            }
        } else {
            Console.WriteLine("File \"{0}\" already exists.", fileName);
            return;
        }

        // Read and display the data from your file.
        try
        {
            byte[] readBuffer = File.ReadAllBytes(pathString);
            foreach (byte b in readBuffer)
            {
                Console.Write(b + " ");
            }
            Console.WriteLine();
        }
        catch (IOException e)
        {
            Console.WriteLine(e.Message);
        }
    }
}