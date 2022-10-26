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
        public string GetData(string rawFrom, string rawTo)
        {
            int from = Convert.ToInt32(rawFrom);
            int to = Convert.ToInt32(rawTo);
            return SpreadsheetMessanger.SpreadsheetContentsMessage(fields, SpreadsheetDataAnalyser.GetData(this, from, to));
        }
        public string EditData(string rawFieldNumber, string rawRowNumber, string rawValue) 
        {
            int fieldNumber = Convert.ToInt32(rawFieldNumber);
            int rowNumber = Convert.ToInt32(rawRowNumber);
            if (fields[fieldNumber].CheckTypeValidity(rawValue))
            {
                FileManager.EditCell(this, fieldNumber, rowNumber, rawValue);
                return SpreadsheetMessanger.CellEdititngMessage(true);
            }
            else
                return SpreadsheetMessanger.CellEdititngMessage(false);
        }
        public void AddField(string name, string type)
        {
            FileManager.AddField(this, name, type, CalculateDefaultValue(type));
            fields.Add(new Field(this, type, name, fields.Count));
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
