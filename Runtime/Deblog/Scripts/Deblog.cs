using dev.nicklaj.clibs.ClassExtensions;
using UnityEngine;

namespace dev.nicklaj.clibs.deblog
{
    public static class Deblog
    {
        private static LogCategories Categories;
        
        static Deblog()
        {
            Categories = Resources.Load<LogCategories>("Categories");
            if (Categories == null)
            {
                Debug.LogWarning("LogCategories asset not found in Resources folder!");
            }
        }

        private static Color GetColor(string category)
        {
            var color = Color.whiteSmoke;
            if(Categories != null) 
                color = Categories.GetColor(category);
            return color;
        }
        
        public static void Log(string msg, string category = "General", Color color = default)
        {
            var col = color != default ? color : GetColor(category);
            Debug.Log($"[{$"{category}".SetColor(col)}] {msg}");
        }

        public static void LogInfo(string msg, string category = "General", Color color = new Color()) => Log(msg, category, color);

        public static void LogWarning(string msg, string category = "General", Color color = default)
        {
            var col = color != default ? color : GetColor(category);
            Debug.LogWarning($"[{$"{category}".SetColor(col)}] {msg}");
        }

        public static void LogError(string msg, string category = "General", Color color = default)
        {
            var col = color != default ? color : GetColor(category);
            Debug.LogError($"[{$"{category}".SetColor(col)}] {msg}");
        }
    }
}