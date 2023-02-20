using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Utilities;

public class ConsoleLog
{
    public readonly TextMeshProUGUI logText;

    public ConsoleLog(TextMeshProUGUI logText)
    {
        this.logText = logText;
    }
    
    public void UpdateLog(string text, Type consoleLogType = Type.Input)
    {
        string log = "";
        
        switch(consoleLogType)
        {
            case Type.Input:
                log = ColorExtensions.GetColoredText(" > " + text, Color.cyan);
                break;
            case Type.Result:
                log = ColorExtensions.GetColoredText(text, Color.grey);
                break;
            case Type.Error:
                log = ColorExtensions.GetColoredText(text, Color.red);
                break;
            
        }

        logText.text += log + "\n";
    }

    public enum Type
    {
        Input,
        Result,
        Error
    }

}
