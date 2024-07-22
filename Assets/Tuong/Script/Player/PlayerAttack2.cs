using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerAttack2 : MonoBehaviour
{
    public Transform attackEnemy;
    public LayerMask whatIsEnemies;
    private Animator anim;
    public float attackRangeX;
    public float attackRangeY;
    public float baseDamage;
    public float criticalChance = 0.1f;
    public float criticalMultiplier = 2.0f;
    public float randomFactor = 0.1f;
    private bool isAttacking = false;
    private Coroutine attackCoroutine;

    //Điểm
    public TMP_Text score;
    private float Score;


    private void Start()
    {
        anim = GetComponent<Animator>();
        Score = 0;
        UpdateScore();
    }
    private void Update()
    {
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            isAttacking = true;
            anim.SetTrigger("attack");
            if (audioManager != null)
            {
                audioManager.StartVolume();
            }
            StartCoroutine(EndAttack());
        }

        if (Input.GetKey(KeyCode.Q) && !isAttacking)
        {
            isAttacking = true;
            anim.SetTrigger("attack2");
            if (audioManager != null)
            {
                audioManager.StartVolume();
            }
            StartCoroutine(EndAttack());
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackEnemy.position, new Vector3(attackRangeX, attackRangeY, 1));
    }
    private void DealDamageToEnemies()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackEnemy.position, Mathf.Max(attackRangeX, attackRangeY), whatIsEnemies);
        foreach (Collider2D enemy in hitEnemies)
        {
            float damage = CalculateDamage();                                          
            EnemyFire enemyFire1 = enemy.GetComponent<EnemyFire>();
            if (enemyFire1 != null)
            {
                enemyFire1.TakeDamage(50);
            }

            EnemyFlying enemyFlying = enemy.GetComponent<EnemyFlying>();
            if (enemyFlying != null)
            {
                enemyFlying.TakeDamage(damage);
            }

            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            if(enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
                
            }
        }
    }

    private IEnumerator EndAttack()
    {
        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
    }

    public void AddScore(float point)
    {
        Score += point;
        UpdateScore();
    }
    private void UpdateScore()
    {
        score.text = Score.ToString();
    }
    private float CalculateDamage()
    {
        float damage = baseDamage * Random.Range(1 - randomFactor, 1 + randomFactor);
        bool isCritial = Random.value < criticalChance;
        if (isCritial)
        {
            damage *= criticalMultiplier;
        }
        return damage;
    }
}
