using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class MeowCommand : SpellCommand
{
    public override void Start()
    {
        base.Start();
        cooldown = 2f;
        baseDamage = 3f;
        baseSpeed = 1f;
        baseSize = 5f;
        commandSOName = "Meow";
        spellType = SpellType.ORBITAL;
    }
    public override void execute()
    {

        Vector3 rotated = (transform.rotation * Quaternion.Euler(0f, 0f, 0f) * Vector3.up ).normalized;



        GameObject obj = Instantiate(SpellReferenceHelper.instance.getKey(SpellReferenceHelper.SpellNames.Meow), transform.position, transform.rotation);
        print("dmg: " + damage + "\t speed :" + speed + "\t size" + size);
        obj.GetComponent<Spell>().Init(damage, speed, size);
        
    }
}
