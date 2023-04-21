using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PrickyCommand : ModifierCommand
{
    private float damageMultiplier = 2f;

    public void Start()
    {
        cooldown = 5f;
        commandSOName = "Pricky";
        applicableSpellTypes = new List<SpellType> {SpellType.MELEE};

    }
    public override SpellCommand ApplyEffect(SpellCommand spell)
    {
        spell.damage *= damageMultiplier;
        base.StartCooldown();
        return spell;
    }

}
