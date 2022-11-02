using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace DBLab
{
    class MainInteractionMenu : MetaInteractionMenu
    {
        static MainMenuManager mainMenuManager;
        private static Dictionary<Func<string[], bool>, string> commandsWithDescriptions = new Dictionary<Func<string[], bool>, string>
        {
            {ShowAllDatabaseNames, "Shows all names of the databases"},
            {EditDatabase, "Allows editing of a database, needs one argument 'name'. Names are case sensitive" },
            {AddDatabase, "Creates new Database, requires one argument \"name\""},
            {DeleteDatabase, "Deletes Database, requires one argument \"name\"" }
        };
        public MainInteractionMenu(FileManager mainFileManager)
        {
            ClearCommands();
            foreach (KeyValuePair<Func<string[], bool>, string> commandWithDescription in commandsWithDescriptions)
            {
                allCommandsWithDescriptions.Add(commandWithDescription.Key, commandWithDescription.Value);
            }
            mainMenuManager = new MainMenuManager(mainFileManager);            
        }
        private static bool ShowAllDatabaseNames(string[] arguments)
        {
            mainMenuManager.ShowAllDatabaseNames();
            return true;              
        }
        private static bool EditDatabase(string[] arguments)
        {
            mainMenuManager.EditDatabase(arguments[0]);
            refreshCommands(commandsWithDescriptions);
            return true;
        }
        private static bool DeleteDatabase(string[] arguments)
        {
            mainMenuManager.DeleteDatabase(arguments[0]);
            return true;
        }
        private static bool AddDatabase(string[] arguments)
        {
            mainMenuManager.AddDatabase(arguments[0]);
            return true;
        }
    }
}
