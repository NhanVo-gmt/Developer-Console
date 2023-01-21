using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Console.Command;
using Console.Processor;
using Utilities;
using UnityEngine;
using System.Text;

namespace Console.Suggestor
{
    public class ConsoleSuggestor
    {
        private List<CommandData> suggestorList = new List<CommandData>();
        private StringBuilder suggestorString = new StringBuilder();
        
        public ConsoleSuggestor()
        {

        }


        public IEnumerable<CommandData> GetCommands(string incompleteInput)
        {
            if (string.IsNullOrWhiteSpace(incompleteInput))
            {
                return Enumerable.Empty<CommandData>();
            }

            return ConsoleProcessor.GetAllCommands().Where(x => x.commandName.ContainsWithoutCaseSensitive(incompleteInput));
        }

        public StringBuilder GetFormattedCommands(string incompleteInput)
        {
            suggestorList = GetCommands(incompleteInput).OrderBy(x => x.commandName).ToList();

            suggestorString.Clear();

            for (int i = 0; i < suggestorList.Count; i++)
            {
                suggestorString.Append(suggestorList[i].commandName);
                
                if (i != suggestorList.Count)
                {
                    suggestorString.Append("\n");
                }
            }

            return suggestorString;
        }
    }
}
