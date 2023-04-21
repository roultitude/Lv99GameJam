using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : MonoBehaviour
{
    [HideInInspector] public float damage;
    [HideInInspector] public float speed;
    [HideInInspector] public float size;

    public void Init(float damage, float speed, float size)
    {
        
        this.speed = speed;
        this.damage = damage;  
        this.size = size;

        gameObject.transform.localScale =  new Vector3(size, size, size); 
    }
}
