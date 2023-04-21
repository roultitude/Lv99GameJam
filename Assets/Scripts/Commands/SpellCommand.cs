using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpellCommand : Command
{

    [HideInInspector] public SpellType spellType;
    public abstract void execute();

    protected float baseDamage;
    protected float baseSpeed;
    protected float baseSize;

    [HideInInspector] public float damage;
    [HideInInspector] public float speed;
    [HideInInspector] public float size;

    public void beginSpellCreation()
    {
        damage = baseDamage;
        speed = baseSpeed;
        size = baseSize;
    }
}
