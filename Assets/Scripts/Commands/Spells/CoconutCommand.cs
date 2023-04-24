using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class CoconutCommand : SpellCommand
{
    int repeats = 3;


    public override void Start()
    {
        base.Start();
        cooldown = 5f;
        baseDamage = 5f;
        baseSpeed = 1f;
        baseSize = 1f;
        commandSOName = "Coconut";
        spellType = SpellType.PROJECTILE;
    }
    public override void execute()
    {

        for(int i = 0; i < repeats; i++)
        {
            GameObject obj = Instantiate(SpellReferenceHelper.instance.getKey(SpellReferenceHelper.SpellNames.Coconut), transform.position, transform.rotation);
            print("dmg: " + damage + "\t speed :" + speed + "\t size" + size);
            obj.GetComponent<Spell>().Init(damage, speed, size);

        }


       
    }
}
