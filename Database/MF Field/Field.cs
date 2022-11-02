using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLab
{
    class Field
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
        public bool CheckTypeValidity(string rawValue)
        {
            switch(type)
            {
                case "int":
                    if (Int32.TryParse(rawValue, out int refinedValue))
                        return true;
                    else
                        return false;
                case "string":
                    return true;
                default:
                    return false;
            }
        }
    }
}
