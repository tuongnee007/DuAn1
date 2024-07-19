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
    public float Health = 10f;
    public float MaxHealth = 10f;
    public int diem = 2;
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
    //UI
    public Slider healthBarSlider;
    public Vector3 healthBarOffset;
    //Audio
    public AudioSource dieAudio;
    private void Awake()
    {
        dieAudio.Stop();
        healthBarSlider.gameObject.SetActive(false);
    }
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        startingPoint = transform.position;
        Health = MaxHealth;
        UpateHeathBarPosition();
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
        UpateHeathBarPosition();
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

    public void TakeDamage(float damage)
    {
        if(Health > 0)
        {
            Health -= damage;
            healthBarSlider.gameObject.SetActive(true);
            StartCoroutine(WaitTime());
        }
        UpateHeathBarPosition();
        if(Health <= 0)
        {
            PlayerAttack playerAttack = FindObjectOfType<PlayerAttack>();
            if(playerAttack != null && scored)
            {
                playerAttack.AddScore(diem);
                anim.SetTrigger("die");
                dieAudio.Play();
                scored = false;
            }
            PlayerAttack2 playerAttack2 = FindObjectOfType<PlayerAttack2>();
            if(playerAttack2 != null && scored)
            {
                playerAttack2.AddScore(diem);
                anim.SetTrigger("die");
                dieAudio.Play();
                scored = false;
            }
            isDead = true;
            //GetComponent<LootBag>().InstantiateLoot(transform.position);
            Destroy(gameObject, 5);
        }
    }
    private IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(time);
        healthBarSlider.gameObject.SetActive(false);
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
    private void UpateHeathBarPosition()
    {
        if(healthBarSlider != null)
        {
            float heathEnemy = Health / MaxHealth;
            healthBarSlider.value = heathEnemy;
            healthBarSlider.transform.position = Camera.main.WorldToScreenPoint(transform.position + healthBarOffset);
        }
    }
}
