using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    private SpriteRenderer sr;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void MoveTowardsPlayer()
    {
        Vector2 moveDir = (Player.Instance.transform.position - transform.position).normalized;
        rb.MovePosition((Vector2)transform.position + moveDir*Time.fixedDeltaTime*moveSpeed);
        sr.flipX = moveDir.x < 0;
    }
    private void FixedUpdate()
    {
        MoveTowardsPlayer();
    }
}
