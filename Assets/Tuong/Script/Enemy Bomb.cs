using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBomb : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;
    public float waitTime = 1f;
    public float detectionRange = 5f;
    public float attackRange = 1f;
    public LayerMask playerLayer;
    public GameObject exclamationMark;
    public float damage = 5f;
    private bool movingToB = true;
    private bool waiting = true;
    private float waitTimer = 0f;
    private Transform player;
    private Animator anim;
    private float originalSpeed;

    private void Start()
    {
        anim = GetComponent<Animator>();
        originalSpeed = speed;
        exclamationMark.SetActive(false);
    }

    private void Update()
    {
        player = DetectPlayer();
        if (player != null)
        {
            exclamationMark.SetActive(true);
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            FlipTowards(player.position);

            if (distanceToPlayer <= attackRange)
            {
                player.GetComponent<Health>().TakeDamage(damage);
                anim.SetBool("attack", true);
                StartCoroutine(DestroyEnemyAfterAttack());
            }
            else
            {
                anim.SetBool("attack", false);
                anim.SetBool("walking", true);
                MoveTowards(player.position, originalSpeed);
            }
        }
        else
        {
            exclamationMark.SetActive(false);
            anim.SetBool("attack", false);
            Patrol();
        }
    }

    Transform DetectPlayer()
    {
        Collider2D[] hitcolliders = Physics2D.OverlapCircleAll(transform.position, detectionRange, playerLayer);
        foreach (var hitcollider in hitcolliders)
        {
            if (hitcollider.CompareTag("Player"))
            {
                return hitcollider.transform;
            }
        }
        return null;
    }

    private void Patrol()
    {
        if (waiting)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer >= waitTime)
            {
                waiting = false;
                waitTimer = 0f;
                movingToB = !movingToB;
                Flip();
            }
            else
            {
                anim.SetBool("walking", false);
                anim.SetBool("idle", true);
                return;
            }
        }

        Vector3 targetPosition = movingToB ? pointB.position : pointA.position;
        if (Vector2.Distance(transform.position, targetPosition) <= 0.1f)
        {
            waiting = true;
        }
        else
        {
            anim.SetBool("walking", true);
            anim.SetBool("idle", false);
            MoveTowards(targetPosition, originalSpeed);
        }
    }

    void MoveTowards(Vector3 targetPosition, float currentSpeed)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, currentSpeed * Time.deltaTime);
    }

    void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    void FlipTowards(Vector3 targetPosition)
    {
        bool playerIsOnRight = targetPosition.x > transform.position.x;
        bool enemyIsFacingRight = transform.localScale.x > 0;

        if (playerIsOnRight != enemyIsFacingRight)
        {
            Flip();
        }
    }

    System.Collections.IEnumerator DestroyEnemyAfterAttack()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(pointA.position, pointB.position);
    }
}
