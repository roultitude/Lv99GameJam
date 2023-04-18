using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Modifier : Command
{
    public abstract Spell ApplyEffect(Spell spell);
    public abstract bool CanApplyEffect(Spell spell);


}
