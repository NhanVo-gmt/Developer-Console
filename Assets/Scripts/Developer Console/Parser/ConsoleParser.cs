using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utilities;

namespace Console.Parser 
{
    public class ConsoleParser 
    {
        public IParser[] parsers;
        private Dictionary<Type, IParser> parserLookup = new Dictionary<Type, IParser>();
        private HashSet<Type> unparserableLookup = new HashSet<Type>();
        private readonly Func<Type, string, object> recursiveParser;
        
        public ConsoleParser(IEnumerable<IParser> parsers)
        {
            this.parsers = parsers.OrderByDescending(x => x.Priority).ToArray();
            recursiveParser = Parse;
        }

        public ConsoleParser() : this(new InjectionLoader<IParser>().GetInjectedInstances())
        {
        }

        public IParser GetParser(Type type)
        {
            if (parserLookup.ContainsKey(type))
            {
                return parserLookup[type];
            }
            else if (!unparserableLookup.Contains(type))
            {
                try
                {
                    foreach (IParser parser in parsers)
                    {
                        if (parser.CanParse(type))
                        {
                            return parserLookup[type] = parser;
                        }
                    }
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }

                unparserableLookup.Add(type);
            }

            return null;
        }

        public object Parse(Type type, string value)
        {
            IParser parser = GetParser(type);
            if (parser == null)
            {
                throw new Exception();
            }

            return parser.Parse(type, value, recursiveParser);
        }
    }
}
