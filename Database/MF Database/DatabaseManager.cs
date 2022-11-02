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
    class DatabaseManager
    {
        private FileManager fileManager;
        public string name;
        public List<string> spreadsheetNames;
        private DatabaseUI databaseUI;
        private DatabaseChecker databaseChecker;
        private DatabaseXmlManager xmlManager;
        public DatabaseManager(string databaseName, FileManager mainFileManager)
        {           
            spreadsheetNames = new();
            name = databaseName;
            fileManager = mainFileManager;
            databaseUI = new DatabaseUI(name);
            databaseChecker = new DatabaseChecker(this);
            xmlManager = new DatabaseXmlManager(mainFileManager, name);
            foreach (string path in xmlManager.GetAllSpreadsheetPathsFromDatabase())
            {
                spreadsheetNames.Add(Regex.Replace(path.Split(@"\")[^1], @"\.xml$", ""));
            }
            
        }
        public void GetSpreadsheetNames()
        {
            databaseUI.SpreadsheetNamesMessage(spreadsheetNames);
        }
        public void AddSpreadsheet(string spreadsheetName)
        {
            if (!databaseChecker.CheckIfSpreadsheetExist(spreadsheetName))
            {
                xmlManager.AddSpreadsheet(spreadsheetName);
                spreadsheetNames.Add(spreadsheetName);
                databaseUI.SpreadsheetAddingMessage(true);
            }
            else
                databaseUI.SpreadsheetAddingMessage(false);
        }
        public void DeleteSpreadsheet(string name)
        {
            if (databaseChecker.CheckIfSpreadsheetExist(name))
            {
                xmlManager.DeleteSpreadsheet(name);
                spreadsheetNames.Remove(name);
                databaseUI.SpreadsheetDeletingMessage(true);
            }
            else
                databaseUI.SpreadsheetDeletingMessage(false);
        }
        public void EditSpreadsheet(string name)
        {
            if (databaseChecker.CheckIfSpreadsheetExist(name))
            {
                SpreadsheetInteractionMenu spreadsheetInteractionMenu = new(this, name, fileManager);
                databaseUI.SpreadsheetEdititngMessage(true);
                spreadsheetInteractionMenu.Start();               
                databaseUI.SpreadsheetEditingEnded();
            }
            else
                databaseUI.SpreadsheetEdititngMessage(false);
        }
    }
}
