using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class SpellReferenceHelper : MonoBehaviour
{
    //Its actually a dict but lazy custom serializer
    //If your spell spawns a prefab, use this singleton class to spawn it.

    [Serializable]
    public enum SpellNames
    {
        Slash
    }
    [Serializable]
    public struct NameAndPrefab
    {
        public SpellNames name;
        public GameObject prefab;
    }
    [SerializeField]
    public List<NameAndPrefab> nameAndPrefab;

    public static SpellReferenceHelper instance;
    public void Start()
    {
        if(instance == null)
            instance = this;
    }

    public GameObject getKey(SpellNames name)
    {
        foreach (var pair in nameAndPrefab)
        {
            if (pair.name == name)
                return pair.prefab;
        }

        return null;
    }
}
