using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Console.Suggestor
{
    public interface ISuggestor 
    {
        IEnumerable<ISuggestion> GetSuggestions(string input);
    }
}
