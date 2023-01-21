using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Console.Command
{
    public class CommandData
    {
        public readonly string commandName;
        
        public readonly MethodInfo methodInfo;
        public readonly ParameterInfo[] paramInfos;
        public readonly Type[] types;
        public readonly int paramCount;

        public readonly string searchInfo;
        

        public CommandData(MethodInfo methodInfo)
        {
            this.methodInfo = methodInfo;

            commandName = methodInfo.Name;
            paramInfos = methodInfo.GetParameters();
            paramCount = paramInfos.Length;

            types = new Type[paramCount];
            for (int i = 0; i < paramCount; i++)
            {
                types[i] = paramInfos[i].ParameterType;
            }

            searchInfo = commandName;
            for (int i = 0; i < paramCount; i++)
            {
                searchInfo += $" <{types[i].Name}>";
            }
        }

        public void Invoke(object[] paramData)
        {
            methodInfo.Invoke(null, paramData);
        }
    }
}
