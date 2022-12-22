using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
using System.Xml;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace DBLab
{
    public class FileManager
    {

        private string databasesDirectoryPath;
        public FileManager(string databaseDirectoryRelaivePath)
        {
            databasesDirectoryPath = databaseDirectoryRelaivePath;
        }
        public DirectoryInfo GetDirectoryInfo(string pathFromMainDirectory)
        {
            return new DirectoryInfo(databasesDirectoryPath + pathFromMainDirectory);
        }
        public string ReadFile(string pathFromMainDirectory)
        {
            return File.ReadAllText(databasesDirectoryPath + pathFromMainDirectory);
        }                           
        public void CreateDirectory(string pathFromMainDirectory)
        {
            Directory.CreateDirectory(databasesDirectoryPath + pathFromMainDirectory);
        }
        public void DeleteDirectory(string pathFromMainDirectory)
        {
            Directory.Delete(databasesDirectoryPath + pathFromMainDirectory, true);
        }
        public void DeleteFile(string pathFromMainDirectory)
        {
            File.Delete(databasesDirectoryPath+pathFromMainDirectory);
        }
        public void WriteFile(string pathFromMainDirectory, string fileContents)
        {
            File.WriteAllText(databasesDirectoryPath + pathFromMainDirectory, fileContents);
        }
        public bool CheckIfFileExists(string pathFromMainDirectory)
        {
            return File.Exists(databasesDirectoryPath + pathFromMainDirectory);
        }
        public void StartFile(string pathFromMainDirectory)
        {
            if (!File.Exists(databasesDirectoryPath + pathFromMainDirectory))
                File.WriteAllText(databasesDirectoryPath + pathFromMainDirectory, "Default text");
            var p = new Process();
            p.StartInfo = new ProcessStartInfo(databasesDirectoryPath + pathFromMainDirectory)
            {
                UseShellExecute = true
            };
            p.Start();
        }
    }
}
