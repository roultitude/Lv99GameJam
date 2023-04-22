using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meow : Spell
{
    private float rotPerFrameEuler = 360;
    private float lifeTime = 0.25f;



    private void Start()
    {
        transform.rotation *= Quaternion.Euler(0, 0, -45 );
        Destroy(gameObject, lifeTime);
    }

    
    void FixedUpdate()
    {
        print(rotPerFrameEuler * Time.fixedDeltaTime);
        transform.rotation *= Quaternion.Euler(0, 0, rotPerFrameEuler * Time.fixedDeltaTime);
        //GetComponent<Rigidbody2D>().velocity = (transform.rotation * Vector3.up).normalized * speed;
    }

}
