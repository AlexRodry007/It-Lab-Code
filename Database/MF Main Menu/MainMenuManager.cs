using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLab
{
    class MainMenuManager
    {
        FileManager fileManager;
        MainMenuXmlManager xmlManager;
        MainMenuDataAnalyser mainMenuDataAnalyser;
        MainMenuUI mainMenuUI;
        public MainMenuManager(FileManager mainFileManager)
        {
            fileManager = mainFileManager;
            xmlManager = new MainMenuXmlManager(mainFileManager);
            mainMenuDataAnalyser = new MainMenuDataAnalyser(this);
            mainMenuUI = new();
        }
        public void ShowAllDatabaseNames()
        {
            mainMenuUI.ShowAllNames(mainMenuDataAnalyser.ConvertPathsToNames(xmlManager.GetDatabasePaths()));
        }
        public void EditDatabase(string databaseName)
        {
            if (mainMenuDataAnalyser.CheckIfNameExists(xmlManager.GetDatabasePaths(),databaseName))
            {
                DatabaseInteractionMenu databaseInteractionMenu = new DatabaseInteractionMenu(databaseName, fileManager);
                mainMenuUI.DatabaseEditingMessage(true);
                databaseInteractionMenu.Start();
                mainMenuUI.DatabaseEditingEndedMessage();
            }
            else
                mainMenuUI.DatabaseEditingMessage(false);
        }
        public void DeleteDatabase(string databaseName)
        {
            if (mainMenuDataAnalyser.CheckIfNameExists(xmlManager.GetDatabasePaths(), databaseName))
            {
                xmlManager.DeleteDatabase(databaseName);
                mainMenuUI.DatabaseDeletingMessage(true);
            }
            else
                mainMenuUI.DatabaseDeletingMessage(false);

        }
        public void AddDatabase(string databaseName)
        {
            if (!mainMenuDataAnalyser.CheckIfNameExists(xmlManager.GetDatabasePaths(), databaseName))
            {
                xmlManager.AddDatabase(databaseName);
                mainMenuUI.DatabaseAddingMessage(true);
            }
            else
                mainMenuUI.DatabaseAddingMessage(false);
        }
    }
}
