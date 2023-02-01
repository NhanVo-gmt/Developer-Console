using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Console.Suggestor
{
    public abstract class CachedISuggestor<TItem> : ISuggestor
    {
        private readonly Dictionary<TItem, ISuggestion> suggestions = new Dictionary<TItem, ISuggestion>();
        
        public abstract IEnumerable<TItem> GetItem(string input);
        public abstract ISuggestion AddToSuggestion(TItem item);

        public abstract bool IsMatch(string input);

        
        public IEnumerable<ISuggestion> GetSuggestions(string input)
        {
            return GetItem(input).Select(AddToSuggestionCache);
        }

        private ISuggestion AddToSuggestionCache(TItem item)
        {
            if (suggestions.TryGetValue(item, out ISuggestion suggestion))
            {
                return suggestion;
            }

            ISuggestion cachedSuggestion = AddToSuggestion(item);
            suggestions.Add(item, cachedSuggestion);

            return cachedSuggestion;
        }
    }
}
