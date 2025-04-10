using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyHealth : MonoBehaviour
{
    public Slider healthBarSlider;
    public Vector3 healthBarOffset;
    public float health;
    public float maxHealth;
    public float score;
    private bool scored = true;
    public float waitTime;
    public float time;
    private Animator anim;
    public AudioSource dieAudio;
    public ParticleSystem takeDamage;
    private bool isDead = false;
    private void Start()
    {
        healthBarSlider.gameObject.SetActive(false);
        health = maxHealth;
        UpateHeathBarPosition();    
        anim = GetComponent<Animator>();
        dieAudio.Stop();
        takeDamage.Stop();
    }
    private void Update()
    {
        UpateHeathBarPosition();
    }
    public void TakeDamage(float damage)
    {
        if (health > 0)
        {
            health -= damage;
            Takedamge();
            healthBarSlider.gameObject.SetActive(true);
            StartCoroutine(WaitTime());
        }
        UpateHeathBarPosition();
        if (health <= 0 && !isDead)
        {
            isDead = true;
            UpateHeathBarPosition();
            anim.SetTrigger("die");
            dieAudio.Play();
            gameManager.Instance.IncreaseEnemyDeathCount();
            PlayerAttack playerAttack = FindObjectOfType<PlayerAttack>();
            if (playerAttack != null && scored)
            {
                playerAttack.AddScore(score);
                scored = false;
            }
            PlayerAttack2 playerAttack2 = FindObjectOfType<PlayerAttack2>();
            if (playerAttack2 != null && scored)
            {
                playerAttack2.AddScore(score);
                scored = false;
            }
            Destroy(gameObject, waitTime);
        }
    }
    private IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(time);
        healthBarSlider.gameObject.SetActive(false);
    }
    private void UpateHeathBarPosition()
    {
        if (healthBarSlider != null)
        {
            float heathEnemy = health / maxHealth;
            healthBarSlider.value = heathEnemy;
            healthBarSlider.transform.position = Camera.main.WorldToScreenPoint(transform.position + healthBarOffset);
        }
    }
    private void Takedamge()
    {
        takeDamage.Play();
    }
}
