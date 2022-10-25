using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLab
{
    
    class Spreadsheet
    {
        public Database database;
        public List<Field> fields = new();
        public string name;
        private int length;
        public Spreadsheet(Database parentDatabase, string spreadsheetName)
        {
            name = spreadsheetName;
            database = parentDatabase;
            fields = FileManager.GetAllFieldsFromSpreadsheet(this);
            length = FileManager.CalculateSpreadsheetLength(this);
        }
        public string GetData(int from, int to)
        {
            string result = "";
            int length = to - from;
            List<List<string>> data = new();
            foreach(Field field in fields)
            {
                result+=field.name + " #";
                data.Add(field.GetValuesAsStrings(from, to));
            }
            result += '\n';
            for (int i = 0; i<=length;i++)
            {
                foreach(List<string> dataField in data)
                {
                    result += dataField[i] + " #";
                }
                result += '\n';
            }
            return result;
        }
        public bool EditData(int fieldNumber, int rowNumber, string rawValue) 
        {
            Field field = fields[fieldNumber];
            return field.EditCell(rowNumber, rawValue);
        }
        public void AddField(string name, string type)
        {
            FileManager.AddField(this, name, type, CalculateDefaultValue(type));
            fields.Add(new Field(this, type, name));
        }
        public string CalculateDefaultValue(string type)
        {
            switch(type)
            {
                case "int":
                    return "0";
                case "string":
                    return "empty";
                default:
                    return "error";
            }
        }
        public void AddRow(string[] rawData)
        {
            int length = rawData.Length < fields.Count ? rawData.Length : fields.Count;
            List<string> refinedData = new();
            for (int i = 0; i < fields.Count; i++) 
            {
                if (i < length)
                {
                    if (fields[i].CheckTypeValidity(rawData[i]))
                        refinedData.Add(rawData[i]);
                    else
                        refinedData.Add(CalculateDefaultValue(fields[i].type));
                }
                else
                    refinedData.Add(CalculateDefaultValue(fields[i].type));
            }
            FileManager.AddRow(this, refinedData);
        }
    }
}
