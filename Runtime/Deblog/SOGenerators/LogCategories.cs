using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VInspector;

namespace dev.nicklaj.clibs.deblog
{
    [CreateAssetMenu(fileName = "Log Categories", menuName = "Nicklibs/Log Categories")]
    public class LogCategories : ScriptableObject
    {
        public SerializedDictionary<string, LogCategory> Categories;

        private void OnEnable()
        {
            if(Categories == null) Categories = new SerializedDictionary<string, LogCategory>();
        }

        public Color GetColor(string category)
        {
            if (Categories.TryGetValue(category, out var cat)) 
                return cat.Color;
            
            return Color.whiteSmoke;
        }
    }

    [Serializable]
    public struct LogCategory
    {
        public Color Color;
    }
}