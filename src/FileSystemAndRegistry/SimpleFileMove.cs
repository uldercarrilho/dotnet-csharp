using System.IO;

public class SimpleFileMove
{
    private static void Initialize()
    {
        Directory.CreateDirectory(@"C:\Users\Public\public\test");
        File.Create(@"C:\Users\Public\public\test.txt").Close();
        if (Directory.Exists(@"C:\Users\Public\private")) {
            Directory.Delete(@"C:\Users\Public\private", true);
        }
        Directory.CreateDirectory(@"C:\Users\Public\private");
    }

    public static void Run()
    {
        Initialize();

        string sourceFile = @"C:\Users\Public\public\test.txt";
        string destinationFile = @"C:\Users\Public\private\test.txt";

        // To move a file or folder to a new location:
        File.Move(sourceFile, destinationFile);

        // To move an entire directory. To programmatically modify or combine
        // path strings, use the System.IO.Path class.
        Directory.Move(@"C:\Users\Public\public\test", @"C:\Users\Public\private\test");
    }
}