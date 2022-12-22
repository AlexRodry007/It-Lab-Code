using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLab
{
    public class Field
    {
        public SpreadsheetManager spreadsheet;
        int number;
        public string type;
        public string name;
        public Field(SpreadsheetManager parentSpreadsheet, string fieldType, string fieldName, int fieldNumber)
        {
            type = fieldType;
            spreadsheet = parentSpreadsheet;
            name = fieldName;
            number = fieldNumber;
        }
        public bool CheckTypeValidity(string rawValue, FileManager fileManager)
        {
            switch(type)
            {
                case "int":
                    if (Int32.TryParse(rawValue, out int intValue))
                        return true;
                    else
                        return false;
                case "char":
                    if (Int32.TryParse(rawValue, out int intForCharValue))
                        if (intForCharValue >= 33 && intForCharValue <= 255)
                            return true;
                        else
                            return false;
                    else
                        return false;
                case "string":
                    return true;
                case "real":
                    if (float.TryParse(rawValue, out float realValue))
                        return true;
                    else
                        return false;
                case "intinv":
                    string[] values = rawValue.Split(":");
                    if (values.Length==2)                   
                        if (Int32.TryParse(values[0], out int firstIntervalNumber) && Int32.TryParse(values[1], out int secondIntervalNumber))
                            return true;
                        else
                            return false;                    
                    else
                        return false;
                case "txt":
                    if (rawValue.EndsWith(".txt") && fileManager.CheckIfFileExists($"{spreadsheet.database.name}\\Texts\\default.txt"))
                        return true;
                    else
                        return false;
                default:
                    return false;
                
            }
        }
    }
}
