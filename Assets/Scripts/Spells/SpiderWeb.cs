using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderWeb : Spell
{
    public void Start()
    {
        Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (!enemy) return;
        enemy.TakeDamage(damage);
    }
}
