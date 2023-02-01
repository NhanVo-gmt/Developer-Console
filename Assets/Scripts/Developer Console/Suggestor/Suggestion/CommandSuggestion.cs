using System.Collections;
using System.Collections.Generic;
using Console.Command;
using UnityEngine;

namespace Console.Suggestor
{
    public class CommandSuggestion : ISuggestion
    {
        private readonly CommandData commandData;
        
        public string FullSignature {
            get
            {
                return string.Empty;
            }
        }

        public bool IsMatch(string input)
        {
            return PrimarySignature.Contains(input);
        }

        public string PrimarySignature => commandData.commandName;

        public string SecondarySignature {get; }


        public CommandSuggestion(CommandData commandData)
        {
            this.commandData = commandData;

            SecondarySignature = string.Empty;
            foreach(string type in commandData.commandTypeSignature)
            {
                if (!string.IsNullOrWhiteSpace(SecondarySignature))
                {
                    SecondarySignature += ", ";
                }
                else
                {
                    SecondarySignature += "<";
                }

                SecondarySignature += $"{type}";
            }

            SecondarySignature += ">";
        }
    }
}
