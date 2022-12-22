using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLab
{
    
    public class SpreadsheetManager
    {
        SpreadsheetUI spreadsheetUI;
        SpreadsheetDataAnalyser dataAnalyser;
        SpreadsheetXmlManager xmlManager;
        FileManager fileManager;
        public DatabaseManager database;
        public List<Field> fields = new();
        public string name;
        private int spreadsheetLength;
        public SpreadsheetManager(DatabaseManager parentDatabase, string spreadsheetName, FileManager mainFileManager)
        {
            name = spreadsheetName;
            database = parentDatabase;
            dataAnalyser = new SpreadsheetDataAnalyser(this);
            spreadsheetUI = new SpreadsheetUI(name);
            fileManager = mainFileManager;
            xmlManager = new SpreadsheetXmlManager(this, mainFileManager);
            fields = xmlManager.GetAllFieldsFromSpreadsheet();
            spreadsheetLength = xmlManager.CalculateSpreadsheetLength();
            
        }
        public void ShowData(string rawFrom, string rawTo)
        {
            int from = Convert.ToInt32(rawFrom);
            int to = Convert.ToInt32(rawTo);
            spreadsheetUI.SpreadsheetContentsMessage(fields, xmlManager.GetData(from, to), dataAnalyser);
        }
        public List<List<string>> GetData()
        {
            return xmlManager.GetData(0, spreadsheetLength);
        }
        public void EditData(string rawFieldNumber, string rawRowNumber, string rawValue) 
        {
            int fieldNumber = Convert.ToInt32(rawFieldNumber);
            int rowNumber = Convert.ToInt32(rawRowNumber);
            if (fields[fieldNumber].CheckTypeValidity(rawValue, fileManager))
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
        public void AddField(Field field)
        {
            xmlManager.AddField(field.name, field.type, dataAnalyser.CalculateDefaultValue(field.type));
            fields.Add(new Field(this, field.type, name, fields.Count));
            spreadsheetUI.FieldAddedMessage();
        }
        public void AddRow(List<string> rawData)
        {
            AddRow(rawData.ToArray());
        }
        public void AddRow(string[] rawData)
        {
            int length = rawData.Length < fields.Count ? rawData.Length : fields.Count;
            List<string> refinedData = new();
            for (int i = 0; i < fields.Count; i++) 
            {
                if (i < length)
                {
                    if (fields[i].CheckTypeValidity(rawData[i], fileManager))
                        refinedData.Add(rawData[i]);
                    else
                        refinedData.Add(dataAnalyser.CalculateDefaultValue(fields[i].type));
                }
                else
                    refinedData.Add(dataAnalyser.CalculateDefaultValue(fields[i].type));
            }
            xmlManager.AddRow(refinedData);
            spreadsheetLength++;
            spreadsheetUI.RowAddedMessage();
        }
        public bool CheckIfFieldExist(string FieldName)
        {
            return dataAnalyser.CheckIfFieldExist(FieldName);
        }
        public List<string> GetAllFieldData(string fieldName)
        {
            return xmlManager.GetFieldData(dataAnalyser.ConvertFieldNameToNumber(fieldName));
        }
        public List<string> GetAllFieldData(int fieldNumber)
        {
            return xmlManager.GetFieldData(fieldNumber);
        }
        public Field GetFieldByName(string fieldName)
        {
            foreach(Field field in fields)
            {
                if (field.name == fieldName)
                    return field;
            }
            return null;
        }
        public void DeleteField(string fieldName)
        {
            xmlManager.DeleteField(dataAnalyser.ConvertFieldNameToNumber(fieldName));
            fields.RemoveAt(dataAnalyser.ConvertFieldNameToNumber(fieldName));
        }
        public void ShowCell(string rawFieldNumber, string rawRowNumber)
        {
            int fieldNumber = Convert.ToInt32(rawFieldNumber);
            int rowNumber = Convert.ToInt32(rawRowNumber);
            if (fields[fieldNumber].type == "txt")
            {
                fileManager.StartFile($"{database.name}\\Texts\\{xmlManager.GetCellData(fieldNumber, rowNumber)}");
            }
            else
                spreadsheetUI.ShowSingleCell(xmlManager.GetCellData(fieldNumber, rowNumber));
        }
    }
}
