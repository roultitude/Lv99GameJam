using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void MoveTowardsPlayer()
    {
        Vector2 moveDir = (Player.Instance.transform.position - transform.position).normalized;
        rb.MovePosition((Vector2)transform.position + moveDir*Time.fixedDeltaTime*moveSpeed);
    }
    private void FixedUpdate()
    {
        MoveTowardsPlayer();
    }
}
