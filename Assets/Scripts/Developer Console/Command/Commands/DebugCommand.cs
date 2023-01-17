using Console.Command;
using UnityEngine;

namespace Console.Extras
{
    public static class DebugCommand
    {
        [Command]
        private static void DebugLog(string[] args)
        {
            string logText = string.Join(' ', args);
            Debug.Log(logText);
        }

        [Command]
        private static void DebugLogCommand(string a, string b) 
        {
            Debug.Log(a + "has" + b);
        }
    }
}
