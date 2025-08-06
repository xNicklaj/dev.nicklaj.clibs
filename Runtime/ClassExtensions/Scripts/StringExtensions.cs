using UnityEngine;

namespace dev.nicklaj.clibs.ClassExtensions
{
    public static class StringExtensions
    {
        public static string ToHex(this Color c) {
            return $"#{c.r.ToByte():X2}{c.g.ToByte():X2}{c.b.ToByte():X2}";
        }
        
        public static string SetColor(this string text, Color color)
        {
            return $"<color={color.ToHex()}>{text}</color>";
        }
    }
}