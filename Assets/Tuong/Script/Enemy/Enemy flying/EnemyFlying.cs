using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

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
    public int diem = 2;
    private bool isDead = false;
    public float time;
    private Animator anim;
    private Rigidbody2D rb;
    private Vector2 startingPoint;
    public float boxX;
    public float boxY;
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
        if(distanceToPlayer > Mathf.Max(boxX,boxY))
        {
            ReturnToStartingPoint();
            return;
        }
        if(distanceToPlayer < Mathf.Max(boxX, boxY) && distanceToPlayer > shotingRange) 
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
        Gizmos.DrawWireCube(transform.position, new Vector2(boxX, boxY));
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
