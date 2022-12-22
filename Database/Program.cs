using System;
using System.Collections.Generic;
using System.IO;
using System.Collections;

namespace DBLab
{
    public class Program
    {  
        static void Main(string[] args)
        {
            string databaseDirectoryRelaivePath = @"..\..\..\Databases\";
            FileManager mainFileManager = new FileManager(databaseDirectoryRelaivePath);
            MainInteractionMenu mainInteractionMenu = new MainInteractionMenu(mainFileManager);
            Console.WriteLine("Main menu started");
            mainInteractionMenu.Start();   
        }
    }
    
}
