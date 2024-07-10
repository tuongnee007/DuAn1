using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class EnemyFlying : MonoBehaviour
{
    public float Speed;
    private Transform player;
    public float shotingRange;
    public GameObject bullet;
    public GameObject bulletParent;
    public float fireRate = 1f;
    private float nextFireTime;
    public bool chase;
    public float Health = 10f;
    public int diem = 2;
    private bool scored = true;
    private bool isDead = false;
    //Enemy
    public LayerMask whatIsPlayers;

    //public Transform startingPoint;
    private Animator anim;
    private Rigidbody2D rb;
    private Vector2 startingPoint;
    public float lineofSite = 10f;
    private bool isAttacking = false;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        startingPoint = transform.position;
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
        Flip();
    }
    private void Chase()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if(distanceToPlayer > lineofSite)
        {
            ReturnToStartingPoint();
            return;
        }
        if(distanceToPlayer < lineofSite && distanceToPlayer > shotingRange) 
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, Speed * Time.deltaTime);
            anim.SetBool("fly", true);
        }
        else if(distanceToPlayer <= shotingRange && nextFireTime < Time.time)
        {         
            nextFireTime = Time.time + fireRate;
            StartCoroutine(AttackandFire());
        }
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        if(Health <= 0)
        {
            PlayerAttack playerAttack = FindObjectOfType<PlayerAttack>();
            if(playerAttack != null && scored)
            {
                playerAttack.AddScore(diem);
                anim.SetTrigger("die");
                scored = false;
            }
            isDead = true;
            //GetComponent<LootBag>().InstantiateLoot(transform.position);
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
        Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);
        isAttacking = false;
    }

    private IEnumerator AttackandFire()
    {
        if(!isAttacking)
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
        Gizmos.DrawWireSphere(transform.position, lineofSite);
        Gizmos.DrawWireSphere(transform.position, shotingRange);
    }
    private void Flip()
    {
        if(transform.position.x > player.transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0,0,0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
