
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Windows;

public class AudioInputParser : MonoBehaviour
{

    public List<CommandSO> commands;
    public string commandHistory = "";
    public static AudioInputParser instance;
    public delegate void SpellCastEvent(CommandSO command);
    public SpellCastEvent OnSpellCast;

    private struct CommandLocation
    {
        public CommandLocation(string name, int index, CommandSO command)
        {
            this.name = name;
            this.index = index;
            this.command = command;
        }

        public string name;
        public int index;
        public CommandSO command;
    }



    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
            instance = this;

        AudioInputManager.onAudioInput += OnAudioInput;
    }

    public void OnAudioInput(string input)
    {
        commandHistory = commandHistory + FormatString(input);
        ProcessCommandHistory();

    }

    public void ProcessCommandHistory()
    {
        commandHistory = " " + commandHistory + " ";
        List<CommandLocation> modifierCommandLocations = new();
        List<CommandLocation> spellCommandLocations = new();


        foreach (CommandSO cmd in commands)
        {
            List<string> commandNames = new();
            if(cmd.alternative.Count > 0)
                commandNames.AddRange(cmd.alternative);
            commandNames.Add(cmd.name);


            //Log locations of found commands
            foreach (string alt in commandNames)
            {
                string currentCommandHistorySubstring = commandHistory;
                int currentSubstringStartIndex = 0;
                
                string altName = FormatString(alt);
                while (currentCommandHistorySubstring.Contains(altName))
                {
                    int index = currentCommandHistorySubstring.IndexOf(altName);
                    if (cmd.commandType == CommandSO.CommandType.Spell)
                    {
                        spellCommandLocations.Add(new CommandLocation(altName, currentSubstringStartIndex + index, cmd));
                    }
                    else
                    {
                        modifierCommandLocations.Add(new CommandLocation(altName, currentSubstringStartIndex + index, cmd));
                    }
                    

                    currentSubstringStartIndex += index+ altName.Length - 1;
                    currentCommandHistorySubstring = currentCommandHistorySubstring.Substring(index + alt.Length);
                }
            }
        }

        spellCommandLocations.Sort((s1, s2) => s1.index.CompareTo(s2.index));
        modifierCommandLocations.Sort((s1, s2) => s1.index.CompareTo(s2.index));

        int maxSpellCommandIndex = 0;
        
        foreach(CommandLocation spellCommandLocation in spellCommandLocations)
        {
            //print("Spell location at " + spellCommandLocation.index);

            if(spellCommandLocation.index > maxSpellCommandIndex)
            {
                maxSpellCommandIndex = spellCommandLocation.index;
            }

            foreach(CommandLocation modifierCommandLocation in modifierCommandLocations)
            {
                if(modifierCommandLocation.index <= spellCommandLocation.index)
                {
                    CastSpell(modifierCommandLocation.command);
                }
            }

            CastSpell(spellCommandLocation.command);

            //Remove all used modifiers
            modifierCommandLocations.RemoveAll( s => s.index <= spellCommandLocation.index );
        }
        print(maxSpellCommandIndex);
        //Remove all used Commands in History.
        if(maxSpellCommandIndex >= commandHistory.Length)
        {
            commandHistory = "";
        }
        else {
            commandHistory = commandHistory.NextWordAfterIndex(maxSpellCommandIndex).Trim();
        }
        

    }

    private void CastSpell(CommandSO command)
    {
        //print("Casting Spell in AudioInput");
        OnSpellCast?.Invoke(command);
    }

    private string FormatString(string input)
    {
        return " " + input.ToLower().StripPunctuation() + " ";
    }


}

public static class StringExtension
{
    public static string StripPunctuation(this string s)
    {
        var sb = new StringBuilder();
        foreach (char c in s)
        {
            if (!char.IsPunctuation(c))
                sb.Append(c);
        }
        return sb.ToString();
    }

    public static string RemoveFirstInstanceOfString(this string value, string removeString)
    {
        int index = value.IndexOf(removeString, StringComparison.Ordinal);
        return index < 0 ? value : value.Remove(index, removeString.Length);
    }

    public static string NextWordAfterIndex(this string str, int index)
    {
        // Find the start of the next word after the given index
        int start = -1;
        for (int i = index + 1; i < str.Length; i++)
        {
            if (char.IsWhiteSpace(str[i]))
            {
                continue;
            }
            start = i;
            break;
        }

        // If no next word was found, return an empty string
        if (start == -1)
        {
            return "";
        }

        // Find the end of the next word
        int end = start + 1;
        for (; end < str.Length; end++)
        {
            if (char.IsWhiteSpace(str[end]))
            {
                break;
            }
        }

        // Extract and return the next word
        return str.Substring(end);
    }
}