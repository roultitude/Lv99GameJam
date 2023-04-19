using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Modifier : Command
{
    public abstract Spell ApplyEffect(Spell spell);
    public virtual bool CanApplyEffect(Spell spell)
    {
        return applicableSpellTypes.Contains(spell.spellType) && !isOnCooldown;
    }

    public List<SpellType> applicableSpellTypes { get; private set; }




}
