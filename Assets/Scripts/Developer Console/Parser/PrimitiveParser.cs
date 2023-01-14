using System;
using System.Collections.Generic;

namespace Console.Parser
{
    public class PrimitiveParser : IParser
    {
        private readonly HashSet<Type> primitiveTypes = new HashSet<Type>
        {
            typeof(int),
            typeof(float),
            typeof(double),
            typeof(decimal),
            typeof(char),
            typeof(string),
            typeof(bool)
        };
        
        public int Priority => 100;

        public bool CanParse(Type type)
        {
            return primitiveTypes.Contains(type);
        }

        public object Parse(Type type, string value)
        {
            try
            {
                return Convert.ChangeType(value, type);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
