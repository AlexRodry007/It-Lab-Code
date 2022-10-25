using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace DBLab
{
    class MainInteractionMenu : MetaInteractionMenu
    {
        private static Dictionary<Func<string[], bool>, string> commandsWithDescriptions = new Dictionary<Func<string[], bool>, string>
        {
            {ShowAllDatabaseNames, "Shows all names of the databases"},
            {EditDatabase, "Allows editing of a database, needs one argument 'name'. Names are case sensitive" },
            {AddDatabase, "Creates new Database, requires one argument \"name\""},
            {DeleteDatabase, "Deletes Database, requires one argument \"name\"" }
        };
        public MainInteractionMenu()
        {
            ClearCommands();
            foreach (KeyValuePair<Func<string[], bool>, string> commandWithDescription in commandsWithDescriptions)
            {
                allCommandsWithDescriptions.Add(commandWithDescription.Key, commandWithDescription.Value);
            }
        }
        private static bool ShowAllDatabaseNames(string[] arguments)
        {
            string[] databasePaths = FileManager.GetAllDatabasePaths();
            foreach (string path in databasePaths)
            {
                Console.WriteLine(path.Split(@"\")[^1]);
            }
            return true;              
        }
        private static bool EditDatabase(string[] arguments)
        {
            if (FileManager.CheckIfDatabaseExist(arguments[0]))
            {
                DatabaseInteractionMenu databaseInteractionMenu = new DatabaseInteractionMenu(arguments[0]);
                databaseInteractionMenu.Start("Database menu started");
                refreshCommands(commandsWithDescriptions);
                Console.WriteLine("Main menu started");
            }
            else
                Console.WriteLine("Invalid name, enter 'ShowAllDatabaseNames' to veiw all database names");
            return true;
        }
        private static bool DeleteDatabase(string[] arguments)
        {
            if (FileManager.CheckIfDatabaseExist(arguments[0]))
            {
                FileManager.DeleteDatabase(arguments[0]);
                Console.WriteLine("Database deleted succesfully");
            }
            else
                Console.WriteLine("Invalid name, enter 'ShowAllDatabaseNames' to veiw all database names");
            return true;
        }
        private static bool AddDatabase(string[] arguments)
        {
            if (!FileManager.CheckIfDatabaseExist(arguments[0]))
            {
                FileManager.AddDatabase(arguments[0]);
                Console.WriteLine("Database added succesfully");
            }
            else
                Console.WriteLine("Database with the current name already exists");
            return true;
        }
    }
}
