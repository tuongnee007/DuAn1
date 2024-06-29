using System.Collections;
using System.Collections.Generic;
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
    //public Transform startingPoint;
    private Animator anim;
    private Rigidbody2D rb;
    //public bool isFly = true;
    //public float attackCooldown = 2f;
    //private float attackTimer = 0f;

    public float lineofSite = 10f;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (player == null)
            return;

            Chase();
        //else
        //{
        //    ReturnStartPoint();
        //}
        Flip();
    }

    //private void ReturnStartPoint()
    //{
    //    transform.position = Vector2.MoveTowards(transform.position, startingPoint.position, Speed * Time.deltaTime);
    //    if(Vector2.Distance(transform.position , startingPoint.position) < 0.1f)
    //    {
    //        anim.SetBool("fly", false);
    //        anim.ResetTrigger("attack");
    //    }
    //}
    private void Chase()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if(distanceToPlayer < lineofSite && distanceToPlayer > shotingRange) 
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, Speed * Time.deltaTime);
        }
        else if(distanceToPlayer <= shotingRange && nextFireTime < Time.time)
        {
            Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);
            nextFireTime = Time.time + fireRate; 
        }


        //if (attackTimer <= 0)
        //{
        //    if (distanceToPlayer <= 10 && distanceToPlayer > 5)
        //    {
        //        StartCoroutine(Attack());
        //    }
        //    else
        //    {
        //        anim.SetBool("fly", true);
        //    }
        //}
        //else
        //{
        //    attackTimer -= Time.deltaTime;
        //}
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineofSite);
        Gizmos.DrawWireSphere(transform.position, shotingRange);

    }
    //private IEnumerator Attack()
    //{
    //    isAttacking = true;
    //    anim.SetTrigger("attack");

    //    yield return new WaitForSeconds(0.1f);

    //    anim.ResetTrigger("attack");

    //    yield return new WaitForSeconds(attackCooldown - 0.1f);
    //    attackTimer = attackCooldown;
    //    isAttacking = false;
    //}
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
