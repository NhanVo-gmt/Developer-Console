using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public static class CollectionExtensions
    {
        public static T[] SubArray<T>(this T[] source, int sourceIndex, int length)
        {
            T[] result = new T[length]; 
            Array.Copy(source, sourceIndex, result, 0, length);
            return result;
        }
    }
}
