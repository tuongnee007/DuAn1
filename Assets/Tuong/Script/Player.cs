using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Tốc độ, độ cao,trái phải")]
    public float runSpeed = 8f;
    public float highJump = 8f;
    private float left_Right;
    private Rigidbody2D rb;
    private Animator anim;
    
    private bool isFacingRight = true;
    private bool grounded;

    public float health = 100f;
    private float currrentHealth;

    private bool isAttacking = false;

    public float attackRange = 3f;
    public LayerMask enemyLayers;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currrentHealth = health;
    }

    private void Update()
    {
        left_Right = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2 (left_Right * runSpeed, rb.velocity.y);
        flip();
        if (Input.GetKey(KeyCode.Space) && grounded)
        {
            Jump();
        }

        if(Input.GetMouseButtonDown(0) && !isAttacking)
        {
            Attack();
        }
        anim.SetBool("run", left_Right != 0);
        anim.SetBool("grounded" ,grounded);
    }
    void flip()
    {
        if((isFacingRight && left_Right < 0) || (!isFacingRight && left_Right > 0))
        {
            isFacingRight = !isFacingRight;
            Vector3 size = transform.localScale;
            size.x *= -1;
            transform.localScale = size;
        }
    }
    
    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, highJump);
        anim.SetTrigger("jump");
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "grounded")
        {
            grounded = true;
        }

        //if(collision.gameObject.tag == "Enemy")
        //{
        //    EnemySlime enemySlime = collision.gameObject.GetComponent<EnemySlime>();
        //    if (enemySlime != null)
        //    {
        //        enemySlime.TakeDamage(100f);
        //    }
        //}
    }

    //Xu li sat thuog
    public void TakeDamage(int damage)
    {
        currrentHealth -= damage;
        if(currrentHealth < 0)
        {
            Die();
        }
    }

    private void Die()
    {

    }

    private void Attack()
    {
        anim.SetTrigger("attack");
        isAttacking = true;
        EndAttack();
    }

    private void DealDamageToEnemies()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange,enemyLayers);
        foreach(Collider2D enemy in hitEnemies)
        {
            EnemySlime enemySlime = enemy.GetComponent<EnemySlime>();
            if(enemySlime != null)
            {
                enemySlime.TakeDamage(100);
            }
        }


    }
   private void EndAttack()
    {
        isAttacking = false;
    }
}
