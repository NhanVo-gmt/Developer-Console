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

        SuggestionStack suggestionStack = new SuggestionStack();
        [SerializeField] TMP_InputField inputField;
        [SerializeField] GameObject popupGameObject;
        [SerializeField] TMP_Text popupText;
        string previousInput;
        string currentInput;

        
        ConsoleLog consoleLog;
        [SerializeField] private TextMeshProUGUI logText;


        private void OnEnable() {
            ConsoleProcessor.GenerateCommandTable();
            consoleLog = new ConsoleLog(logText);

            Application.logMessageReceived += LogCaughtException;
        }

        void Update() 
        {
            ProcessInput();

            if (Input.GetKeyDown(KeyCode.Return))
            {
                InvokeCommand();
            }
        }

        private void ProcessInput()
        {   
            previousInput = currentInput;
            currentInput = inputField.text;

            if (previousInput != currentInput)
            {
                OnInputChanged(currentInput);
            }
            else if (string.IsNullOrWhiteSpace(currentInput))
            {
                ClearSuggestionDisplay();
            }
        }

        void OnInputChanged(string input)
        {
            UpdateSuggestion(input);
        }

    #region Suggestion
        
        private void UpdateSuggestion(string input)
        {
            UpdateSuggestionDisplay(suggestionStack.GetFormattedSuggestions(input));
        }

        private void UpdateSuggestionDisplay(string commandText)
        {
            popupGameObject.SetActive(true);
            popupText.text = commandText;
        }

        public void ClearSuggestionDisplay()
        {
            popupGameObject.SetActive(false);
            popupText.text = string.Empty;
        }

    #endregion

    #region Button

        public void InvokeCommand()
        {
            string inputText = inputField.text.Trim();
            
            UpdateLog(inputText);
            
            ConsoleProcessor.InvokeCommand(inputText);
            inputField.text = string.Empty;
        }

        
    #endregion

    #region Log

        public void UpdateLog(string text)
        {
            consoleLog.UpdateLog(text);
        }

        private void LogCaughtException(string condition, string stackTrace, LogType type)
        {
            consoleLog.UpdateLog(condition, ConsoleLog.Type.Error);
        }

    #endregion

    }
}
