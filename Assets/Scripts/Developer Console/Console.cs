using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Console.Parser;
using Console.Processor;
using TMPro;
using UnityEngine;
using Utilites;

namespace Console
{
    public class Console : MonoBehaviour
    {
        [SerializeField] TMP_InputField inputField;
        ConsoleParser consoleParser = new ConsoleParser();
        
        private void OnEnable() {
            ConsoleProcessor.GenerateCommandTable();
        }

        void Update() 
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log(consoleParser.parsers[0]);
            }
        }

        public void InvokeCommand()
        {
            ConsoleProcessor.InvokeCommand(inputField.text.Trim());
            inputField.text = string.Empty;
        }
    }
}
