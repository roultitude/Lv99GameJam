using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ModifierCommand : Command
{
    public abstract SpellCommand ApplyEffect(SpellCommand spell);
    public virtual bool CanApplyEffect(SpellCommand spell)
    {
        return applicableSpellTypes.Contains(spell.spellType) && !isOnCooldown;
    }

    public List<SpellType> applicableSpellTypes { get; protected set; }




}
