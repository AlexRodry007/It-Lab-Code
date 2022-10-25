using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLab
{
    class DatabaseInteractionMenu : MetaInteractionMenu
    {
        private static Dictionary<Func<string[], bool>, string> commandsWithDescriptions = new()
        {
            { ShowAllSpreadsheetNames, "Shows all names of the spreadsheets in the current database" },
            { EditSpreadsheet, "Allows editing of a spreadsheet, requires one argument \"name\". Names are case sensitive" },
            { AddSpreadsheet, "Creates new Spreadsheet, requires one argument \"name\"" },
            { DeleteSpreadsheet, "Deletes spreadsheet, requires one argument \"name\"" }
        };
        static Database database;
        public DatabaseInteractionMenu(string databaseName)
        {
            database = new Database(databaseName);
            refreshCommands(commandsWithDescriptions);
        }
        
        private static bool ShowAllSpreadsheetNames(string[] arguments)
        {
            List<string> databasePaths = database.spreadsheetNames;
            foreach (string path in databasePaths)
            {
                Console.WriteLine(path.Split(@"\")[^1]);
            }
            return true;
        }
        private static bool EditSpreadsheet(string[] arguments)
        {
            if (database.CheckIfSpreadsheetExist(arguments[0]))
            {
                SpreadsheetInteractionMenu spreadsheetInteractionMenu = new(database, arguments[0]);
                spreadsheetInteractionMenu.Start("Spreadsheet menu started");
                refreshCommands(commandsWithDescriptions);
                Console.WriteLine("Database menu started");
            }
            else
                Console.WriteLine("Invalid name, enter 'ShowAllSpreadsheetNames' to veiw all spreadsheet's names");
            return true;
        }
        private static bool AddSpreadsheet(string[] arguments)
        {
            if (database.AddSpreadsheet(arguments[0]))
                Console.WriteLine("Added spreadsheet succesfully");
            else
                Console.WriteLine("This Name already exists");
            return true;
        }
        private static bool DeleteSpreadsheet(string[] arguments)
        {
            if (database.CheckIfSpreadsheetExist(arguments[0]))
            {
                FileManager.DeleteSpreadsheet(database.name, arguments[0]);
                database.spreadsheetNames.Remove(arguments[0]);
            }
            return true;
        }
    }
}
