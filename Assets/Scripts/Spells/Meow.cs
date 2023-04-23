using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meow : Spell
{
    private float rotPerFrameEuler = 180;
    private float lifeTime = 2f;



    private void Start()
    {
        transform.rotation *= Quaternion.Euler(0, 0, -45 );
        Destroy(gameObject, lifeTime);
    }

    
    void FixedUpdate()
    {
        print(rotPerFrameEuler * Time.fixedDeltaTime);
        transform.localRotation *= Quaternion.Euler(0, 0, rotPerFrameEuler * Time.fixedDeltaTime * speed);
        //GetComponent<Rigidbody2D>().velocity = (transform.rotation * Vector3.up).normalized * speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        print("meow is hitting");
        if (!enemy) return;
        enemy.TakeDamage(damage);
    }
}
