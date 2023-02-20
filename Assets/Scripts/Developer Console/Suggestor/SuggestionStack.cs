using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Utilities;

namespace Console.Suggestor
{
    public class SuggestionStack
    {
        public string suggestionSet;
        public readonly ISuggestor[] suggestors;

        public StringBuilder suggestionString = new StringBuilder();

        public SuggestionStack(IEnumerable<ISuggestor> suggestors)
        {
            this.suggestors = suggestors.ToArray(); 
        }

        public SuggestionStack() : this(new InjectionLoader<ISuggestor>().GetInjectedInstances())
        {
            
        }

        public IEnumerable<ISuggestion> GetSuggestions(string incompleteInput)
        {
            return suggestors.SelectMany(x => x.GetSuggestions(incompleteInput));
        }

        public string GetFormattedSuggestions(string incompleteInput)
        {
            List<ISuggestion> suggestions = GetSuggestions(incompleteInput).ToList();

            suggestionString.Clear();
            for (int i = 0; i < suggestions.Count; i++)
            {
                suggestionString.Append($"<link={i}>");
                suggestionString.Append($"{suggestions[i].PrimarySignature}");
                suggestionString.AppendColoredText(suggestions[i].SecondarySignature, Color.gray);
                suggestionString.Append("</link>");
                suggestionString.Append("\n");
            }


            return suggestionString.ToString();
        }

    }    
}
