using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlime : MonoBehaviour
{
    public float Speed = 5f;
    public float circleRadius;
    private Rigidbody2D rb;
    private Animator anim;
    public GameObject groundCheck;
    public LayerMask groundPlayer;
    public bool facingRight;
    public bool isGrounded;
    public float score;
    public float heathEnemy = 100f;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        rb.velocity = Vector2.right * Speed * Time.deltaTime;
        isGrounded = Physics2D.OverlapCircle(groundCheck.transform.position, circleRadius, groundPlayer);
        if (!isGrounded && facingRight)
        {
            Flip();
        }
        else if(!isGrounded && !facingRight)
        {
            Flip();
        }
    }
    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
        Speed  = - Speed;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.transform.position, circleRadius);
    }

    public void TakeDamage(float damage)
    {
        heathEnemy -= damage;
        if(heathEnemy <= 0)
        {
            anim.SetTrigger("die");
            PlayerAttack playerAttack = FindAnyObjectByType<PlayerAttack>();
            if (playerAttack != null)
            {
                playerAttack.AddScore(score);
            }
            Destroy(gameObject, 1f);
        }
    }
}
