using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLab
{
    class Field
    {
        public Spreadsheet spreadsheet;
        public string type;
        public string name;
        public Field(Spreadsheet parentSpreadsheet, string fieldType, string fieldName)
        {
            type = fieldType;
            spreadsheet = parentSpreadsheet;
            name = fieldName;
        }
        public List<string> GetValuesAsStrings(int from, int to)
        {
            return FileManager.GetStringFieldFromXml(this, from, to);
           
        }
        public bool EditCell(int row, string rawValue)
        {
            if(CheckTypeValidity(rawValue))
            {
                FileManager.EditCell(this, row, rawValue);
                return true;
            }
            else
            return false;
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
