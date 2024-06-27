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

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //currentHealth = health;
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
    }

    //Xu li sat thuog
    //public void TakeDamage(int damage)
    //{
    //    currentHealth -= damage;
    //    if(currentHealth <= 0)
    //    {
    //        Die();
    //    }
    //}
    private void Die()
    {

    }
}
