using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float speed;
    public Transform rightPọint;
    public Transform leftPoint;
    private bool movingRight = true;
    private Rigidbody2D rb;
    private Animator anim;
    public float wattingTime;
    private bool isWaiting = false;
    public float detecrionRange;
    private float attackRange;
    public Transform player;
    public PolygonCollider2D colider;
    public float health;
    private bool scored;
    public float score;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        colider.enabled = false;
    }
    private void Update()
    {
        if (!isWaiting)
        {
            if (movingRight)
            {
                float disctanceToPlayer = Vector2.Distance(transform.position, player.position);
                if (disctanceToPlayer <= detecrionRange)
                {
                    MoveTowardsPlayer();
                }
                else
                {
                    Patrol();
                }
            }
        }
    }

    private void Patrol()
    {
        if (movingRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            if (transform.position.x >= rightPọint.position.x)
            {
                StartCoroutine(WaitAndFlip());
                movingRight = false;
                Flip();
            }
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            if (transform.position.x <= leftPoint.position.x)
            {
                StartCoroutine(WaitAndFlip());
                movingRight = false;
                Flip();
            }
        }
    }

    private void MoveTowardsPlayer()
    {
        if (Vector2.Distance(transform.position, player.transform.position) >= attackRange)
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime);
        }
        else
        {
            anim.SetTrigger("attack");
        }
    }
    private void Flip()
    {
        Vector2 size = transform.localScale;
        size.x *= -1;
        transform.localScale = size;
    }

    IEnumerator WaitAndFlip()
    {
        isWaiting = true;
        anim.SetBool("walk", false);
        yield return new WaitForSeconds(wattingTime);
        anim.SetBool("walk", true);
        isWaiting = false;
    }
    public void EnableAttackColider()
    {
        colider.enabled = true;
    }
    public void DisableAttackColider()
    {
        colider.enabled = false;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            PlayerAttack playerAttack = FindObjectOfType<PlayerAttack>();
            if (playerAttack != null && scored)
            {
                playerAttack.AddScore(score);
                anim.SetTrigger("die");
                scored = false;
            }
            PlayerAttack2 playerAttack2 = FindObjectOfType<PlayerAttack2>();
            if (playerAttack2 != null && scored)
            {
                playerAttack2.AddScore(score);
                anim.SetTrigger("die");
                scored = false;
            }
            //GetComponent<LootBag>().InstantiateLoot(transform.position);
            Destroy(gameObject, 5);
        }
    }
}
