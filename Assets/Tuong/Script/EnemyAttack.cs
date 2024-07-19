using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class EnemyMovement : MonoBehaviour
{
    public float speed;
    public float patrolRangeX;
    public float patrolRangeY;
    public float attackRangeX;
    public float attackRangeY;
    public float fireRate;
    private float nextFiretime;
    private Transform player;
    private Animator anim;
    private Rigidbody2D rb;
    private bool isDead = false;
    private bool scored = true;
    private bool isAttacking = false;
    public LayerMask whatIsPlayers;
    public float health;
    public float score;
    private Vector2 patrolStartPoint;
    private Vector2 patrolEndPoint;
    private bool movingToStart = true;
    public float damage;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        patrolStartPoint = transform.position;
        patrolEndPoint = new Vector2(transform.position.x + Mathf.Max(patrolRangeX, patrolRangeY), transform.position.y);
    }

    private void Update()
    {
        if(isDead)
            return;
        if(player == null)
        {
            return;
        }
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if(distanceToPlayer <= Mathf.Max(attackRangeX,attackRangeY) && Time.time >= nextFiretime)
        {
            Attack();
        }
        else if(distanceToPlayer<= Mathf.Max(patrolRangeX, patrolRangeY))
        {
            Chase();
        }
        else
        {
            Patrol();
        }
        Flip();
    }

    private void Patrol()
    {
        anim.SetBool("isRunning", true);
        if (movingToStart)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolStartPoint, speed * Time.deltaTime);
            if(Vector2.Distance(transform.position, patrolStartPoint) <= 0.1f)
            {
                movingToStart = false;
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolEndPoint, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, patrolStartPoint) <= 0.1f)
            {
                movingToStart = true;
            }
        }
    }
    private void Chase()
    {
        anim.SetBool("isRunning", true);
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x, transform.position.y), speed * Time.deltaTime);
    }
    private void Attack()
    {
        if (!isAttacking)
        {
            anim.SetTrigger("attack");
            nextFiretime = Time.deltaTime + fireRate;
            isAttacking = true;
            StartCoroutine(ResertAttack());
        }
    }

    private IEnumerator ResertAttack()
    {
        yield return new WaitForSeconds(nextFiretime);
        isAttacking = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((1< collision.gameObject.layer) && whatIsPlayers != 0)
        {
            Health health = collision.gameObject.GetComponent<Health>();
            if(health != null)
            {
                health.TakeDamage(damage);
            }
        }
    }

    public void TakeDamage()
    {
        health -= damage;
        if(health <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        isDead = true;
        anim.SetTrigger("die");
        PlayerAttack playerAttack = FindObjectOfType<PlayerAttack>();
        if(playerAttack != null && scored)
        {
            playerAttack.AddScore(score);
            scored = false;
        }
        Destroy(gameObject, 3f);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(patrolStartPoint, new Vector2(patrolRangeX, patrolRangeY));
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector2(attackRangeX, attackRangeY));
    }
    private void Flip()
    {
        if(transform.position.x > player.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}

