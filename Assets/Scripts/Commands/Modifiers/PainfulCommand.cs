using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PainfulCommand : ModifierCommand
{
    private float damageMultiplier = 1.5f;

    public override void Start()
    {
        base.Start();
        cooldown = 3f;
        commandSOName = "Painful";
        applicableSpellTypes = new List<SpellType> { SpellType.MELEE, SpellType.AOE, SpellType.ORBITAL, SpellType.PROJECTILE };

    }
    public override SpellCommand ApplyEffect(SpellCommand spell)
    {
        spell.damage *= damageMultiplier;
        base.StartCooldown();
        return spell;
    }

}
