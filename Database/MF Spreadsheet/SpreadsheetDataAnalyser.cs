using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLab
{
    class SpreadsheetDataAnalyser
    {
        SpreadsheetManager spreadsheet;
        public SpreadsheetDataAnalyser(SpreadsheetManager parentSpreadsheet)
        {
            spreadsheet = parentSpreadsheet;
        }
        
        public string CalculateDefaultValue(string type)
        {
            switch (type)
            {
                case "int":
                    return "0";
                case "string":
                    return "empty";
                default:
                    return "error";
            }
        }
    }
}
