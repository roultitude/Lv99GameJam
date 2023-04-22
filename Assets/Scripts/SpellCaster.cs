using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.WSA;

public class SpellCaster : MonoBehaviour
{
    List<Command> commands;
    Queue<CommandSO> modifierQueue = new(10);
    bool hasSubscribed = false;
    // Start is called before the first frame update
    void Start()
    {
        commands = new List<Command>(GetComponents<Command>());
    }

    private void Update()
    {
        if(!hasSubscribed && AudioInputParser.instance != null)
        {
            AudioInputParser.instance.OnSpellCast += OnSpellCast;
            hasSubscribed = true;
        }
    }

    void AddNewCommand(string commandName)
    {
        //Todo
    }

    public void OnSpellCast(CommandSO command)
    {
        print("OnspellCast" + command.name);
        if (command.commandType == CommandSO.CommandType.Spell)
        {
            PerformSpell(command);
        }
        else
        {
            modifierQueue.Enqueue(command);
        }

    }

    private void PerformSpell(CommandSO spell)
    {
        print("Doing the spell!");
        SpellCommand toCast = (SpellCommand) FindAvailableCommand(spell);
        if(toCast == null) {
            modifierQueue.Clear();
            return;
        }

        toCast.beginSpellCreation();

        foreach(CommandSO command in modifierQueue)
        {
            ModifierCommand modifier = (ModifierCommand) FindAvailableCommand(command);
            if( modifier && modifier.applicableSpellTypes.Contains(toCast.spellType))
            {
                modifier.ApplyEffect(toCast);
            }
        }

        modifierQueue.Clear();
        toCast.execute();
    }

    private Command FindAvailableCommand(CommandSO command)
    {
        foreach (Command cmd in commands)
        {
            if (!cmd.isOnCooldown && cmd.commandSOName == command.name)
            {
                return cmd;

            }
        }
        return null;
    }
}
