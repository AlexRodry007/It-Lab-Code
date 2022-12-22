using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DBLab
{
    public class MainMenuXmlManager
    {
        FileManager fileManager;
        public MainMenuXmlManager(FileManager mainFileManager)
        {
            fileManager = mainFileManager;
        }
        public string[] GetDatabasePaths()
        {
            DirectoryInfo directoryInfo = fileManager.GetDirectoryInfo("");
            DirectoryInfo[] databaseDirectories = directoryInfo.GetDirectories();
            int i = 0;
            string[] databasePaths = new string[databaseDirectories.Length];
            foreach(DirectoryInfo databaseInfo in databaseDirectories)
            {
                databasePaths[i] = databaseInfo.FullName;
                i++;
            }
            return databasePaths;
        }
        public void DeleteDatabase(string databaseName)
        {
            fileManager.DeleteDirectory(databaseName + "\\");
        }
        public void AddDatabase(string databaseName)
        {
            fileManager.CreateDirectory(databaseName + @"\Spreadsheets\");
        }
    }
}
