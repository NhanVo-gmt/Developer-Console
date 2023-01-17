using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public static class CollectionExtensions
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
    }
}
