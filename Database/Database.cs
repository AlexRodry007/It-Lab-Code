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
                spreadsheetNames.Add(Regex.Replace(path.Split(@"\")[^1], ".xml", ""));
            }

        }
        public bool CheckIfSpreadsheetExist(string name)
        {
            return spreadsheetNames.Contains(name);
        }
        public bool AddSpreadsheet(string spreadsheetName)
        {
            if (!FileManager.CheckIfSpreadsheetExist(name, spreadsheetName))
            {
                FileManager.AddSpreadsheet(this, spreadsheetName);
                spreadsheetNames.Add(spreadsheetName);
                return true;
            }
            else
                return false;
        }
    }
}
