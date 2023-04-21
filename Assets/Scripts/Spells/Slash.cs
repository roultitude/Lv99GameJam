using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Slash : Spell
{
    [HideInInspector] public Quaternion rotBase = Quaternion.Euler(0,0,-45);
    [HideInInspector] public float rotPerSecEuler = 1f;
    [HideInInspector] public float rotIncrement = 1f;
    [HideInInspector] public float lifeTime = 4f;



    private void Start()
    {
        transform.rotation *= rotBase;
        Destroy(gameObject, 3f);
    }
    void FixedUpdate()
    {
        rotPerSecEuler = rotPerSecEuler * (1 + rotIncrement * Time.fixedDeltaTime);
        transform.rotation *= Quaternion.Euler(0, 0, rotPerSecEuler);
        GetComponent<Rigidbody2D>().velocity = (transform.rotation * Vector3.up).normalized * speed;
    }


}
