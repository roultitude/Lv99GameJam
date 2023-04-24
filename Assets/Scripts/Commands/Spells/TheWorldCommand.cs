using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class TheWorldCommand : SpellCommand
{
    public override void Start()
    {
        base.Start();
        cooldown = 30f;
        baseDamage = 3f;
        baseSpeed = 5f;
        baseSize = 5f;
        commandSOName = "The World";
        spellType = SpellType.SUPER;
    }
    public override void execute()
    {


       foreach (Command command in SpellCaster.instance.commands)
        {
            if(command.isSpell && command.isOnCooldown && ((SpellCommand) command).spellType != SpellType.SUPER)
            {
                command.EndCooldown();
            }
        }


    }
}
