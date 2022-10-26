using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLab
{
    class SpreadsheetInteractionMenu : MetaInteractionMenu
    {
        static Spreadsheet spreadsheet;
        private static Dictionary<Func<string[], bool>, string> commandsWithDescriptions = new Dictionary<Func<string[], bool>, string>
        {
            {ShowSpreadsheet,"Shows spreadsheet data, requires two integer arguments \"from\" and \"to\"" },
            {EditCellData, "Changes cell value, first two integer arguments are \"field\" and \"row\" respectively, third argument is value" },
            {AddField, "Adds a new field, requires two atributes \"name\" and \"type\"" },
            {AddRow, "Adds a new row, and fills it with data from aruments, or default values" }
        };
        public SpreadsheetInteractionMenu(Database parentDatabase, string spreadheetName)
        {
            spreadsheet = new Spreadsheet(parentDatabase, spreadheetName);
            refreshCommands(commandsWithDescriptions);
        }
        private static bool ShowSpreadsheet(string[] arguments)
        {
            Console.WriteLine(spreadsheet.GetData(arguments[0], arguments[1]));
            return true;
        }
        private static bool EditCellData(string[] arguments)
        {
            Console.WriteLine(spreadsheet.EditData(arguments[0], arguments[1], arguments[2]));
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

    }
}
