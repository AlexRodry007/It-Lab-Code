using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLab
{
    
    class SpreadsheetManager
    {
        SpreadsheetUI spreadsheetUI;
        SpreadsheetDataAnalyser dataAnalyser;
        SpreadsheetXmlManager xmlManager;
        public DatabaseManager database;
        public List<Field> fields = new();
        public string name;
        private int length;
        public SpreadsheetManager(DatabaseManager parentDatabase, string spreadsheetName, FileManager mainFileManager)
        {
            name = spreadsheetName;
            database = parentDatabase;
            dataAnalyser = new SpreadsheetDataAnalyser(this);
            spreadsheetUI = new SpreadsheetUI(name);
            xmlManager = new SpreadsheetXmlManager(this, mainFileManager);
            fields = xmlManager.GetAllFieldsFromSpreadsheet();
            length = xmlManager.CalculateSpreadsheetLength();
            
        }
        public void GetData(string rawFrom, string rawTo)
        {
            int from = Convert.ToInt32(rawFrom);
            int to = Convert.ToInt32(rawTo);
            spreadsheetUI.SpreadsheetContentsMessage(fields, xmlManager.GetData(from, to));
        }
        public void EditData(string rawFieldNumber, string rawRowNumber, string rawValue) 
        {
            int fieldNumber = Convert.ToInt32(rawFieldNumber);
            int rowNumber = Convert.ToInt32(rawRowNumber);
            if (fields[fieldNumber].CheckTypeValidity(rawValue))
            {
                xmlManager.EditCell(fieldNumber, rowNumber, rawValue);
                spreadsheetUI.CellEdititngMessage(true);
            }
            else
                spreadsheetUI.CellEdititngMessage(false);
        }
        public void AddField(string name, string type)
        {
            xmlManager.AddField(name, type, dataAnalyser.CalculateDefaultValue(type));
            fields.Add(new Field(this, type, name, fields.Count));
            spreadsheetUI.FieldAddedMessage();
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
                        refinedData.Add(dataAnalyser.CalculateDefaultValue(fields[i].type));
                }
                else
                    refinedData.Add(dataAnalyser.CalculateDefaultValue(fields[i].type));
            }
            xmlManager.AddRow(refinedData);
            spreadsheetUI.RowAddedMessage();
        }
    }
}
