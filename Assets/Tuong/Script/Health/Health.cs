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
    public AudioSource takeDamage;
    // Nâng cấp
    public float healthUpdateCost = 75;
    public int upgradeLevel = 1;
    public int maxUpgradeLevel = 10;
    // UI
    public Slider upgradeSlider;
    public TMP_Text upgradeCostText;
    private void Awake()
    {
        takedamage.Stop();
        takeDamage.Stop();
    }
    private void Start()
    {
        health = maxHealth;
        UpdateHealthUI();
        AddHealth(health);
        LoadPlayerStats();
        UpdateUpgradeSlider();
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
            takeDamage.Play();
        }
    }
    void UpdateHealthUI()
    {
        float healthPercent = (health / maxHealth) *100f;
        healthSlider.value = healthPercent;
        healthText.text = $"Health: {healthPercent.ToString("0")}%";
    }
    public void UpgradeHealth()
    {
        if(upgradeLevel >= maxUpgradeLevel)
        {
            return;
        }
        float cost = GetUpgradeCost();
        PlayerAttack2 playerAttack2 = FindObjectOfType<PlayerAttack2>();
        if(playerAttack2 != null && playerAttack2.score >= cost)
        {
            float healthIncrease = GetMaxHealthIncrease();
            maxHealth += healthIncrease;
            health += healthIncrease;
            playerAttack2.AddScore(-cost);
            upgradeLevel++;
            SavePlayerStats();
            UpdateHealthUI();
            UpdateUpgradeSlider(); ;
        }
    }
    private void SavePlayerStats()
    {
        PlayerPrefs.SetFloat("MaxHealth", maxHealth); 
        PlayerPrefs.SetInt("UpgradeLevel", upgradeLevel);
        PlayerPrefs.Save();
    }
    private void LoadPlayerStats()
    {
        maxHealth = PlayerPrefs.GetFloat("MaxHealth", 100f); 
        upgradeLevel = PlayerPrefs.GetInt("UpgradeLevel", 1);
        health = maxHealth;
    }
    private void UpdateUpgradeSlider()
    {
        if (upgradeSlider != null)
        {
            upgradeSlider.maxValue = maxUpgradeLevel;
            upgradeSlider.value = upgradeLevel;
        }
        if(upgradeCostText != null)
        {
            upgradeCostText.text = $"Upgrade Cost: {GetUpgradeCost()}";
        }
    }
    private float GetUpgradeCost()
    {
        if (upgradeLevel >= 1 && upgradeLevel <= 3)
        {
            return healthUpdateCost * upgradeLevel;
        }
        else if (upgradeLevel == 4 || upgradeLevel == 5)
        {
            return healthUpdateCost * 3;
        }
        else if (upgradeLevel >= 6 && upgradeLevel <= 8)
        {
            return healthUpdateCost * 5;
        }
        else if (upgradeLevel == 9)
        {
            return healthUpdateCost * 7;
        }
        else if (upgradeLevel == 10)
        {
            return healthUpdateCost * 8;
        }
        return healthUpdateCost;
    }
    private float GetMaxHealthIncrease()
    {
        return 20f;
    }
}
