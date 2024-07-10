using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    public float Speed = 5f;
    public float damage;
    public float circleRadius;
    private Rigidbody2D rb;
    private Animator anim;
    public GameObject groundCheck;
    public LayerMask groundPlayer;
    public bool facingRight;
    public bool isGrounded;
    public float score;
    public float healthEnemy;
    private bool isDead = false;
    private bool scored = true;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        anim.SetBool("walk", true);
    }

    private void Update()
    {
        if (isDead)
        {
            return;
        }
        rb.velocity = Vector2.left * Speed * Time.deltaTime;
        isGrounded = Physics2D.OverlapCircle(groundCheck.transform.position, circleRadius, groundPlayer);
        if (!isGrounded && facingRight)
        {
            Flip();
        }
        else if (!isGrounded && !facingRight)
        {
            Flip();
        }
    }
    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
        Speed = -Speed;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.transform.position, circleRadius);
    }

    public void TakeDamage(float damage)
    {
        healthEnemy -= damage;
        if (healthEnemy <= 0 && scored)
        {
            anim.SetTrigger("death");
            PlayerAttack playerAttack = FindAnyObjectByType<PlayerAttack>();
            if (playerAttack != null)
            {
                playerAttack.AddScore(score);              
                scored = false;
            }
            isDead = true;
            Destroy(gameObject, 2f);
        }
        else
        {
            anim.SetTrigger("take hit");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            rb.velocity = Vector2.zero;
            anim.SetBool("walk", false);
            anim.SetTrigger("attack");
            Health health = FindAnyObjectByType<Health>();
            if(health != null)
            {
                health.TakeDamage(damage);
            }
        }
    }
}
