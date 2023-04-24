using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class MeowCommand : SpellCommand
{
    public void Start()
    {
        cooldown = 2f;
        baseDamage = 5f;
        baseSpeed = 2f;
        baseSize = 5f;
        commandSOName = "Meow";
        spellType = SpellType.MELEE;
    }
    public override void execute()
    {
        if (isOnCooldown)
            return;

        Vector3 rotated = (transform.rotation * Quaternion.Euler(0f, 0f, 0f) * Vector3.up ).normalized;



        GameObject obj = Instantiate(SpellReferenceHelper.instance.getKey(SpellReferenceHelper.SpellNames.Meow), transform);
        print("dmg: " + damage + "\t speed :" + speed + "\t size" + size);
        obj.GetComponent<Spell>().Init(damage, speed, size);
        
        

        base.StartCooldown();
    }
}
