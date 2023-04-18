using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public abstract class Command : MonoBehaviour
{
    public float cooldown;
    public bool isOnCooldown { set; get; }

    public void StartCooldown()
    {
        StartCoroutine(StartCooldownInternal());
    }

    private IEnumerator StartCooldownInternal()
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(cooldown);
        isOnCooldown = false;
        yield return null;
    }

    
}
