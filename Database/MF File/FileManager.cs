using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using System.Xml;
using System.Text.RegularExpressions;

namespace DBLab
{
    static class FileManager
    {
        private const string databasesDirectoryPath = @"..\..\..\Databases\";
        private static string CalculatePath(Spreadsheet spreadsheet)
        {
            return $"{databasesDirectoryPath}\\{spreadsheet.database.name}\\Spreadsheets\\{spreadsheet.name}.xml";
        }
        private static string CalculatePath(Field field)
        {
            return $"{databasesDirectoryPath}\\{field.spreadsheet.database.name}\\Spreadsheets\\{field.spreadsheet.name}.xml";
        }
        private static string CalculatePath(string databaseName, string fieldName)
        {
            return $"{databasesDirectoryPath}\\{databaseName}\\Spreadsheets\\{fieldName}.xml";
        }
        private static string CalculatePath(string databaseName)
        {
            return $"{databasesDirectoryPath}\\{databaseName}\\Spreadsheets\\";
        }
        public static string[] GetAllDatabasePaths()
        {
            string[] allDatabaseNames = Directory.GetDirectories(databasesDirectoryPath);
            return allDatabaseNames;
        }
        public static bool CheckIfDatabaseExist(string databaseName)
        {
            return Directory.GetDirectories(databasesDirectoryPath).Contains(databasesDirectoryPath + databaseName);
        }
        public static bool CheckIfSpreadsheetExist(string databaseName, string spreadsheetName)
        {
            return Directory.GetFiles(CalculatePath(databaseName)).Contains(CalculatePath(databaseName, spreadsheetName));
        }
        public static string[] GetAllSpreadsheetPathsFromDatabase(string databaseName)
        {
            return Directory.GetFiles(CalculatePath(databaseName));
        }
        public static List<string> GetRow(Spreadsheet spreadsheet, int row)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(CalculatePath(spreadsheet));
            List<string> result = new List<string>();
            foreach(XmlNode cell in xmlDoc.DocumentElement.LastChild.ChildNodes[row].ChildNodes)
            {
                result.Add(cell.InnerText);
            }
            return result;
        }
        public static List<Field> GetAllFieldsFromSpreadsheet(Spreadsheet spreadsheet)
        {
            List<Field> result = new();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(CalculatePath(spreadsheet));
            int i = 0;
            foreach(XmlNode fieldNode in xmlDoc.DocumentElement.ChildNodes)
            {
                if(fieldNode.LocalName=="field")                
                    result.Add(new Field(spreadsheet, fieldNode.Attributes.GetNamedItem("type").Value, fieldNode.Attributes.GetNamedItem("name").Value,i++));
                
            }
            return result;
        }
        public static int CalculateSpreadsheetLength(Spreadsheet spreadsheet)
        {
            int length = 0;
            using (XmlReader reader = XmlReader.Create(CalculatePath(spreadsheet)))
            {
                if(reader.ReadToFollowing("data"))
                {
                    XmlReader inner = reader.ReadSubtree();
                    while (inner.ReadToFollowing("row"))
                    {
                        length++;
                    }
                }
            }
            return length;
        }
        public static void EditCell(Spreadsheet spreadsheet,int field, int row, string value)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(CalculatePath(spreadsheet));
            XmlNode dataNode = xmlDoc.DocumentElement.LastChild;
            dataNode.ChildNodes[row].ChildNodes[field].InnerText = value;
            xmlDoc.Save(CalculatePath(spreadsheet));
        }
        public static void AddField(Spreadsheet spreadsheet, string name, string type, string defaultValue)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(CalculatePath(spreadsheet));

            XmlNode newField = xmlDoc.CreateElement("field");
            XmlAttribute nameAttribute = xmlDoc.CreateAttribute("name");
            nameAttribute.Value = name;
            XmlAttribute typeAttribute = xmlDoc.CreateAttribute("type");
            typeAttribute.Value = type;
            newField.Attributes.Append(nameAttribute);
            newField.Attributes.Append(typeAttribute);

            xmlDoc.DocumentElement.InsertBefore(newField, xmlDoc.DocumentElement.LastChild);
            foreach(XmlNode rowNode in xmlDoc.DocumentElement.LastChild.ChildNodes)
            {
                XmlNode newCell = xmlDoc.CreateElement("cell");
                newCell.InnerText = defaultValue;
                rowNode.AppendChild(newCell);
            }
            xmlDoc.Save(CalculatePath(spreadsheet));
        }       
        public static void AddRow(Spreadsheet spreadsheet, List<string> data)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(CalculatePath(spreadsheet));

            XmlNode row = xmlDoc.CreateElement("row");

            foreach(string colValue in data)
            {
                XmlNode newCell = xmlDoc.CreateElement("cell");
                newCell.InnerText = colValue;
                row.AppendChild(newCell);
            }

            xmlDoc.DocumentElement.LastChild.AppendChild(row);
            xmlDoc.Save(CalculatePath(spreadsheet));
        }
        public static void AddSpreadsheet(Database database, string spreadsheetName)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlNode rootNode = xmlDoc.CreateElement("spreadsheet");
            XmlAttribute attribute = xmlDoc.CreateAttribute("name");
            attribute.Value = spreadsheetName;
            rootNode.Attributes.Append(attribute);
            xmlDoc.AppendChild(rootNode);
            xmlDoc.Save(CalculatePath(database.name, spreadsheetName));
        }
        public static void AddDatabase(string databaseName)
        {
            Directory.CreateDirectory(databasesDirectoryPath + @"\" + databaseName + @"\Spreadsheets\");
        }
        public static void DeleteDatabase(string databaseName)
        {
            Directory.Delete(databasesDirectoryPath + @"\" + databaseName + @"\", true);
        }
        public static void DeleteSpreadsheet(string databaseName, string spreadsheetName)
        {
            File.Delete(CalculatePath(databaseName, spreadsheetName));
        }

    }
}
