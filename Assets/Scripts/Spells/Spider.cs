using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Spell
{
    private float lifeTime = 0.7f;

    private float attackRangeEuler = 30f;
    private float totalAttacksPerSide = 3;

    private float cooldownSide = 0.1f;
    private float cooldownMiddle = 0.2f;


    private void Start()
    {
        Destroy(gameObject, lifeTime * size);
        //Middle
        GameObject obj = Instantiate(SpellReferenceHelper.instance.getKey(SpellReferenceHelper.SpellNames.SpiderWebMover), transform);
        obj.GetComponent<Spell>().Init(damage, speed, size);
        obj.GetComponent<SpiderWebMover>().setCooldown(cooldownSide);

        //Right
        for (int x = 1; x <= totalAttacksPerSide; x++)
        {
            if(x == totalAttacksPerSide)
            {
                GameObject toSpawn = Instantiate(SpellReferenceHelper.instance.getKey(SpellReferenceHelper.SpellNames.SpiderWebMover), transform);
                toSpawn.transform.rotation *= Quaternion.Euler(0, 0, (attackRangeEuler / 2) / totalAttacksPerSide * x);
                toSpawn.GetComponent<SpiderWebMover>().setCooldown(cooldownSide);
                toSpawn.GetComponent<Spell>().Init(damage, speed, size);
            }
            else
            {
                GameObject toSpawn = Instantiate(SpellReferenceHelper.instance.getKey(SpellReferenceHelper.SpellNames.SpiderWebMover), transform);
                toSpawn.transform.rotation *= Quaternion.Euler(0, 0, (attackRangeEuler / 2) / totalAttacksPerSide * x);
                toSpawn.GetComponent<SpiderWebMover>().setCooldown(cooldownMiddle);
                toSpawn.GetComponent<Spell>().Init(damage, speed, size);
            }
        }

        //Left
        for (int x = 1; x <= totalAttacksPerSide; x++)
        {
            if (x == totalAttacksPerSide)
            {
                GameObject toSpawn = Instantiate(SpellReferenceHelper.instance.getKey(SpellReferenceHelper.SpellNames.SpiderWebMover), transform);
                toSpawn.transform.rotation *= Quaternion.Euler(0, 0, -(attackRangeEuler / 2) / totalAttacksPerSide * x);
                toSpawn.GetComponent<SpiderWebMover>().setCooldown(cooldownSide);
                toSpawn.GetComponent<Spell>().Init(damage, speed, size);
            }
            else
            {
                GameObject toSpawn = Instantiate(SpellReferenceHelper.instance.getKey(SpellReferenceHelper.SpellNames.SpiderWebMover), transform);
                toSpawn.transform.rotation *= Quaternion.Euler(0, 0, -(attackRangeEuler / 2) / totalAttacksPerSide * x);
                toSpawn.GetComponent<SpiderWebMover>().setCooldown(cooldownMiddle);
                toSpawn.GetComponent<Spell>().Init(damage, speed, size);
            }
        }

    }


}
