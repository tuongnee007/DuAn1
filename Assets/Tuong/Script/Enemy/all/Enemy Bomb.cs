using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBomb : MonoBehaviour
{
    public Transform attackEnemy;
    public Transform direction;
    public float Speed;
    private Transform player;
    public float shotingRangeX;
    public float shotingRangeY;
    public float fireRate = 1f;
    private float nextFireTime;
    public bool chase;
    public int diem = 2;
    private bool isDead = false;
    public float time;
    private Animator anim;
    private Rigidbody2D rb;
    private Vector2 startingPoint;
    public float boxX;
    public float boxY;
    private bool isAttacking = false;
    public float damage;
    public float pushForce;
    public GameObject exclamationMark;
    // Tuần tra
    public Transform[] patrolPoints;
    private int currentPatrolIndex = 0;
    public float patrolWaitTime = 2f;
    private float patrolTimer;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        startingPoint = transform.position;
        exclamationMark.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isDead)
        {
            return;
        }

        if (player == null)
            return;

        Chase();
    }

    private void Chase()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        bool isPlayerIsDetectionRange = Mathf.Abs(player.transform.position.x - direction.position.x) <= boxX / 2 &&
                                        Mathf.Abs(player.transform.position.y - direction.position.y) <= boxY / 2;
        if (!isPlayerIsDetectionRange)
        {
            ReturnToStartingPointOrPatrol();
            return;
        }
        if (distanceToPlayer <= Mathf.Max(shotingRangeX, shotingRangeY) && nextFireTime < Time.time)
        {
            nextFireTime = Time.time + fireRate;
            StartCoroutine(AttackandFire());
            Destroy(gameObject,1f);
        }
        else if (distanceToPlayer > Mathf.Max(shotingRangeX, shotingRangeY))
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, Speed * Time.deltaTime);
            anim.SetBool("fly", true);
            exclamationMark.gameObject.SetActive(true);
            FlipTowardsPlayer();
        }
    }

    private void ReturnToStartingPointOrPatrol()
    {
        if (patrolPoints.Length > 0)
        {
            Patrol();
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, startingPoint, Speed * Time.deltaTime);
            anim.SetBool("fly", false);
            isAttacking = false;
        }
    }

    private void Patrol()
    {
        if (patrolPoints.Length == 0) return;

        Transform targetPatrolPoint = patrolPoints[currentPatrolIndex];
        transform.position = Vector2.MoveTowards(transform.position, targetPatrolPoint.position, Speed * Time.deltaTime);
        FlipTowardsPatrolPoint(targetPatrolPoint);

        if (Vector2.Distance(transform.position, targetPatrolPoint.position) < 0.1f)
        {
            patrolTimer += Time.deltaTime;
            if (patrolTimer >= patrolWaitTime)
            {
                currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
                patrolTimer = 0f;
            }
        }
        else
        {
            patrolTimer = 0f;
        }

        anim.SetBool("fly", true);
    }

    private IEnumerator AttackandFire()
    {
        if (!isAttacking)
        {
            anim.SetTrigger("attack");
            isAttacking = true;
            yield return new WaitForSeconds(0.2f);
            DealDamage();
            isAttacking = false;
        }
    }

    private void DealDamage()
    {
        bool playerPushed = false;
        Collider2D[] hitPlayers = Physics2D.OverlapBoxAll(attackEnemy.position, new Vector2(shotingRangeX, shotingRangeY), 0);
        foreach (Collider2D hitPlayer in hitPlayers)
        {
            if (hitPlayer.gameObject.CompareTag("Player"))
            {
                Health health = hitPlayer.GetComponent<Health>();
                if (health != null)
                {
                    health.TakeDamage(damage);
                    if (!playerPushed)
                    {
                        Rigidbody2D playerRb = hitPlayer.GetComponent<Rigidbody2D>();
                        if (playerRb != null)
                        {
                            Vector2 pushDirection = hitPlayer.transform.position - transform.position;
                            pushDirection.Normalize();
                            playerRb.AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
                            playerPushed = true;
                        }
                    }
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(direction.position, new Vector2(boxX, boxY));
        Gizmos.DrawWireCube(attackEnemy.position, new Vector2(shotingRangeX, shotingRangeY));
    }

    private void FlipTowardsPlayer()
    {
        if (transform.position.x > player.transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    private void FlipTowardsPatrolPoint(Transform targetPatrolPoint)
    {
        if (transform.position.x > targetPatrolPoint.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
    

