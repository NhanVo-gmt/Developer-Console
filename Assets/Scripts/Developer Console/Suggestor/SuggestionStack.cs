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
            IEnumerable<ISuggestion> suggestions = GetSuggestions(incompleteInput);

            suggestionString.Clear();
            foreach(ISuggestion suggestion in suggestions)
            {
                suggestionString.Append(suggestion.PrimarySignature);
                suggestionString.AppendColoredText(suggestion.SecondarySignature, ColorExtention.Grey);
                suggestionString.Append("\n");
            }

            return suggestionString.ToString();
        }
    }    
}
