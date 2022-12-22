using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;

namespace DBLab
{
    public class DatabaseXmlManager
    {
        string databaseName;
        string pathToSpreadsheetsFolder;
        FileManager fileManager;
        public DatabaseXmlManager(FileManager mainFileManager, string parentDatabaseName)
        {
            fileManager = mainFileManager;
            databaseName = parentDatabaseName;
            pathToSpreadsheetsFolder = $"{databaseName}\\Spreadsheets\\";
        }
        private string CalculatePath(string fileName)
        {
            return $"{pathToSpreadsheetsFolder}{fileName}.xml";
        }
        public void DeleteSpreadsheet(string spreadsheetName)
        {
            fileManager.DeleteFile(CalculatePath(spreadsheetName));
        }
        public string[] GetAllSpreadsheetPathsFromDatabase()
        {
            DirectoryInfo directoryInfo = fileManager.GetDirectoryInfo(pathToSpreadsheetsFolder);
            FileInfo[] fileInfos = directoryInfo.GetFiles();
            string[] paths = new string[fileInfos.Length];
            int i = 0;
            foreach (FileInfo fileInfo in fileInfos)
            {
                paths[i] = fileInfo.FullName;
                i++;
            }
            return paths;
        }
        public void AddSpreadsheet(string spreadsheetName)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlNode rootNode = xmlDoc.CreateElement("spreadsheet");
            XmlAttribute attribute = xmlDoc.CreateAttribute("name");
            XmlNode rows = xmlDoc.CreateElement("rows");
            attribute.Value = spreadsheetName;
            rootNode.Attributes.Append(attribute);
            rootNode.AppendChild(rows);
            xmlDoc.AppendChild(rootNode);
            fileManager.WriteFile(CalculatePath(spreadsheetName), xmlDoc.OuterXml);
        }
    }
}
