using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLab
{
    public class FieldInteractionMenu : MetaInteractionMenu
    {
        static Field field;
        private static Dictionary<Func<string[], bool>, string> commandsWithDescriptions = new Dictionary<Func<string[], bool>, string>
        {

        };
        public FieldInteractionMenu(Field thisField)
        {
            ClearCommands();
            field = thisField;
            foreach (KeyValuePair<Func<string[], bool>, string> commandWithDescription in commandsWithDescriptions)
            {
                allCommandsWithDescriptions.Add(commandWithDescription.Key, commandWithDescription.Value);
            }
        }
    }
}
