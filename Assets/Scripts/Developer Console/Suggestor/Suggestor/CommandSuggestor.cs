using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Console.Command;
using Console.Processor;
using UnityEngine;
using Utilities;

namespace Console.Suggestor
{
    public class CommandSuggestor : CachedISuggestor<CommandData>
    {
        public override ISuggestion AddToSuggestion(CommandData commandData)
        {
            return new CommandSuggestion(commandData);
        }

        public override IEnumerable<CommandData> GetItem(string input)
        {
            return ConsoleProcessor.GetAllCommands().Where(x => x.commandName.ContainsWithoutCaseSensitive(input));
        }

        public override bool IsMatch(string input)
        {
            return true;
        }
    }
}
