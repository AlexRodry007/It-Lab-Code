using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLab
{
    class DatabaseChecker
    {
        DatabaseManager database;
        public DatabaseChecker(DatabaseManager parentDatabase)
        {
            database = parentDatabase;
        }
        public bool CheckIfSpreadsheetExist(string name)
        {
            return database.spreadsheetNames.Contains(name);
        }

    }
}
