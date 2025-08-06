using UnityEditor;
using UnityEngine;
using System.IO;
using dev.nicklaj.clibs.deblog;

namespace Dev.Nicklaj.EditorTools
{
    [InitializeOnLoad]
    public static class PackageInstallRunner
    {
        private const string AssetPath = "Assets/Plugins/dev.nicklaj.clibs/Userdata/Resources/Categories.asset";

        static PackageInstallRunner()
        {
            // Delay execution until editor is fully loaded to avoid issues
            EditorApplication.delayCall += CheckAndCreateAsset;
        }

        private static void CheckAndCreateAsset()
        {
            // Try loading the asset
            var asset = AssetDatabase.LoadAssetAtPath<LogCategories>(AssetPath);
            if (asset == null)
            {
                Deblog.Log("Initializing Nicklaj's Toolkit.", "Nicklibs", Color.cyan);
                // Ensure directory exists
                string directory = Path.GetDirectoryName(AssetPath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                    AssetDatabase.Refresh();
                }

                // Create new instance
                LogCategories newAsset = ScriptableObject.CreateInstance<LogCategories>();

                // Create asset at path
                AssetDatabase.CreateAsset(newAsset, AssetPath);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                
                Deblog.Log($"Created Log Categories asset at {AssetPath}.", "Nicklibs", Color.aquamarine);
            }
        }
    }
}