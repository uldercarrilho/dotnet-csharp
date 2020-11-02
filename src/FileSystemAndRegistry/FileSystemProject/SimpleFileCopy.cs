using System;
using System.IO;

public class SimpleFileCopy
{
    private static void Initialize()
    {
        // To run this sample, first create the following directories and files:
        // C:\Users\Public\TestFolder
        // C:\Users\Public\TestFolder\test.txt
        // C:\Users\Public\TestFolder\SubDir\test.txt
        Directory.CreateDirectory(@"C:\Users\Public\TestFolder\SubDir");
        File.Create(@"C:\Users\Public\TestFolder\test.txt").Close();
        File.Create(@"C:\Users\Public\TestFolder\SubDir\test.txt").Close();
    }

    public static void Run()
    {
        Console.WriteLine("\nRunning SimpleFileCopy");

        Initialize();

        string fileName = "test.txt";
        string sourcePath = @"C:\Users\Public\TestFolder";
        string targetPath =  @"C:\Users\Public\TestFolder\SubDir";

        // Use Path class to manipulate file and directory paths.
        string sourceFile = Path.Combine(sourcePath, fileName);
        string destFile = Path.Combine(targetPath, fileName);

        // To copy a folder's contents to a new location:
        // Create a new target folder.
        // If the directory already exists, this method does not create a new directory.
        Directory.CreateDirectory(targetPath);

        // To copy a file to another location and
        // overwrite the destination file if it already exists.
        File.Copy(sourceFile, destFile, true);

        // To copy all the files in one directory to another directory.
        // Get the files in the source folder. (To recursively iterate through
        // all subfolders under the current directory, see
        // "How to: Iterate Through a Directory Tree.")
        // Note: Check for target path was performed previously in this code example.
        if (Directory.Exists(sourcePath))
        {
            string[] files = Directory.GetFiles(sourcePath);

            // Copy the files and overwrite destination files if they already exist.
            foreach (string s in files)
            {
                // Use static Path methods to extract only the file name from the path.
                fileName = Path.GetFileName(s);
                destFile = Path.Combine(targetPath, fileName);
                File.Copy(s, destFile, true);
            }
        }
        else
        {
            Console.WriteLine("Source path does not exist!");
        }
    }
}