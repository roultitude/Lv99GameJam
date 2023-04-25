using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemySO data;
    public SpriteRenderer spriteRenderer;
    public float exp;
    public Color onDamageColor;

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
        DynamicTextManager.CreateText2D(transform.position, amt.ToString(),DynamicTextManager.defaultData);
        if(!DOTween.IsTweening(this.gameObject))
        {
            spriteRenderer.DOColor(onDamageColor, 0.5f).From();
        }
        health -= amt;
        if (health < 0) 
        {
            Die();
        }
    }

    public void Die()
    {
        DOTween.Kill(this);
        EXPPoolManager.instance.SpawnEXP(transform.position, exp);
        spriteRenderer.enabled = false;
        gameObject.SetActive(false);
        EnemySpawner.instance.AddToPool(this);
    }
}
