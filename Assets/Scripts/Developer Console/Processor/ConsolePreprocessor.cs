using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Console.Command;
using UnityEngine;

namespace Console.Preprocessor
{
    public static class ConsolePreprocessor 
    {
        #region Handle command data

        public static string CreateCommandKey(CommandData commandData)
        {
            return $"{commandData.commandName}({commandData.paramCount})";
        }

        public static CommandData CreateCommandData(MethodInfo methodInfo)
        {
            return new CommandData(methodInfo);
        }

        #endregion


        #region  Handle command string
        
        #endregion
    }
}
