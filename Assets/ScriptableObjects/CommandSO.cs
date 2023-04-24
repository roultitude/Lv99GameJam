using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/CommandSO")]
public class CommandSO : ScriptableObject
{

    public List<string> alternative;

    [Serializable]
    public enum CommandType
    {
        Modifier,
        Spell
    }

    public CommandType commandType;

    public Sprite brightSprite;
    public Sprite dullSprite;
    public string description;
}
