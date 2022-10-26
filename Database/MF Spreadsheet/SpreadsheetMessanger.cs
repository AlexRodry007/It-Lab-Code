using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLab
{
    class SpreadsheetMessanger
    {
        public static string SpreadsheetContentsMessage(List<Field> fields, List<List<string>> data)
        {
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
            return message;
        }
        public static string CellEdititngMessage(bool success)
        {
            if (success)
                return "Edit succesfull";
            else
                return "Wrong type";
        }
    }
}
