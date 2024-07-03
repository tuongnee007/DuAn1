using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player01 : MonoBehaviour
{
    public float runspeed;    
    public float highJump;
    public ParticleSystem dust;
    private float leftRight;
    private float downUp;
    private Animator anim;
    private Rigidbody2D rb;
    private bool IsFacingRight = true;
    private bool isGround = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        leftRight = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2 (leftRight * runspeed, rb.velocity.y);
        Flip();
        if (Input.GetKey(KeyCode.Space) && isGround)
        {
            Jump();
        }
        anim.SetBool("run", leftRight != 0);
        anim.SetBool("ground", isGround);
        //downUp = Input.GetAxis("Vertical");
        //rb.velocity = new Vector2(rb.velocity.x, downUp * highJump);
    }

    void Flip()
    {
        if((IsFacingRight && leftRight < 0) ||(!IsFacingRight && leftRight > 0 ))
        {
            IsFacingRight = !IsFacingRight;
            Vector3 size = transform.localScale;
            size.x *= -1;
            transform.localScale = size;
            if (isGround)
            {
                CrateDust();
            }
        }        
    }

    private void Jump()
    {
        rb.velocity = new Vector2 (rb.velocity.x, highJump);
        anim.SetTrigger("Jump");
        isGround = false;
        CrateDust();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "grounded")
        {
            isGround = true;
        }
    }
    private void CrateDust()
    {
        dust.Play();
    }

}
