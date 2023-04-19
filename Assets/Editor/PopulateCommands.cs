using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class PopulateCommands : EditorWindow
{
    [MenuItem("Tools/Populate Commands")]
    public static void Populate()
    {
        // Get all CommandSO assets in the project
        string[] assetGUIDs = AssetDatabase.FindAssets("t:CommandSO");
        List<CommandSO> allCommands = new List<CommandSO>();
        foreach (string guid in assetGUIDs)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            CommandSO command = AssetDatabase.LoadAssetAtPath<CommandSO>(assetPath);
            allCommands.Add(command);
        }

        // Sort the commands alphabetically by name
        allCommands.Sort((x, y) => x.name.CompareTo(y.name));

        // Get the SpellSystem prefab
        string prefabPath = "Assets/Prefabs/SpellSystem.prefab";
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);

        // Get the AudioInputParser script on the prefab
        AudioInputParser audioInputParser = prefab.GetComponent<AudioInputParser>();
        if (audioInputParser != null)
        {
            // Set the commands list and mark the prefab as dirty
            audioInputParser.commands = allCommands;
            PrefabUtility.SaveAsPrefabAsset(prefab, prefabPath);
            Debug.Log("Populated commands list with " + allCommands.Count + " commands and saved to prefab.");
        }
        else
        {
            Debug.LogError("Could not find AudioInputParser script on the SpellSystem prefab.");
        }
    }
}