using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;

namespace DBLab
{
    class Database
    {
        public string name;
        public List<string> spreadsheetNames;
        public Database(string databaseName)
        {
            spreadsheetNames = new();
            name = databaseName;
            foreach(string path in FileManager.GetAllSpreadsheetPathsFromDatabase(name))
            {
                spreadsheetNames.Add(Regex.Replace(path.Split(@"\")[^1], @"\.xml$", ""));
            }

        }
        public string GetSpreadsheetNames()
        {
            return DatabaseMessanger.SpreadsheetNamesMessage(spreadsheetNames);
        }
        public string AddSpreadsheet(string spreadsheetName)
        {
            if (!DatabaseChecker.CheckIfSpreadsheetExist(spreadsheetName, this))
            {
                FileManager.AddSpreadsheet(this, spreadsheetName);
                spreadsheetNames.Add(spreadsheetName);
                return DatabaseMessanger.SpreadsheetAddingMessage(true);
            }
            else
                return DatabaseMessanger.SpreadsheetAddingMessage(false);
        }
        public string DeleteSpreadsheet(string name)
        {
            if (DatabaseChecker.CheckIfSpreadsheetExist(name, this))
            {
                FileManager.DeleteSpreadsheet(this.name, name);
                spreadsheetNames.Remove(name);
                return DatabaseMessanger.SpreadsheetDeletingMessage(true);
            }
            else
                return DatabaseMessanger.SpreadsheetDeletingMessage(false);
        }
        public string EditSpreadsheet(string name)
        {
            if (DatabaseChecker.CheckIfSpreadsheetExist(name, this))
            {
                SpreadsheetInteractionMenu spreadsheetInteractionMenu = new(this, name);
                spreadsheetInteractionMenu.Start(DatabaseMessanger.SpreadsheetEdititngMessage(true));               
                return DatabaseMessanger.SpreadsheetEditingEnded;
            }
            else
                return DatabaseMessanger.SpreadsheetEdititngMessage(false);
        }
    }
}
