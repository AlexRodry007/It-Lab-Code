using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLab
{
    abstract class MetaInteractionMenu
    {
        protected static Dictionary<Func<string[], bool>, string> allCommandsWithDescriptions = new Dictionary<Func<string[], bool>, string>();
        private static Dictionary<Func<string[], bool>, string> metaCommandsWithDescriptions = new Dictionary<Func<string[], bool>, string>
        {
            {Help,"shows the list of commands" },
            {Exit,"exits the program" }
        };
        protected static void refreshCommands(Dictionary<Func<string[], bool>, string> commandsWithDescriptions)
        {
            ClearCommands();
            foreach (KeyValuePair<Func<string[], bool>, string> commandWithDescription in commandsWithDescriptions)
            {
                allCommandsWithDescriptions.Add(commandWithDescription.Key, commandWithDescription.Value);
            }
        }
        protected static void ClearCommands()
        {
            allCommandsWithDescriptions.Clear();
            foreach (KeyValuePair<Func<string[], bool>, string> commandWithDescription in metaCommandsWithDescriptions)
            {
                allCommandsWithDescriptions.Add(commandWithDescription.Key, commandWithDescription.Value);
            }
        }
        public void Start()
        {
            string command;
            do
            {
                Console.Write(">>");
                command = Console.ReadLine();
                Console.WriteLine("---------------");
            } while (ExecuteCommand(command));
        }
        public void Start(string message)
        {
            Console.WriteLine(message);
            string command;
            do
            {
                Console.Write(">>");
                command = Console.ReadLine();
                Console.WriteLine("---------------");
            } while (ExecuteCommand(command));
        }
        private static bool Help(string[] arguments)
        {
            foreach(KeyValuePair<Func<string[], bool>, string> commandWithDescription in allCommandsWithDescriptions)
            {
                Console.WriteLine(commandWithDescription.Key.Method.Name + ": " + commandWithDescription.Value);
            }
            return true;
        }
        private static bool Exit(string[] arguments)
        {
            return false;
        }
        public bool ExecuteCommand(string command)
        {
            string commandName = command.Split(" ")[0];
            string[] commandArguments = command.Split(" ").Skip(1).ToArray();
            foreach (KeyValuePair<Func<string[], bool>, string> commandWithDescription in allCommandsWithDescriptions)
            {
                if (commandWithDescription.Key.Method.Name.ToLower() == commandName.ToLower())
                    return commandWithDescription.Key(commandArguments);
            }
            Console.WriteLine("Invalid command, enter 'help' to see the list of commands");
            return true;
        }
    }
}
