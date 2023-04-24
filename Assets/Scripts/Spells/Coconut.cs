using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coconut : Spell
{

    private float lifeTime = 5f;
    private Vector2 XRange = new Vector2(-15, 15);
    private Vector2 YRange = new Vector2(15, 30);


    private void Start()
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce( new Vector2(Random.Range(XRange.x, XRange.y) * speed, Random.Range(YRange.x, YRange.y) * speed));
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (!enemy) return;
        enemy.TakeDamage(damage);
    }
}
