using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLab
{
    class SpreadsheetUI
    {
        string name;
        public SpreadsheetUI(string spreadsheetName)
        {
            name = spreadsheetName;
        }
        private void Separator()
        {
            Console.WriteLine($"In spreadsheet \"{name}\":");
        }
        public void SpreadsheetContentsMessage(List<Field> fields, List<List<string>> data)
        {
            Separator();
            string message = "";
            foreach (Field field in fields)
            {
                message += $"{field.name} # ";
            }
            message += "\n";
            foreach(List<string> row in data)
            {
                foreach(string cellData in row)
                {
                    message += $"{cellData} # ";
                }
                message += "\n";
            }
            Console.WriteLine(message);
        }
        public void CellEdititngMessage(bool success)
        {
            Separator();
            if (success)
                Console.WriteLine("Edit succesfull");
            else
                Console.WriteLine("Wrong type");
        }
        public void FieldAddedMessage()
        {
            Separator();
            Console.WriteLine("Field added succesfully");
        }
        public void RowAddedMessage()
        {
            Separator();
            Console.WriteLine("Row added succesfully");
        }
    }
}
