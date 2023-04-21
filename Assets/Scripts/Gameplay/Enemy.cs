using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemySO data;
    public SpriteRenderer spriteRenderer;

    [SerializeField]
    private float health;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void Setup(EnemySO data)
    {
        this.data = data;
        spriteRenderer.sprite = data.sprite;
        health = data.maxHP;
    }
    public void TakeDamage(float amt)
    {
        health -= amt;
        if (health < 0) 
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
