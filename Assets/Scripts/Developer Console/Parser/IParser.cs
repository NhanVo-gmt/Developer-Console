using System;

namespace Console.Parser
{
    public interface IParser
    {
        int Priority {get; }

        bool CanParse(Type type);
        
        object Parse(Type type, string value);
    }
}
