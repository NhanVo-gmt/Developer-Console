using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace Console.Parser
{
    public class ArrayParser : IParser
    {
        public int Priority => 10;

        public bool CanParse(Type type)
        {
            return type.IsArray;
        }

        public object Parse(Type type, string value, Func<Type, string, object> recursiveParser)
        {
            Type elementType = type.GetElementType();
            string[] valueParts = value.ReduceScope('[', ']').SplitScope(',');
            
            IList dataArray = Array.CreateInstance(elementType, valueParts.Length);
            for (int i = 0; i < dataArray.Count; i++)
            {
                dataArray[i] = recursiveParser(elementType, valueParts[i]);
            }
            return dataArray;
        }
    }
}
