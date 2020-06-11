using System;
using Microsoft.Win32;


public class RegistryTest
{
    public static void Run()
    {
        Console.WriteLine("\nRunning RegistryTest");

        // dotnet add package Microsoft.Win32.Registry --version 4.7.0
        RegistryKey key;
        key = Registry.CurrentUser.CreateSubKey("Names");
        key.SetValue("Name", "Isabella");
        key.Close();
    }
}
