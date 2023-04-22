using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public abstract class Command : MonoBehaviour
{
    [HideInInspector] public float cooldown;
    [HideInInspector] public bool isOnCooldown { set; get; }

    [HideInInspector] public string commandSOName;

    [HideInInspector] public bool isSpell;

    public void StartCooldown()
    {
        StartCoroutine(StartCooldownInternal());
    }

    public enum SpellType
    {
        MELEE,
        AOE,
        ORBITAL,
        PROJECTILE,
        DODGE,
        SUPER
    }

    private IEnumerator StartCooldownInternal()
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(cooldown);
        isOnCooldown = false;
        yield return null;
    }

    public void EndCooldown()
    {
        isOnCooldown = false;
    }

    
}
