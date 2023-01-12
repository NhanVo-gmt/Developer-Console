using System;

namespace Console.Parser
{
    public interface IParser
    {
        int Priority {get; }

        bool CanParse(Type type);
        
        object Parse(string value, Type type);
    }
}
