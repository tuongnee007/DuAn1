using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float explosionTime;
    public float bombDamage;
    public float attackRange;
    public LayerMask WhatIsPlayers;
    private GameObject tagert;
    public float destroyDistance;
    private bool isExploding = false;
    //Effect
    public Color blinkColor = Color.red;
    private Color  originalColor;
    private SpriteRenderer spriteRenderer;
    private Animator anim;
    //Push Force
    public float pushForce;
    //Audio 
    public AudioSource clockAudio;
    public AudioSource bombAudio;
    private void Start()
    {
        tagert = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        clockAudio.Stop();
        bombAudio.Stop();
    }
    private void Update()
    {
        if (tagert != null && !isExploding)
        {
            float distance = Vector2.Distance(transform.position, tagert.transform.position);
            if (distance <= destroyDistance)
            {
                StartCoroutine(ExpolisonCountdown());
                isExploding = true;
                StartCoroutine(BlinkEffect());
                clockAudio.Play(); 
                StartCoroutine(WaitAudioBomb());
                Destroy(gameObject, explosionTime +0.7f);
            }
        }     
    }
    private void DealDametoPlayer()
    {
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(transform.position, attackRange, WhatIsPlayers);

        foreach (Collider2D hitPlayer in hitPlayers)
        {
            Health health = hitPlayer.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(bombDamage);              
                PushPlayer(hitPlayer);
            }
            EnemySlime enemySlime = hitPlayer.GetComponent<EnemySlime>();
            if(enemySlime != null)
            {
                enemySlime.TakeDamage(bombDamage);
                PushPlayer(hitPlayer);
            } 
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    private IEnumerator ExpolisonCountdown()
    {
        yield return new WaitForSeconds(explosionTime);
        DealDametoPlayer();
    }
    private IEnumerator BlinkEffect()
    {
        while (isExploding)
        {
            spriteRenderer.color = blinkColor;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = originalColor;
            yield return new WaitForSeconds(0.1f);
        }
    }
    private IEnumerator WaitAudioBomb()
    {
        yield return new WaitForSeconds(1.5f);
        anim.SetBool("bomb", true);
        bombAudio.Play();
    }
    private void PushPlayer(Collider2D hitPlayer)
    {
        Rigidbody2D rb = hitPlayer.GetComponent<Rigidbody2D>();
        if(rb != null)
        {
            Vector2 pushDirection = (hitPlayer.transform.position - transform.position).normalized;
            rb.AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
        }
    }
}
