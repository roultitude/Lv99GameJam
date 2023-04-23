using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public abstract class SpellCommand : Command
{

    [HideInInspector] public SpellType spellType;
    public abstract void execute();

    protected float baseDamage;
    protected float baseSpeed;
    protected float baseSize;
    

    public virtual void Start()
    {
        isSpell = true;
    }

    [HideInInspector] public float damage;
    [HideInInspector] public float speed;
    [HideInInspector] public float size;
    [HideInInspector]  public int lotsOf;

    public void beginSpellCreation()
    {
        damage = baseDamage;
        speed = baseSpeed;
        size = baseSize;
        lotsOf = 0;
    }

    public void CreateSpell()
    {
        if (isOnCooldown)
            return;

        print("Lots of: " + lotsOf);
        for(int x = 0; x < lotsOf; x++) {
            
            transform.rotation *= Quaternion.Euler(0, 0, -45);
            execute();
            transform.rotation *= Quaternion.Euler(0, 0, 90);
            execute();
            transform.rotation *= Quaternion.Euler(0, 0, -45);
        }

        execute();

        base.StartCooldown();
    }
}
