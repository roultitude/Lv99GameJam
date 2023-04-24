using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.WSA;

public class SpellCaster : MonoBehaviour
{
    public List<Command> commands;
    Queue<CommandSO> modifierQueue = new(10);
    public static SpellCaster instance;
    bool hasSubscribed = false;
    // Start is called before the first frame update
    void Start()
    {
        commands = new List<Command>(GetComponents<Command>());
        if(instance == null) { instance = this; }
    }

    private void Update()
    {
        if(!hasSubscribed && AudioInputParser.instance != null)
        {
            AudioInputParser.instance.OnSpellCast += OnSpellCast;
            hasSubscribed = true;
        }
    }

    public void AddNewCommand(CommandSO upgrade)
    {
        string commandName = upgrade.name;
        Command commandComponent = null;

        switch(commandName)
        {
            case "Meow":
                commandComponent = gameObject.AddComponent<MeowCommand>(); break;
            case "Coconut":
                commandComponent = gameObject.AddComponent<CoconutCommand>(); break;
            case "Overwhelming":
                commandComponent = gameObject.AddComponent<OverwhelmingCommand>(); break;
            case "Slash":
                commandComponent = gameObject.AddComponent<SlashCommand>(); break;
            case "Accelerate":
                commandComponent = gameObject.AddComponent<AccelerateCommand>(); break;
            case "Blink":
                commandComponent = gameObject.AddComponent<BlinkCommand>(); break;
            case "Fast":
                commandComponent = gameObject.AddComponent<FastCommand>(); break;
            case "Its Morbin Time":
                commandComponent = gameObject.AddComponent<ItsMorbinTimeCommand>(); break;
            case "Lots Of":
                commandComponent = gameObject.AddComponent<LotsOfCommand>(); break;
            case "Painful":
                commandComponent = gameObject.AddComponent<PainfulCommand>(); break;
            case "Pricky":
                commandComponent = gameObject.AddComponent<PrickyCommand>(); break;
            case "Retreat":
                commandComponent = gameObject.AddComponent<RetreatCommand>(); break;
            case "The World":
                commandComponent = gameObject.AddComponent<TheWorldCommand>(); break;
            case "Spider":
                commandComponent = gameObject.AddComponent<SpiderCommand>(); break;
            default:
                break;
        }
        CommandCooldownUI.instance.AddCommandToUi(commandComponent, upgrade);
        commands = new List<Command>(GetComponents<Command>());
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
        toCast.CreateSpell();
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
