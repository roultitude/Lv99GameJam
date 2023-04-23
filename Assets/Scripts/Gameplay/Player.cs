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
    private SpriteRenderer spriteRenderer;

    private float currentHealth;
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
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<PolygonCollider2D>();
        enemyContactColliders = new List<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Move();
        CheckForDmg();
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
