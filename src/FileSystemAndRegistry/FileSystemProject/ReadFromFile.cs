using System;
using System.IO;

class ReadFromFile
{
    public static void Run()
    {
        Console.WriteLine("\nRunning ReadFromFile");

        // The files used in this example are created in the topic
        // How to: Write to a Text File. You can change the path and
        // file name to substitute text files of your own.

        // Example #1
        // Read the file as one string.
        string text = File.ReadAllText(@"C:\Users\Public\TestFolder\WriteText.txt");

        // Display the file contents to the console. Variable text is a string.
        Console.WriteLine("Contents of WriteText.txt = {0}", text);

        // Example #2
        // Read each line of the file into a string array. Each element
        // of the array is one line of the file.
        string[] lines = File.ReadAllLines(@"C:\Users\Public\TestFolder\WriteLines2.txt");

        // Display the file contents by using a foreach loop.
        Console.WriteLine("Contents of WriteLines2.txt = ");
        foreach (string line in lines)
        {
            // Use a tab to indent each line of the file.
            Console.WriteLine("\t" + line);
        }

        // How to read a text file one line at a time
        ReadOneLineAtTime();
    }

    private static void ReadOneLineAtTime()
    {
        Console.WriteLine("\nRunning ReadFromFile.ReadOneLineAtTime");

        int counter = 0;  
        string line;  
        
        // Read the file and display it line by line.  
        StreamReader file = new StreamReader(@"C:\Users\Public\TestFolder\WriteLines2.txt");
        while((line = file.ReadLine()) != null)  
        {  
            Console.WriteLine(line);  
            counter++;  
        }  
        file.Close();  

        Console.WriteLine("There were {0} lines.", counter);  
    }
}