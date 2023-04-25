using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderCommand : SpellCommand
{
    public override void Start()
    {
        base.Start();
        cooldown = 20f;
        baseDamage = 3f;
        baseSpeed = 10f;
        baseSize = 1f;
        commandSOName = "Spider";
        spellType = SpellType.AOE;
    }
         
    public override void execute()
    {
        Quaternion quart = GetRotationToDirection(Player.Instance.GetNearbyEnemyPosition());
        GameObject obj = Instantiate(SpellReferenceHelper.instance.getKey(SpellReferenceHelper.SpellNames.Spider)
            , transform.position, quart * shootDirection);
        print("dmg: " + damage + "\t speed :" + speed + "\t size" + size);
        obj.GetComponent<Spell>().Init(damage, speed, size);



    }

    public Quaternion GetRotationToDirection(Vector3 direction)
    {
        // Get the player's forward direction
        Vector3 forward = transform.right;

        // Calculate the rotation needed to point forward in the direction of the input vector
        Quaternion rotation = Quaternion.FromToRotation(forward, direction);

        // Return the global rotation from the player to the input vector
        return rotation * Quaternion.Euler(0,0,180);
    }

}
