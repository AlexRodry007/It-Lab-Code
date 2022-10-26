using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLab
{
    class DatabaseMessanger
    {
        public static string SpreadsheetNamesMessage(List<string> names)
        {
            string message = "";
            foreach (string name in names)
            {
                message+=$"{name}\n";
            }
            return message;
        }
        public static string SpreadsheetAddingMessage(bool success)
        {
            if(success)
                return "Added spreadsheet succesfully";
            else
                return "This Name already exists";
        }
        public static string SpreadsheetDeletingMessage(bool success)
        {
            if (success)
                return "Deleted spreadsheet succesfully";
            else
                return "Invalid name, enter 'ShowAllSpreadsheetNames' to veiw all spreadsheet's names";
        }
        public static string SpreadsheetEdititngMessage(bool success)
        {
            if (success)
                return "Spreadsheet menu started";
            else
                return "Invalid name, enter 'ShowAllSpreadsheetNames' to veiw all spreadsheet's names";
        }
        public static string SpreadsheetEditingEnded = "Exited to database menu";
    }
}
