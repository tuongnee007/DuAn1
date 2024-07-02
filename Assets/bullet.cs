using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class bullet : MonoBehaviour
{
    private GameObject target;
    private Animator animator;
    public float speed;
    public float damage;
    private Rigidbody2D bulletRb;
    public LayerMask WhatIsPlayers;
    public float attackRangeX;
    public float attackRangeY;
    public Transform attackPlayers;
    public float desTroyDistance = 0.1f;
    private bool hasDealDamage = false; 

    private void Start()
    {
        animator = GetComponent<Animator>();
        bulletRb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        Vector2 moveDir = (target.transform.position - transform.position).normalized * speed;
        bulletRb.velocity = new Vector2(moveDir.x, moveDir.y);
        Destroy(this.gameObject, 2);
    }

    private void Update()
    {
        if(hasDealDamage) 
            return;
        if(target != null)
        {
            float distance = Vector2.Distance(transform.position, target.transform.position);
            if (distance <= desTroyDistance)
            {
                DealDametoPlayer();
                hasDealDamage = true;
                Destroy(gameObject);
            }
        }
       
    }
    private void DealDametoPlayer()
    {
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(transform.position, Mathf.Max(attackRangeX, attackRangeY), WhatIsPlayers);

        foreach(Collider2D hitPlayer in hitPlayers)
        {
            Health health = hitPlayer.GetComponent<Health>();
            if(health != null)
            {
                health.TakeDamage(damage);
            }
            //HealthPlayer health = hitPlayer.GetComponent<HealthPlayer>();
            //if (health != null)
            //{
            //    health.TakeDamage(damage);
            //}
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPlayers.position, new Vector3(attackRangeX, attackRangeY, 1));
    }
}
