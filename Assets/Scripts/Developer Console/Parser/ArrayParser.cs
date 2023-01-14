using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Console.Parser
{
    public class ArrayParser : IParser
    {
        public int Priority => 10;

        public bool CanParse(Type type)
        {
            return type.IsArray;
        }

        public object Parse(Type type, string value)
        {
            return new object{};
        }
    }
}
