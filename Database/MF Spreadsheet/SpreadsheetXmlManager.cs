using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace DBLab
{
    public class SpreadsheetXmlManager
    {
        SpreadsheetManager spreadsheet;
        FileManager fileManager;
        string pathToFile;
        public SpreadsheetXmlManager(SpreadsheetManager parentSpreadsheet, FileManager mainFIleManager)
        {
            spreadsheet = parentSpreadsheet;
            fileManager = mainFIleManager;
            pathToFile = $"{spreadsheet.database.name}\\Spreadsheets\\{spreadsheet.name}.xml";
        }
        public List<List<string>> GetData(int from, int to)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(fileManager.ReadFile(pathToFile));
            List<List<string>> result = new();
            for (int i = from; i < to; i++)
            {
                List<string> row = new();
                foreach (XmlNode cell in xmlDocument.DocumentElement.LastChild.ChildNodes[i].ChildNodes)
                {
                    row.Add(cell.InnerText);
                }
                result.Add(row);
            }
            return result;
        }
        public string GetCellData(int fieldNumber, int rowNumber)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(fileManager.ReadFile(pathToFile));
            return xmlDocument.DocumentElement.LastChild.ChildNodes[rowNumber].ChildNodes[fieldNumber].InnerText;
        }
        public List<Field> GetAllFieldsFromSpreadsheet()
        {
            List<Field> result = new();
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(fileManager.ReadFile(pathToFile));
            int i = 0;
            foreach (XmlNode fieldNode in xmlDocument.DocumentElement.ChildNodes)
            {
                if (fieldNode.LocalName == "field")
                    result.Add(new Field(spreadsheet, fieldNode.Attributes.GetNamedItem("type").Value, fieldNode.Attributes.GetNamedItem("name").Value, i++));
            }
            return result;
        }
        public int CalculateSpreadsheetLength()
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(fileManager.ReadFile(pathToFile));
            return xmlDocument.LastChild.ChildNodes.Count;
        }
        public void EditCell(int field, int row, string value)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(fileManager.ReadFile(pathToFile));
            XmlNode dataNode = xmlDocument.DocumentElement.LastChild;
            dataNode.ChildNodes[row].ChildNodes[field].InnerText = value;
            fileManager.WriteFile(pathToFile, xmlDocument.OuterXml);
        }
        public void AddField(string name, string type, string defaultValue)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(fileManager.ReadFile(pathToFile));

            XmlNode newField = xmlDocument.CreateElement("field");
            XmlAttribute nameAttribute = xmlDocument.CreateAttribute("name");
            nameAttribute.Value = name;
            XmlAttribute typeAttribute = xmlDocument.CreateAttribute("type");
            typeAttribute.Value = type;
            newField.Attributes.Append(nameAttribute);
            newField.Attributes.Append(typeAttribute);

            xmlDocument.DocumentElement.InsertBefore(newField, xmlDocument.DocumentElement.LastChild);
            foreach (XmlNode rowNode in xmlDocument.DocumentElement.LastChild.ChildNodes)
            {
                XmlNode newCell = xmlDocument.CreateElement("cell");
                newCell.InnerText = defaultValue;
                rowNode.AppendChild(newCell);
            }
            fileManager.WriteFile(pathToFile, xmlDocument.OuterXml);
        }
        public void AddField(string name, string type, List<string> data)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(fileManager.ReadFile(pathToFile));

            XmlNode newField = xmlDocument.CreateElement("field");
            XmlAttribute nameAttribute = xmlDocument.CreateAttribute("name");
            nameAttribute.Value = name;
            XmlAttribute typeAttribute = xmlDocument.CreateAttribute("type");
            typeAttribute.Value = type;
            newField.Attributes.Append(nameAttribute);
            newField.Attributes.Append(typeAttribute);

            xmlDocument.DocumentElement.InsertBefore(newField, xmlDocument.DocumentElement.LastChild);
            int i = 0;
            foreach (XmlNode rowNode in xmlDocument.DocumentElement.LastChild.ChildNodes)
            {
                XmlNode newCell = xmlDocument.CreateElement("cell");
                newCell.InnerText = data[i];
                rowNode.AppendChild(newCell);
                i++;
            }
            fileManager.WriteFile(pathToFile, xmlDocument.OuterXml);
        }
        public void AddRow(List<string> data)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(fileManager.ReadFile(pathToFile));

            XmlNode row = xmlDocument.CreateElement("row");

            foreach (string colValue in data)
            {
                XmlNode newCell = xmlDocument.CreateElement("cell");
                newCell.InnerText = colValue;
                row.AppendChild(newCell);
            }

            xmlDocument.DocumentElement.LastChild.AppendChild(row);
            fileManager.WriteFile(pathToFile, xmlDocument.OuterXml);
        }
        public List<string> GetFieldData(int fieldNumber)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(fileManager.ReadFile(pathToFile));

            List<string> result = new();

            foreach(XmlNode xmlNode in xmlDocument.DocumentElement.LastChild.ChildNodes)
            {
                result.Add(xmlNode.ChildNodes[fieldNumber].InnerText);
            }
            return result;
        }
        public void DeleteField(int fieldNumber)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(fileManager.ReadFile(pathToFile));

            List<string> result = new();

            foreach (XmlNode xmlNode in xmlDocument.DocumentElement.LastChild.ChildNodes)
            {
                xmlNode.RemoveChild(xmlNode.ChildNodes[fieldNumber]);
            }
            xmlDocument.DocumentElement.RemoveChild(xmlDocument.DocumentElement.ChildNodes[fieldNumber]);
            fileManager.WriteFile(pathToFile, xmlDocument.OuterXml);
        }
    }
}
