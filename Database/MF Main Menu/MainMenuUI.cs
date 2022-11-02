using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLab
{
    class MainMenuUI
    {
        public MainMenuUI()
        { }
        public void ShowAllNames(string[] names)
        {
            foreach(string name in names)
            {
                Console.WriteLine(name);
            }
        }
        public void DatabaseEditingMessage(bool success)
        {
            if(success)
                Console.WriteLine("Database menu started");
            else
                Console.WriteLine("Invalid name, enter 'ShowAllDatabaseNames' to veiw all database names");
        }
        public void DatabaseEditingEndedMessage()
        {
            Console.WriteLine("Exited to Main Menu");
        }
        public void DatabaseDeletingMessage(bool success)
        {
            if(success)
                Console.WriteLine("Database deleted succesfully");
            else
                Console.WriteLine("Invalid name, enter 'ShowAllDatabaseNames' to veiw all database names");
        }
        public void DatabaseAddingMessage(bool success)
        {
            if (success)
                Console.WriteLine("Database added succesfully");        
            else
                Console.WriteLine("Database with the current name already exists");
        }
    }
}
