using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLab
{
    class DatabaseUI
    {
        string name;
        public DatabaseUI(string databaseName)
        {
            name = databaseName;

        }
        private void Separator()
        {
            Console.WriteLine($"In database \"{name}\":");
        }
        public void SpreadsheetNamesMessage(List<string> names)
        {
            Separator();
            string message = "";
            foreach (string name in names)
            {
                message+=$"{name}\n";
            }
            Console.WriteLine(message);
        }
        public void SpreadsheetAddingMessage(bool success)
        {
            Separator();
            if (success)
                Console.WriteLine("Added spreadsheet succesfully");
            else
                Console.WriteLine("This Name already exists");
        }
        public void SpreadsheetDeletingMessage(bool success)
        {
            Separator();
            if (success)
                Console.WriteLine("Deleted spreadsheet succesfully");
            else
                Console.WriteLine("Invalid name, enter 'ShowAllSpreadsheetNames' to veiw all spreadsheet's names");
        }
        public void SpreadsheetEdititngMessage(bool success)
        {
            Separator();
            if (success)
                Console.WriteLine("Spreadsheet menu started");
            else
                Console.WriteLine("Invalid name, enter 'ShowAllSpreadsheetNames' to veiw all spreadsheet's names");
        }
        public void SpreadsheetEditingEnded()
        {
            Separator();
            Console.WriteLine("Exited to database menu");
        }
    }
}
