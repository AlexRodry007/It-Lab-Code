using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLab
{
    public class DatabaseInteractionMenu : MetaInteractionMenu
    {
        private static Dictionary<Func<string[], bool>, string> commandsWithDescriptions = new()
        {
            { ShowAllSpreadsheetNames, "Shows all names of the spreadsheets in the current database" },
            { EditSpreadsheet, "Allows editing of a spreadsheet, requires one argument \"name\". Names are case sensitive" },
            { AddSpreadsheet, "Creates new Spreadsheet, requires one argument \"name\"" },
            { DeleteSpreadsheet, "Deletes spreadsheet, requires one argument \"name\"" },
            { MergeSpreadsheets, "Merges two spreadsheet using common field, requires four arguments \"Name of the first spreadsheet\", \"Name of the field in the first spreadsheet\", \"Name of the second spreadsheet\", \"Name of the field in the second spreadsheet\", \"Name of the new spreadsheet\"" }
        };
        static DatabaseManager database;
        public DatabaseInteractionMenu(string databaseName, FileManager mainFileManager)
        {
            database = new DatabaseManager(databaseName, mainFileManager);
            refreshCommands(commandsWithDescriptions);
        }
        
        private static bool ShowAllSpreadsheetNames(string[] arguments)
        {
            database.GetSpreadsheetNames();
            return true;
        }
        private static bool EditSpreadsheet(string[] arguments)
        {
            database.EditSpreadsheet(arguments[0]);
            refreshCommands(commandsWithDescriptions);
            return true;
        }
        private static bool AddSpreadsheet(string[] arguments)
        {
            database.AddSpreadsheet(arguments[0]);
            return true;
        }
        private static bool DeleteSpreadsheet(string[] arguments)
        {
            database.DeleteSpreadsheet(arguments[0]);
            return true;
        }
        private static bool MergeSpreadsheets(string[] arguments)
        {
            database.MergeSpreadsheets(arguments[0], arguments[1], arguments[2], arguments[3], arguments[4]);
            return true;
        }
    }
}
