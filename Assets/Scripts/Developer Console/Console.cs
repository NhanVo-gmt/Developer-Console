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

        
        private void OnEnable() {
            ConsoleProcessor.GenerateCommandTable();
        }

        void Update() 
        {
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
            else if (string.IsNullOrWhiteSpace(currentInput))
            {
                ClearSuggestionDisplay();
            }
        }

        void OnInputChanged(string input)
        {
            UpdateSuggestion(input);
        }
        
        private void UpdateSuggestion(string input)
        {
            UpdateSuggestionDisplay(suggestionStack.GetFormattedSuggestions(input));
        }

        private void UpdateSuggestionDisplay(string commandText)
        {
            popupGameObject.SetActive(true);
            popupText.text = commandText;
        }


        #region Button

        public void InvokeCommand()
        {
            ConsoleProcessor.InvokeCommand(inputField.text.Trim());
            inputField.text = string.Empty;
        }

        public void ClearSuggestionDisplay()
        {
            popupGameObject.SetActive(false);
            popupText.text = string.Empty;
        }


        #endregion
    }
}
