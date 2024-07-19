using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public Slider healthSlider;
    public TMP_Text healthText;
    public ParticleSystem takedamage;
    private void Awake()
    {
        takedamage.Stop();
    }
    private void Start()
    {
        health = maxHealth;
        UpdateHealthUI();
        AddHealth(health);
    }

    public void AddHealth(float health1)
    {
        if(health < maxHealth)
        {
            health += health1;
            if (health > maxHealth)
            {
                health = maxHealth;
            }
            UpdateHealthUI();
        }       
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            UpdateHealthUI();
            Destroy(gameObject);
        }
        else
        {
            UpdateHealthUI();
            takedamage.Play();
        }
    }
    void UpdateHealthUI()
    {
        float healthPercent = health / maxHealth;
        healthSlider.value = healthPercent;
        healthText.text = $"Health: {health} / {maxHealth}";
    }
    private void Takedame()
    {

    }
}
