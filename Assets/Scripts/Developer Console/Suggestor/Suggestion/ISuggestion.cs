using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Console.Suggestor
{
    public interface ISuggestion 
    {
        string FullSignature { get; }

        string PrimarySignature { get; }

        string SecondarySignature { get; }

        bool IsMatch(string input);
    }
}
