using System;
using System.Text;
using UnityEngine;
using System.Collections.Generic;

namespace Utilities
{
    public static class ColorExtensions
    {
        private static Dictionary<Color, string> colorToHexDictionary = new Dictionary<Color, string>();
        public static string Grey = "#D3D3D3";

        public static void AppendColoredText(this StringBuilder stringBuilder, string text, Color color)
        {
            string hexColor = ColorToHexString(color);
            if (string.IsNullOrWhiteSpace(text))
            {
                return;
            }
            
            stringBuilder.Append($" <color=#{hexColor}>{text}</color> ");
        }

        private static string ColorToHexString(Color color)
        {
            if (colorToHexDictionary.ContainsKey(color))
            {
                return colorToHexDictionary[color];
            }

            string hexColor = ColorUtility.ToHtmlStringRGBA(color);
            colorToHexDictionary.Add(color, hexColor);

            return hexColor;
        }

        public static string GetColoredText(string text, Color color)
        {
            string hexColor = ColorToHexString(color);
            if (string.IsNullOrWhiteSpace(text))
            {
                return string.Empty;
            }
            
            return $" <color=#{hexColor}>{text}</color> ";
        }
    }
}