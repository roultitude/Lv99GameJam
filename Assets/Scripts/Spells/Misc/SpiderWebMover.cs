using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderWebMover : Spell
{
    float cooldown = 0.3f;
    float current = 0f;

    public void setCooldown(float f)
    {
        this.cooldown = f;
    }
    public void FixedUpdate()
    {
        current += Time.fixedDeltaTime;
        float moveDistance = speed * Time.fixedDeltaTime;
        transform.position += transform.right * moveDistance;
        if(current > cooldown)
        {
            current = 0f;
            //Spawn web here
            GameObject obj = Instantiate(SpellReferenceHelper.instance.getKey(SpellReferenceHelper.SpellNames.SpiderWeb), transform.position, transform.rotation);
            obj.GetComponent<Spell>().Init(damage, speed, size);
        }
    }
}
