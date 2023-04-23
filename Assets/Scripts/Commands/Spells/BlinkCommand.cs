using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkCommand : SpellCommand
{
    private float blinkDistance = 2f;
    public override void Start()
    {
        base.Start();   
        cooldown = 20f;
        commandSOName = "Blink";
        spellType = SpellType.DODGE;

    }
    public override void execute()
    {

        Player.Instance.Blink(Player.Instance.prevMoveDir * blinkDistance);

      
    }


}
