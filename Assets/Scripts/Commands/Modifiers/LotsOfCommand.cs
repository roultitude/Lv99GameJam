using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LotsOfCommand : ModifierCommand
{

    public override void Start()
    {
        cooldown = 5f;
        commandSOName = "Lots Of";
        applicableSpellTypes = new List<SpellType> { SpellType.MELEE, SpellType.AOE, SpellType.ORBITAL, SpellType.PROJECTILE };

    }
    public override SpellCommand ApplyEffect(SpellCommand spell)
    {
        spell.lotsOf +=1;
        base.StartCooldown();
        return spell;
    }

}
