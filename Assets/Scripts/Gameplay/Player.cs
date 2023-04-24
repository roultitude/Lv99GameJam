using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    [SerializeField]
    private float maxHealth;
    [SerializeField]
    public float moveSpeed;
    [SerializeField]
    private ContactFilter2D enemyFilter;
    [SerializeField]
    private List<Collider2D> enemyContactColliders;

    [SerializeField]
    private ContactFilter2D expFilter;
    [SerializeField]
    private List<Collider2D> expContactColliders;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    private float currentHealth;
    private float currentEXP = 0;
    private float expToNextLevel = 6;
    private float expScale = 1.3f;
    private int currentLevel {get;set;}

    private Rigidbody2D rb;
    private PolygonCollider2D collider;
    private Animator animator;

    public Vector2 prevMoveDir;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
        }
        Instance = this;
        Setup();
    }

    private void Setup()
    {
        currentHealth = maxHealth;
        currentLevel = 1;
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<PolygonCollider2D>();
        enemyContactColliders = new List<Collider2D>();
        expContactColliders = new List<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Move();
        CheckForDmg();
        CheckForExp();
    }

    private void Move()
    {
        Vector2 moveDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        if(moveDir.magnitude >= 1)
        {
            prevMoveDir = moveDir;
        }

        rb.MovePosition((Vector2) transform.position + moveDir * Time.fixedDeltaTime * moveSpeed);
        animator.SetBool("isWalking", moveDir.magnitude > 0);
        if (moveDir.x != 0)
        {
            spriteRenderer.flipX = moveDir.x < 0;
        }
        
    }

    public void Blink(Vector2 amt)
    {
        transform.position += new Vector3(amt.x, amt.y, 0);
    }

    private void CheckForDmg()
    {
        Physics2D.OverlapCollider(collider, enemyFilter, enemyContactColliders);
        foreach(Collider2D col in enemyContactColliders)
        {
            Enemy e = col.GetComponent<Enemy>();
            if (e)
            {
                changeHealth(-e.data.damage);
            }
        }
    }

    private void CheckForExp()
    {
        Physics2D.OverlapCollider(collider, expFilter, expContactColliders);
        foreach(Collider2D col in expContactColliders)
        {
            EXP e = col.GetComponent<EXP>();
            print("Touched" + col.gameObject.name);

            if (e)
            {
                
                changeEXP(e.expValue);
                e.Pool();
            }
        }
    }

    public Vector3 GetNearbyEnemyPosition()
    {
        Collider2D[] collides = Physics2D.OverlapCircleAll(transform.position, 10, enemyFilter.layerMask);
        
        if(collides.Length == 0)
        {
            return transform.position;
        } 
        Vector3 result = collides[0].gameObject.transform.position;
        foreach(Collider2D collider in collides)
        {
            if((collider.gameObject.transform.position - transform.position).sqrMagnitude <
                (result - transform.position).sqrMagnitude)
            {
                result = collider.gameObject.transform.position;
            }
        }
        return result;
    }

    private void changeEXP(float expValue)
    {
        currentEXP += expValue;
        if(currentEXP > expToNextLevel)
        {
            LevelUp();
            currentEXP -= expToNextLevel;
            expToNextLevel *= expScale;
            currentLevel += 1;

        }

    }

    private void LevelUp()
    {
        print("wow!");
    }

    private void changeHealth(float amt)
    {
        currentHealth += amt;
        if(currentHealth < 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
