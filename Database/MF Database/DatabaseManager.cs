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
    public class DatabaseManager
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
        public void MergeSpreadsheets(string firstSpreadsheetName, string firstFieldName, string secondSpreadsheetName, string secondFieldName, string newSpreadsheetName)
        {
            if(databaseChecker.CheckIfSpreadsheetExist(firstSpreadsheetName) && databaseChecker.CheckIfSpreadsheetExist(secondSpreadsheetName))
            {
                SpreadsheetManager firstSpreadsheet = new SpreadsheetManager(this, firstSpreadsheetName, fileManager);
                SpreadsheetManager secondSpreadsheet = new SpreadsheetManager(this, secondSpreadsheetName, fileManager);
                if(databaseChecker.CheckIfTwoSpreadsheetsHaveTheSameField(firstSpreadsheet,firstFieldName,secondSpreadsheet,secondFieldName))
                {
                    AddSpreadsheet(newSpreadsheetName);
                    SpreadsheetManager newSpreadsheet = new SpreadsheetManager(this, newSpreadsheetName, fileManager);
                    Field firstCommonField = firstSpreadsheet.GetFieldByName(firstFieldName);
                    Field secondCommonField = secondSpreadsheet.GetFieldByName(secondFieldName);
                    int firstCommonFieldId = firstSpreadsheet.fields.IndexOf(firstCommonField);
                    int secondCommonFieldId = secondSpreadsheet.fields.IndexOf(secondCommonField);
                    List<Field> allFields = databaseChecker.MergeSpreadsheetFieldsByCommonField(firstSpreadsheet, secondSpreadsheet, firstCommonFieldId, secondCommonFieldId);
                    foreach (Field field in allFields)
                    {
                        newSpreadsheet.AddField(field);
                    }
                    List<List<string>> mergedData = databaseChecker.MergeData(firstSpreadsheet, secondSpreadsheet, firstCommonFieldId, secondCommonFieldId);
                    foreach(List<string> row in mergedData)
                    {
                        newSpreadsheet.AddRow(row);
                    }

                }
            }
        }
    }
}
