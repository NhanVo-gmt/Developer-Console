using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Console.Command
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Field | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public sealed class CommandAttribute : Attribute
    {
        public string name;
        public string description;

        public CommandAttribute(){}

        public CommandAttribute(string name)
        {
            this.name = name;
        }

        public CommandAttribute(string name, string description) : this(name)
        {
            this.description = description;
        }
    }
}
