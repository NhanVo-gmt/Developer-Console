using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Console.Command;
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
                    CommandData commandData = CreateCommandData(methodInfo);
                    string key = CreateCommandKey(commandData);

                    commandTable.TryAdd(key, commandData);
                }
            }
        }

        public static string CreateCommandKey(CommandData commandData)
        {
            return $"{commandData.commandName}({commandData.paramCount})";
        }

        public static CommandData CreateCommandData(MethodInfo methodInfo)
        {
            return new CommandData(methodInfo);
        }

        public static string[] GetCommandParts(string command) 
        {
            return command.Split(" ");
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
    #endregion

    

    #region Invoke
        public static void InvokeCommand(string command) 
        {
            string[] commandParts = command.Split(' ');

            string commandName = commandParts[0];
            if (commandTable.ContainsKey(commandName))
            {
                CommandData commandData = commandTable[commandName];
            }
        }

        
        private static void InvokeMethod(MethodInfo methodInfo, ParameterInfo[] parameterInfos)
        {
            methodInfo.Invoke(null, parameterInfos);
        }
    #endregion
    }
}
