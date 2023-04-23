using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FastCommand : ModifierCommand
{
    private float speedMultiplier = 2f;

    public override void Start()
    {
        cooldown = 5f;
        commandSOName = "Fast";
        applicableSpellTypes = new List<SpellType> { SpellType.MELEE, SpellType.AOE, SpellType.ORBITAL, SpellType.PROJECTILE };

    }
    public override SpellCommand ApplyEffect(SpellCommand spell)
    {
        spell.speed *= speedMultiplier;
        base.StartCooldown();
        return spell;
    }

}
