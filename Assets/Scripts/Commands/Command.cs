using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public abstract class Command : MonoBehaviour
{
    [HideInInspector] public float cooldown;
    [HideInInspector] public bool isOnCooldown { get { return timer > 0; } }

    [HideInInspector] public string commandSOName;

    [HideInInspector] public bool isSpell;


    public float timer;
    public void StartCooldown()
    {
        timer = cooldown;
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

    public void EndCooldown()
    {
        timer = 0;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
    }

    public float GetCooldownRemaining()
    {
        if (timer < 0) return 0;
        return timer / cooldown;
    }

}
