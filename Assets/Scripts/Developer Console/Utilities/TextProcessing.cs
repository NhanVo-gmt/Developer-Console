using System;
using System.Collections.Generic;
using System.Linq;
using Console.Containers;
using UnityEngine; //todo

namespace Utilities
{
    public static class TextProcessing
    {
        private readonly static char[] DefaultLeftScoper = {'[', '{'};
        private readonly static char[] DefaultRightScoper = {']', '}'};
        
        public static string[] SplitScope (this string input, char separator, bool autoReducedScope = false)
        {
            return input.SplitScope(separator, DefaultLeftScoper, DefaultRightScoper);
        }


        public static string[] SplitScope<T> (this string input, char separator, T leftScopers, T rightScopers) where T : IReadOnlyList<char>
        {
            if (string.IsNullOrWhiteSpace(input)) return Array.Empty<string>();

            int[] scoperPoints = input.GetScopedSplitPoints(separator, leftScopers, rightScopers).ToArray();

            if (scoperPoints.Length == 0)
            {
                return new[]{input};
            }

            string[] scoperString = new string[scoperPoints.Length + 1];
            int lastIndex = 0;
            for (int i = 0; i < scoperPoints.Length; i++)
            {
                scoperString[i] = input.Substring(lastIndex, scoperPoints[i] - lastIndex).Trim();
                lastIndex = scoperPoints[i];
            }

            scoperString[scoperString.Length - 1] = input.Substring(lastIndex).Trim();

            return scoperString;
        }

        public static IEnumerable<int> GetScopedSplitPoints<T> (this string input, char separator, T leftScopers, T rightScopers) where T : IReadOnlyList<char>
        {
            int[] scopers = new int[leftScopers.Count];
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < leftScopers.Count; j++)
                {
                    if (input[i] == leftScopers[j]) scopers[j]++;
                    else if (input[i] == rightScopers[j]) scopers[j]--;
                }

                if (input[i] == separator && scopers.All(x => x == 0))
                {
                    yield return i;
                }
            }
        }

        public static string ReduceScope(this string input) 
        {
            return input.ReduceScope(DefaultLeftScoper, DefaultRightScoper);
        }

        public static string ReduceScope(this string input, char leftScopers, char rightScopers)
        {
            return input.ReduceScope(leftScopers.AsArraySingle(), rightScopers.AsArraySingle());
        }

        public static string ReduceScope<T> (this string input, T leftScopers, T rightScopers, int maxReduction = 1) where T : IReadOnlyList<char>
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return string.Empty;
            }
            
            if (maxReduction == 0)
            {
                return input.Trim();
            }

            string trimmedInput = input.Trim();
            for (int i = 0; i < leftScopers.Count; i++)
            {
                if (trimmedInput[0] == leftScopers[i] && trimmedInput[trimmedInput.Length - 1] == rightScopers[i])
                {
                    return trimmedInput.Substring(1, trimmedInput.Length - 2).ReduceScope(leftScopers, rightScopers, maxReduction - 1);
                }
            }

            return trimmedInput;
        }
    }
}