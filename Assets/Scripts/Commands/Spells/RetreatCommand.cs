using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetreatCommand : SpellCommand
{
    private float blinkDistance = 2f;
    public override void Start()
    {
        base.Start();
        cooldown = 20f;
        commandSOName = "Retreat";
    }
    public override void execute()
    {

        Player.Instance.Blink(-Player.Instance.prevMoveDir * blinkDistance);

 
    }


}
