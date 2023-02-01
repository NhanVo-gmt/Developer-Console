using System.Text;

namespace Utilities
{
    public static class ColorExtention
    {
        public static string Grey = "#D3D3D3";

        public static void AppendColoredText(this StringBuilder stringBuilder, string text, string color)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return;
            }
            
            stringBuilder.Append($" <color={color}>{text}</color> ");
        }
    }
}