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
            return databasesDirectoryPath + @"\" + spreadsheet.database.name + @"\Spreadsheets\" + spreadsheet.name + ".xml";
        }
        private static string CalculatePath(Field field)
        {
            return databasesDirectoryPath + @"\" + field.spreadsheet.database.name + @"\Spreadsheets\" + field.spreadsheet.name + ".xml";
        }
        private static string CalculatePath(string databaseName, string fieldName)
        {
            return databasesDirectoryPath + @"\" + databaseName + @"\Spreadsheets\" + fieldName + ".xml";
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
            return Directory.GetFiles(databasesDirectoryPath + @"\" + databaseName + @"\Spreadsheets\").Contains(CalculatePath(databaseName, spreadsheetName));
        }
        public static string[] GetAllSpreadsheetPathsFromDatabase(string databaseName)
        {
            return Directory.GetFiles(databasesDirectoryPath + @"\" + databaseName + @"\Spreadsheets\");
        }
        public static List<string> GetStringFieldFromXml(Field field, int from, int to)
        {
            List<string> result = new();
            using (XmlReader reader = XmlReader.Create(CalculatePath(field))) 
            {
                while (reader.ReadToFollowing("field"))
                {
                    if (reader.GetAttribute("name") == field.name)
                    {
                        XmlReader inner = reader.ReadSubtree();
                        for (int i = 0; i < from; i++)
                        {
                            inner.ReadToFollowing("cell");
                        }
                        for (int i = from; inner.ReadToFollowing("cell") && i <= to; i++)
                        {
                            if (inner.LocalName == "cell")
                                result.Add(inner.ReadElementContentAsString());
                        }
                        inner.Close();
                    }
                    else reader.Skip();
                }
                
            }
                return result;
        }
        public static List<Field> GetAllFieldsFromSpreadsheet(Spreadsheet spreadsheet)
        {
            List<Field> result = new();
            using (XmlReader reader = XmlReader.Create(CalculatePath(spreadsheet)))
            {
                while(reader.ReadToFollowing("field"))
                {
                    result.Add(new Field(spreadsheet, reader.GetAttribute("type"), reader.GetAttribute("name")));
                }
            }
            return result;
        }
        public static int CalculateSpreadsheetLength(Spreadsheet spreadsheet)
        {
            int length = 0;
            using (XmlReader reader = XmlReader.Create(CalculatePath(spreadsheet)))
            {
                if(reader.ReadToFollowing("field"))
                {
                    XmlReader inner = reader.ReadSubtree();
                    while (inner.ReadToFollowing("cell"))
                    {
                        length++;
                    }
                }
            }
            return length;
        }
        public static void EditCell(Field field, int row, string value)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(CalculatePath(field));
            foreach(XmlNode node in xmlDoc.DocumentElement.ChildNodes)
            {
                if(node.Attributes["name"].Value == field.name)
                {
                    node.ChildNodes[row].InnerText = value;
                    xmlDoc.Save(CalculatePath(field));
                }
            }
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
            
            for (int i = 0; i < CalculateSpreadsheetLength(spreadsheet); i++)
            {
                XmlNode newCell = xmlDoc.CreateElement("cell");
                newCell.InnerText = defaultValue;
                newField.AppendChild(newCell);
            }
            xmlDoc.DocumentElement.AppendChild(newField);
            xmlDoc.Save(CalculatePath(spreadsheet));
        }
        public static void AddRow(Spreadsheet spreadsheet, List<string> data)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(CalculatePath(spreadsheet));
            int col = 0;
            foreach(XmlNode node in xmlDoc.DocumentElement.ChildNodes)
            {
                XmlNode newCell = xmlDoc.CreateElement("cell");
                newCell.InnerText = data[col];
                node.AppendChild(newCell);
                col++;
            }
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
