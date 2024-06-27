using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    public float healthEnemy = 100f;
    private float currentHealth;
    private bool isGrounded;
    public float Speed = 2f;
    private bool movingRight = true;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHealth = healthEnemy;
    }

    private void Update()
    {
        Move();
        animator.SetBool("walk", isGrounded);
    }

    private void Move()
    {
        if(movingRight)
        {
            rb.velocity = new Vector2(Speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2 (-Speed, rb.velocity.x);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "grounded")
        {
            movingRight = false;
            Flip();
        }
    }

    private void Flip()
    {
        Vector3 size = transform.localScale;
        size.x *= -1;
        transform.localScale = size;
    }
}
