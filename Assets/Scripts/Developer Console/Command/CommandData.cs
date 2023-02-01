using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Utilities;

namespace Console.Command
{
    public class CommandData
    {
        public readonly string commandName;
        public readonly string commandSignature;
        public readonly string[] commandTypeSignature;
        
        public readonly MethodInfo methodInfo;
        public readonly ParameterInfo[] paramInfos;
        public readonly Type[] types;
        public readonly int paramCount;
        

        public CommandData(MethodInfo methodInfo)
        {
            this.methodInfo = methodInfo;

            commandName = methodInfo.Name;
            paramInfos = methodInfo.GetParameters();
            paramCount = paramInfos.Length;

            types = new Type[paramCount];
            if (paramCount > 1)
            {
                for (int i = 0; i < paramCount; i++)
                {
                    types[i] = paramInfos[i].ParameterType;
                }
            }
            else
            {
                types[0] = paramInfos[0].ParameterType;
            }

            commandTypeSignature = new string[paramCount];
            for (int i = 0; i < paramCount; i++)
            {
                commandTypeSignature[i] += $"{ConsoleExtensions.ConvertTypeName(types[i].Name)}";
            }
        }

        public void Invoke(object[] paramData)
        {
            methodInfo.Invoke(null, paramData);
        }
    }
}
