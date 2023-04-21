

using UnityEngine;

public class SlashCommand : SpellCommand
{
    public void Start()
    {
        cooldown = 5f;
        baseDamage = 5f;
        baseSpeed = 3f;
        baseSize = 5f;
        commandSOName = "Slash";
    }
    public override void execute()
    {
        if (isOnCooldown)
            return;

        GameObject obj = Instantiate(SpellReferenceHelper.instance.getKey(SpellReferenceHelper.SpellNames.Slash),transform.position,transform.rotation);
        print("dmg: " + damage + "\t speed :" + speed + "\t size" + size);
        obj.GetComponent<Spell>().Init(damage, speed, size);

        base.StartCooldown();
    }
}
