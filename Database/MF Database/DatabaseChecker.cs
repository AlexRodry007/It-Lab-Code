using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLab
{
    class DatabaseChecker
    {
        public static bool CheckIfSpreadsheetExist(string name, Database database)
        {
            return database.spreadsheetNames.Contains(name);
        }

    }
}
