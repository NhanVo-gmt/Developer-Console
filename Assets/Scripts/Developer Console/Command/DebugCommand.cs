using System.Collections;
using System.Collections.Generic;
using Console.Command;
using UnityEditor;
using UnityEngine;

public static class DebugCommand
{
    [Command]
    private static void DebugLogCommand(string[] args)
    {
        string logText = string.Join(' ', args);
        Debug.Log(logText);
    }

    [Command]
    private static void DebugLogCommand(string a, string b) 
    {

    }
}

