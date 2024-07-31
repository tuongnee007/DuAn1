using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_test : MonoBehaviour
{
    public static Player_test Instance { get; private set; } // Singleton instance

    public float moveSpeed = 5f; // Tốc độ di chuyển
    public float jumpForce = 10f; // Lực nhảy
    public Transform groundCheck; // Kiểm tra xem nhân vật có chạm đất không
    public LayerMask groundLayer; // Lớp đất

    private Rigidbody2D rb;
    private Animator animator;
    private bool isGrounded;
    private bool facingRight = true; // Biến để lưu hướng của nhân vật

    void Awake()
    {
        // Ensure there is only one instance of Player_test
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        Jump();
        Attack();
        UpdateAnimationState();
    }

    void Move()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Đổi hướng nhân vật dựa trên hướng di chuyển
        if (moveInput > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveInput < 0 && facingRight)
        {
            Flip();
        }

        // Cập nhật trạng thái chạy của nhân vật
        animator.SetBool("isRunning", moveInput != 0);
    }

    void Jump()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animator.SetTrigger("Jump");
        }
    }

    void Attack()
    {
        if (Input.GetButtonDown("Fire1")) // Mặc định phím "Fire1" là phím chuột trái
        {
            animator.SetTrigger("Attack");
            // Xử lý logic tấn công ở đây (ví dụ: kiểm tra va chạm, gây sát thương)
        }
    }

    void Flip()
    {
        // Đổi hướng nhân vật
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    void UpdateAnimationState()
    {
        animator.SetBool("isJumping", !isGrounded);
    }
}
