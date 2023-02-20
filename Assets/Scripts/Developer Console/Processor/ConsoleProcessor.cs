using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Console.Command;
using Console.Parser;
using Console.Preprocessor;
using UnityEditor;
using UnityEngine;
using Utilities;

namespace Console.Processor
{
    public static class ConsoleProcessor 
    {
        private static Assembly[] loadedAssembly = AppDomain.CurrentDomain.GetAssemblies();
        private static Dictionary<string, CommandData> commandTable = new Dictionary<string, CommandData>();

        
    #region Create Table
        public static void GenerateCommandTable()
        {
    #if THREADS_SUPPORTED
            Parallel.ForEach(_loadedAssemblies, LoadCommandsFromAssembly);
    #else
            foreach (Assembly assembly in loadedAssembly)
            {
                LoadCommandsFromAssembly(assembly);
            }
    #endif
        }

        public static void LoadCommandsFromAssembly(Assembly assembly)
        {
            Type[] types = assembly.GetTypes();
            foreach(Type type in types)
            {
                LoadCommandsFromType(type);
            }
        }

        public static void LoadCommandsFromType(Type type)
        {
            const BindingFlags flags = 
                    BindingFlags.Static 
                    | BindingFlags.Instance 
                    | BindingFlags.Public
                    | BindingFlags.NonPublic
                    | BindingFlags.DeclaredOnly;

            MethodInfo[] methodInfos = type.GetMethods(flags);
            foreach(MethodInfo methodInfo in methodInfos)
            {
                if (methodInfo.GetCustomAttributes(typeof(CommandAttribute), false).Length > 0)
                {
                    CommandData commandData = ConsolePreprocessor.CreateCommandData(methodInfo);
                    string key = ConsolePreprocessor.CreateCommandKey(commandData);

                    commandTable.TryAdd(key, commandData);
                }
            }
        }

        public static IEnumerable<CommandData> GetAllCommands()
        {
            foreach(CommandData commandData in commandTable.Values)
            {
                yield return commandData;
            }
        }
        
    #endregion

    
    #region Invoke

        private static ConsoleParser consoleParser = new ConsoleParser();
    
        public static void InvokeCommand(string commandString) 
        {
            string[] commandParts = commandString.SplitScope(' ');

            string commandName = GetCommandName(commandParts);
            string[] commandParams = GetCommandParams(commandParts, out int paramCount);

            string commandKey = GetKey(commandName, paramCount);
            if (commandTable.ContainsKey(commandKey))
            {
                CommandData command = commandTable[commandKey];
                
                object[] parsedParamData = ParseParamData(command.types, commandParams);

                command.Invoke(parsedParamData);
            }
        }

        private static object[] ParseParamData(Type[] types, string[] commandParams)
        {
            object[] parsedParam =  new object[commandParams.Length];
            for (int i = 0; i < parsedParam.Length; i++)
            {
                parsedParam[i] = consoleParser.Parse(types[i], commandParams[i]);
            }

            return parsedParam;
        }

        public static string GetCommandName(string[] commandParts) 
        {
            return commandParts[0];
        }

        public static string[] GetCommandParams(string[] commandParts, out int paramCount)
        {
            string[] commandParams = commandParts.SubArray(1, commandParts.Length - 1);
            paramCount = commandParams.Length;
            return commandParams;
        }

        public static string GetKey(string commandName, int paramCount)
        {
            return $"{commandName}({paramCount})";
        }

    #endregion


    }
}
