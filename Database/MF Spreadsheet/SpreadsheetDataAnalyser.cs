using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLab
{
    public class SpreadsheetDataAnalyser
    {
        SpreadsheetManager spreadsheet;
        public SpreadsheetDataAnalyser(SpreadsheetManager parentSpreadsheet)
        {
            spreadsheet = parentSpreadsheet;
        }
        public bool CheckIfFieldExist(string fieldName)
        {
            foreach(Field field in spreadsheet.fields)
            {
                if (field.name == fieldName)
                    return true;
            }
            return false;
        }
        public int ConvertFieldNameToNumber(string fieldName)
        {
            for (int i = 0; i < spreadsheet.fields.Count; i++) 
            {
                if (spreadsheet.fields[i].name == fieldName)
                    return i;
            }
            return -1;
        }
        public string CalculateDefaultValue(string type)
        {
            switch (type)
            {
                case "int":
                    return "0";
                case "string":
                    return "empty";
                case "char":
                    return "33";
                case "real":
                    return "0,123";
                case "intinv":
                    return "0:1";
                case "txt":
                    return "default.txt";
                default:
                    return "error";
            }
        }
        public string ConvertCellData(string data, string type)
        {
            switch (type)
            {
                case "int":
                    return data;
                case "string":
                    return data;
                case "char":
                    Int32.TryParse(data, out int intForCharValue);                   
                    return Convert.ToString((char)intForCharValue);
                case "real":
                    return data;
                case "intinv":
                    return $"[{data}]";
                case "txt":
                    return data;
                default:
                    return "error";
            }
        }
    }
}
