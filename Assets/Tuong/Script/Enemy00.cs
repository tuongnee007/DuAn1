using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy00 : MonoBehaviour
{
    #region Public Variable;
    public float attackDistance;
    public float moveSpeed;
    public float timer;
    public Transform leftLimit;
    public Transform rightLimit;
    [HideInInspector]public Transform tagert;
    [HideInInspector] public bool inRange;
    public GameObject hotZone;
    public GameObject triggerArea;
    #endregion
    #region Private Variables;
    private Animator anim;
    private bool attackMode;
    private float distance;
    private bool cooling;
    private float intTimer;
    #endregion

    private void Awake()
    {
        SelectTagert();
        intTimer = timer;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!attackMode)
        {
            Move();
        }
        if(!InsideofLimits() && !inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            SelectTagert();
        }
        if(inRange)
        {
            EnemyLogic();
        }
    }
    private void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, tagert.position);
        if (distance> attackDistance)
        {
            StopAttack();
        }
        else if(attackDistance >= distance && cooling == false)
        {
            Attack();
        }
        if (cooling)
        {
            Cooldown();
            anim.SetBool("attack", false);
        }
    }
    private void Move()
    {
        anim.SetBool("canWalk", true);
        if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            Vector2 tagertPostion = new Vector2(tagert.position.x, transform.position.y);

            transform.position = Vector2.MoveTowards(transform.position, tagertPostion, moveSpeed * Time.deltaTime);
        }
    }

    private void Attack()
    {
        timer = intTimer;
        attackMode = true;
        anim.SetBool("canWalk", false);
        anim.SetBool("attack", true);
    }

    private void Cooldown()
    {
        timer -= Time.deltaTime;
        if(timer <= 0&& cooling && attackMode)
        {
            cooling = false;
            timer = intTimer;
        } 
    }
    private void StopAttack()
    {
        cooling = false;
        attackMode= false;
        anim.SetBool("attack", false);
    }
    public void TriggerColing()
    {
        cooling = true;
    }

    private bool InsideofLimits()
    {
        return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x;
    }
    public void SelectTagert()
    {
        float distanceToLeft = Vector3.Distance(transform.position, leftLimit.position);
        float distanceToRight = Vector3.Distance(transform.position, rightLimit.position);
        if(distanceToLeft > distanceToRight)
        {
            tagert = leftLimit;
        }
        else
        {
            tagert = rightLimit;
        }
        Flip();
    }
    public void Flip()
    {
        Vector3 rotation = transform.eulerAngles;
        if(transform.position.x > tagert.position.x)
        {
            rotation.y = 180;
        }
        else
        {
            rotation.y = 0;
        }
        transform.eulerAngles = rotation;
    }
}
