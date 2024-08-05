using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFly2 : MonoBehaviour
{

    public float damage;
    public LayerMask WhatIsPlayers;
    public float attackRange;
    public Transform attackPlayers;
    public float desTroyDistance = 0.1f;
    private bool hasDealDamage = false;

    public float Speed;
    private Transform player;
    public float shotingRangeX;
    public float shotingRangeY;

    public float fireRate = 1f;
    private float nextFireTime;
    public bool chase;
    public float Health = 10f;
    public float MaxHealth = 10f;
    public int score = 2;
    private bool scored = true;
    private bool isDead = false;
    public float time;
    //public Transform startingPoint;
    private Animator anim;
    private Rigidbody2D rb;
    private Vector2 startingPoint;
    //public float lineofSite = 10f;
    public float boxX;
    public float boxY;
    private bool isAttacking = false;

    //Audio
    public AudioSource dieAudio;
    private void Awake()
    {
        dieAudio.Stop();
    }
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        startingPoint = transform.position;
        Health = MaxHealth;
    }
    private void Update()
    {
        if (hasDealDamage)
            return;
        if (player != null)
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);
            if (distance <= desTroyDistance)
            {
                DealDametoPlayer();
                hasDealDamage = true;
            }
        }
        if (isDead)
        {
            return;
        }

        if (player == null)
            return;
        Chase();
        Flip();
    }
    private void Chase()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distanceToPlayer > Mathf.Max(boxX, boxY))
        {
            ReturnToStartingPoint();
            return;
        }
        if (distanceToPlayer < Mathf.Max(boxX, boxY) && distanceToPlayer > Mathf.Max(shotingRangeX,shotingRangeY))
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, Speed * Time.deltaTime);
            anim.SetBool("fly", true);
        }
        else if (distanceToPlayer <= Mathf.Max(shotingRangeX, shotingRangeY) && nextFireTime < Time.time)
        {
            nextFireTime = Time.time + fireRate;
            StartCoroutine(AttackandFire());
        }
    }

    public void TakeDamage(float damage)
    {
        if (Health > 0)
        {
            Health -= damage;
        }
        if (Health <= 0)
        {
            PlayerAttack playerAttack = FindObjectOfType<PlayerAttack>();
            if (playerAttack != null && scored)
            {
                playerAttack.AddScore(score);
                anim.SetTrigger("die");
                dieAudio.Play();
                scored = false;
            }
            PlayerAttack2 playerAttack2 = FindObjectOfType<PlayerAttack2>();
            if (playerAttack2 != null && scored)
            {
                playerAttack2.AddScore(score);
                anim.SetTrigger("die");
                dieAudio.Play();
                scored = false;
            }
            isDead = true;
            Destroy(gameObject, 5);
        }
    }
    private void ReturnToStartingPoint()
    {
        transform.position = Vector2.MoveTowards(transform.position, startingPoint, Speed * Time.deltaTime);
        anim.SetBool("fly", false);
        anim.ResetTrigger("attack");
        isAttacking = false;
    }

    private void FireBullet()
    {
        isAttacking = false;
    }

    private IEnumerator AttackandFire()
    {
        if (!isAttacking)
        {
            anim.SetTrigger("attack");
            isAttacking = true;
            yield return new WaitForSeconds(0.2f);
            FireBullet();
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector2(boxX, boxY));
        Gizmos.DrawWireCube(transform.position, new Vector2(shotingRangeX,shotingRangeY));
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPlayers.position, attackRange);
    }
    private void DealDametoPlayer()
    {
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(transform.position, attackRange, WhatIsPlayers);

        foreach (Collider2D hitPlayer in hitPlayers)
        {
            Health health = hitPlayer.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }
        }
    }
    private void Flip()
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
}

