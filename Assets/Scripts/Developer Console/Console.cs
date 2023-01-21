using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Console.Command;
using Console.Parser;
using Console.Processor;
using Console.Suggestor;
using TMPro;
using UnityEngine;
using Utilities;

namespace Console
{
    public class Console : MonoBehaviour
    {
        ConsoleParser consoleParser = new ConsoleParser();

        ConsoleSuggestor consoleSuggestor = new ConsoleSuggestor();
        [SerializeField] TMP_InputField inputField;
        [SerializeField] TMP_Text popupText;
        string previousInput;
        string currentInput;


        
        private void OnEnable() {
            ConsoleProcessor.GenerateCommandTable();
        }

        void Update() 
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log(consoleParser.parsers[0]);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                ReduceScope();
            }

            ProcessInput();
        }

        private void ProcessInput()
        {   
            previousInput = currentInput;
            currentInput = inputField.text;

            if (previousInput != currentInput)
            {
                OnInputChanged(currentInput);
            }
        }

        void OnInputChanged(string input)
        {
            UpdateSuggestionDisplay(input);
        }
        
        private void UpdateSuggestionDisplay(string input)
        {
            StringBuilder commandText =  consoleSuggestor.GetFormattedCommands(input);
            popupText.text = commandText.ToString();
        }



        //todo remove
        public void SplitScope()
        {
            string[] textSplit = TextProcessing.SplitScope(inputField.text.Trim(), ' ');
            foreach(string text in textSplit)
            {
                Debug.Log(text);
            }
        }

        //todo remove
        public void ReduceScope()
        {
            Debug.Log(inputField.text.ReduceScope());
        }


        //todo remove
        public void InvokeCommand()
        {
            ConsoleProcessor.InvokeCommand(inputField.text.Trim());
            inputField.text = string.Empty;
        }
    }
}
