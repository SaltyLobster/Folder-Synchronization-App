using System;
using System.IO;

class SynchronizationApp{ 
static void Main(string[] args)
{
    //Set up the source and replica folder paths via console
    Console.WriteLine("Enter the path for a source folder");
    DirectoryInfo sourceDirectory = new(Console.ReadLine());
    Console.WriteLine("Enter the path for a source folder");
    DirectoryInfo replicaDirectory = new(Console.ReadLine());

        //Get the file lists from the directories
        FileInfo[] Sourcefiles = sourceDirectory.GetFiles();
        Console.WriteLine(Sourcefiles);
    }
}