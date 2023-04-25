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

    protected Quaternion shootDirection = Quaternion.Euler(0, 0, 0);
    

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
        shootDirection = Quaternion.Euler(0, 0, 0); 
        for (int x = 1; x <= lotsOf; x++) {

            shootDirection *= Quaternion.Euler(0, 0, -45 * x);
            execute();
            shootDirection *= Quaternion.Euler(0, 0, 90 * x);
            execute();
            shootDirection *= Quaternion.Euler(0, 0, -45 * x);
        }

        execute();

        base.StartCooldown();
    }
}
