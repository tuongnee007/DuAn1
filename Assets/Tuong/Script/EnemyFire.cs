using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private float damage;
    [SerializeField] private  BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;

    private float cooldownTimer = Mathf.Infinity;

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (PlayerInSight())
        {
            if(cooldownTimer >= attackCooldown)
            {

            }
        }
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x,
           new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
           0, Vector2.left, 0, playerLayer);
        return hit.collider != null;
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.Red;
    //    Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x,
    //        new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y * range, boxCollider.bounds.size.z));
    //}

    //private Rigidbody2D rb;
    //private Animator animator;
    //public float healthEnemy = 100f;
    //private float currentHealth;
    //private bool isGrounded;
    //public float Speed = 2f;
    //private bool movingRight = true;
    //private void Start()
    //{
    //    rb = GetComponent<Rigidbody2D>();
    //    animator = GetComponent<Animator>();
    //    currentHealth = healthEnemy;
    //}

    //private void Update()
    //{
    //    Move();
    //    animator.SetBool("walk", isGrounded);
    //}

    //private void Move()
    //{
    //    if(movingRight)
    //    {
    //        rb.velocity = new Vector2(Speed, rb.velocity.y);
    //    }
    //    else
    //    {
    //        rb.velocity = new Vector2 (-Speed, rb.velocity.x);
    //    }
    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if(collision.gameObject.tag == "grounded")
    //    {
    //        movingRight = false;
    //        Flip();
    //    }
    //}

    //private void Flip()
    //{
    //    Vector3 size = transform.localScale;
    //    size.x *= -1;
    //    transform.localScale = size;
    //}
}
