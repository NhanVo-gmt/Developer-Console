using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public static class ConsoleExtensions
    {
        public static Dictionary<TValue, TKey> Invert<TKey, TValue>(Dictionary<TKey, TValue> sourceDictionary)
        {
            Dictionary<TValue, TKey> newDictionary = new Dictionary<TValue, TKey>();
            foreach(TKey key in sourceDictionary.Keys)
            {
                if (!newDictionary.ContainsKey(sourceDictionary[key]))
                {
                    newDictionary.Add(sourceDictionary[key], key);
                }
            }

            return newDictionary;
        }
        
        public static T[] SubArray<T>(this T[] source, int sourceIndex, int length)
        {
            T[] result = new T[length]; 
            Array.Copy(source, sourceIndex, result, 0, length);
            return result;
        }

        public static bool ContainsWithoutCaseSensitive(this string parent, string child) 
        {
            return parent.ToUpper().Contains(child.ToUpper());
        }

        static readonly Dictionary<string, string> converTypeNameDictionary = new Dictionary<string, string>
        {
            {"Int32", "Int"},
            {"Single", "Float"}
        };

        public static string ConvertTypeName(string systemName)
        {
            if (converTypeNameDictionary.ContainsKey(systemName))
            {
                return converTypeNameDictionary[systemName];
            }

            return systemName;
        }
    }
}
