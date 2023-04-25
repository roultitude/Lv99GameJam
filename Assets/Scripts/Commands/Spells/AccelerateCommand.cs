using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerateCommand : SpellCommand
{

    private float moveSpeedIncrement = 5f;
    private float duration = 5f;
    public override void Start()
    {
        base.Start();  
        cooldown = 20f;
        commandSOName = "Accelerate";
        spellType = SpellType.DODGE;
    }
    public override void execute()
    {

        StartCoroutine(speedCoroutine());

    }

    private IEnumerator speedCoroutine()
    {
        Player.Instance.moveSpeed += moveSpeedIncrement;
        yield return new WaitForSeconds(duration);
        Player.Instance.moveSpeed -= moveSpeedIncrement;
    }


}
