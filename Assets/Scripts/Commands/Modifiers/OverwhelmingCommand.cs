using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverwhelmingCommand : ModifierCommand
{
    float dmgMulti = 1.5f;
    float sizeMulti = 2f;

    public void Start()
    {
        cooldown = 5f;
        commandSOName = "Overwhelming";
        applicableSpellTypes = new List<SpellType> { SpellType.MELEE, SpellType.AOE, SpellType.ORBITAL, SpellType.PROJECTILE };

    }


    public override SpellCommand ApplyEffect(SpellCommand spell)
    {
       spell.damage *= dmgMulti;
       spell.size *= sizeMulti;
        base.StartCooldown();
        return spell;
    }
}
