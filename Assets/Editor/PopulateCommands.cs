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
        string spellSystemPrefabPath = "Assets/Prefabs/SpellSystem.prefab";
        GameObject spellSystemPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(spellSystemPrefabPath);

        // Get the AudioInputParser script on the prefab
        AudioInputParser audioInputParser = spellSystemPrefab.GetComponent<AudioInputParser>();
        if (audioInputParser != null)
        {
            // Set the commands list and mark the prefab as dirty
            audioInputParser.commands = allCommands;
            PrefabUtility.SaveAsPrefabAsset(spellSystemPrefab, spellSystemPrefabPath);
            Debug.Log("Populated commands list with " + allCommands.Count + " commands and saved to prefab.");
        }
        else
        {
            Debug.LogError("Could not find AudioInputParser script on the SpellSystem prefab.");
        }

        // Get the GameManager prefab
        string gameManagerPath = "Assets/Prefabs/GameManager.prefab";
        GameObject gameManagerPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(gameManagerPath);

        // Get the UpgradeManager script
        UpgradeManager upgradeManager = gameManagerPrefab.GetComponent<UpgradeManager>();
        if(upgradeManager != null)
        {
            upgradeManager.upgradePool = allCommands;
            PrefabUtility.SaveAsPrefabAsset(spellSystemPrefab, spellSystemPrefabPath);
            Debug.Log("Populated commands list with " + allCommands.Count + " commands and saved to prefab.");
        }
        else
        {
            Debug.LogError("Could not find UpgradeManager script on the GameManager prefab.");
        }
    }
}