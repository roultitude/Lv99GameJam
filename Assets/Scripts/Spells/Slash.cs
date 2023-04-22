using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Slash : Spell
{
    private Quaternion rotBase = Quaternion.Euler(0,0,-45);
    private float rotPerSecEuler = 1f;
    private float rotIncrement = 1f;
    private float lifeTime = 4f;



    private void Start()
    {
        transform.rotation *= rotBase;
        Destroy(gameObject, lifeTime);
    }
    void FixedUpdate()
    {
        rotPerSecEuler = rotPerSecEuler * (1 + rotIncrement * Time.fixedDeltaTime);
        transform.rotation *= Quaternion.Euler(0, 0, rotPerSecEuler);
        GetComponent<Rigidbody2D>().velocity = (transform.rotation * Vector3.up).normalized * speed;
    }


}
