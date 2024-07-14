using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("Tốc độ, độ cao,trái phải")]
    public float runSpeed = 8f;
    public float highJump = 8f;
    public float crouch = 8f;
    private float left_Right;
    private Rigidbody2D rb;
    private Animator anim;
    public ParticleSystem dust;
    public bool IsFacingRight = true;
    private bool grounded = true;
    private bool isCrouch = false;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        left_Right = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(left_Right * runSpeed, rb.velocity.y);
        flip();
        if (Input.GetKey(KeyCode.Space) && grounded)
        {
            Jump();
        }

        if (Input.GetKey(KeyCode.S))
        {
            Crouch();
        }
        anim.SetBool("run", left_Right != 0);
        anim.SetBool("grounded", grounded);
    }

    void flip()
    {
        if ((IsFacingRight && left_Right < 0) || (!IsFacingRight && left_Right > 0))
        {
            IsFacingRight = !IsFacingRight;
            Vector3 size = transform.localScale;
            size.x *= -1;
            transform.localScale = size;
            if (grounded)
            {
                CreateDust();
            }
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, highJump);
        anim.SetTrigger("jump");
        grounded = false;
        CreateDust();
    }

    private void Crouch()
    {
        if (Input.GetKey(KeyCode.S))
        {
            if (!isCrouch)
            {
                isCrouch = true;
                rb.velocity = new Vector2(crouch, rb.velocity.y);
                anim.SetBool("crouch", !isCrouch);
                CreateDust();
            }
        }
        else
        {
            isCrouch = false;
            anim.SetBool("crouch", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "grounded")
        {
            grounded = true;
        }
    }

    private void CreateDust()
    {
        dust.Play();
    }
}
