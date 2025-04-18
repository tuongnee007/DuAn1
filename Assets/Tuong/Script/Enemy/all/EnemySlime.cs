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
    //Effect
    public GameObject poison;
    public Transform poisonTransform;
    private Vector3 lastTrailPosion;
    public float traiSpacing = 0.5f;
    public float time;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        lastTrailPosion = transform.position;
        CreatePoisonSpot();
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
        if(Vector3.Distance(transform.position, lastTrailPosion)> traiSpacing)
        {
            CreatePoisonSpot();
            lastTrailPosion = transform.position;
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

    private void CreatePoisonSpot()
    {  
        GameObject poisonPost = Instantiate(poison, poisonTransform.position, Quaternion.identity);
        Destroy(poisonPost, time);
    }
}

