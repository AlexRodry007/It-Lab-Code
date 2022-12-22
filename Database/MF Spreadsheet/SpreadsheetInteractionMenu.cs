using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLab
{
    public class SpreadsheetInteractionMenu : MetaInteractionMenu
    {
        static SpreadsheetManager spreadsheet;
        private static Dictionary<Func<string[], bool>, string> commandsWithDescriptions = new Dictionary<Func<string[], bool>, string>
        {
            {ShowSpreadsheet,"Shows spreadsheet data, requires two integer arguments \"from\" and \"to\"" },
            {EditCellData, "Changes cell value, first two integer arguments are \"field number\" and \"row number\" respectively, third argument is value" },
            {AddField, "Adds a new field, requires two atributes \"name\" and \"type\"" },
            {AddRow, "Adds a new row, and fills it with data from aruments, or default values" },
            {DeleteField, "Deletes a field with all of it's contents, requires one attribute \"names\"" },
            {ShowCell, "Shows cell or opens a text file, requires two integer arguments are \"field number\" and \"row number\" respectively" }
        };
        public SpreadsheetInteractionMenu(DatabaseManager parentDatabase, string spreadheetName, FileManager mainFileManager)
        {
            spreadsheet = new SpreadsheetManager(parentDatabase, spreadheetName, mainFileManager);
            refreshCommands(commandsWithDescriptions);
        }
        private static bool ShowSpreadsheet(string[] arguments)
        {
            spreadsheet.ShowData(arguments[0], arguments[1]);
            return true;
        }
        private static bool EditCellData(string[] arguments)
        {
            spreadsheet.EditData(arguments[0], arguments[1], arguments[2]);
            return true;
        }
        private static bool AddField(string[] arguments)
        {
            string name = arguments[0];
            string type = arguments[1];
            spreadsheet.AddField(name, type);
            return true;
        }
        private static bool AddRow(string[] arguments)
        {
            spreadsheet.AddRow(arguments);
            return true;
        }
        private static bool DeleteField(string[] arguments)
        {
            spreadsheet.DeleteField(arguments[0]);
            return true;
        }
        private static bool ShowCell(string[] arguments)
        {
            spreadsheet.ShowCell(arguments[0], arguments[1]);
            return true;
        }

    }
}
