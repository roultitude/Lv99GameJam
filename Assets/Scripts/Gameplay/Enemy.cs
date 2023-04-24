using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemySO data;
    public SpriteRenderer spriteRenderer;
    public float exp;

    [SerializeField]
    private float health;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void Setup(EnemySO data)
    {
        this.data = data;
        this.exp = data.expValue;
        spriteRenderer.enabled = true;
        gameObject.SetActive(true);
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
        EXPPoolManager.instance.SpawnEXP(transform.position, exp);
        spriteRenderer.enabled = false;
        gameObject.SetActive(false);
        EnemySpawner.instance.AddToPool(this);
    }
}
