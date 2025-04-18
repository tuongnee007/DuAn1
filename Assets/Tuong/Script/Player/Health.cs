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

    //Điểm hồi sinh
    private Vector3 respawnPoint;
    public bool hasDieonce = false;
    private List<Vector3> respawnPoints = new List<Vector3>();
    //UI chết
    public GameObject gameoverPanel;
    public Animator anim;
    //Hack Health
    public TMP_Text updateHealthText;
    private void Awake()
    {
        //takedamage.Stop();
        //takeDamage.Stop();
        respawnPoint = transform.position;;
        anim = GetComponent<Animator>();
        //gameoverPanel.SetActive(false);
    }
    private void Start()
    {
        health = maxHealth;
        UpdateHealthUI();
        AddHealth(health);
        LoadPlayerStats();
        UpdateUpgradeSlider();
        UpdateHealthText();
    }
    public void AddHealth(float health1)
    {
        if (health < maxHealth)
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
        if (health <= 0)
        {
            if (!hasDieonce)
            {
                Respawn();
                hasDieonce = true;
            }
            else
            {
                anim.SetTrigger("die");
                gameoverPanel.SetActive(true);
                Destroy(gameObject);
            }
        }
        else
        {
            takedamage.Play();
            takeDamage.Play();
        }
        UpdateHealthUI();
    }
    public void UpdateHealthUI()
    {
        float healthPercent = (health / maxHealth);
        if(healthSlider != null) 
            healthSlider.value = healthPercent; 
        if(healthText != null)
            healthText.text = $"Health: {healthPercent * 100f:0}%"; 
    }
    public void UpgradeHealth()
    {
        if (upgradeLevel >= maxUpgradeLevel)
        {
            return;
        }
        float cost = GetUpgradeCost();
        PlayerAttack2 playerAttack2 = FindObjectOfType<PlayerAttack2>();
        if (playerAttack2 != null && playerAttack2.score >= cost)
        {
            float healthIncrease = GetMaxHealthIncrease();
            maxHealth += healthIncrease;
            health += healthIncrease;
            playerAttack2.AddScore(-cost);
            upgradeLevel++;
            SavePlayerStats();
            UpdateHealthUI();
            UpdateUpgradeSlider();
            HackUpdateHealth();
        }
    }

    public void HackUpdateHealth()
    {
        float cost = GetUpgradeCost();
        PlayerAttack2 playerAttack2 = FindObjectOfType<PlayerAttack2>();
        if (playerAttack2 != null && playerAttack2.score >= cost)
        {
            float healthIncrease = GetMaxHealthIncrease();
            maxHealth += healthIncrease;
            health += healthIncrease;
            playerAttack2.AddScore(-cost);
            upgradeLevel++;
            UpdateHealthUI();
            UpdateUpgradeSlider();
            UpdateHealthText();
        }
    }

    public void healthDelete()
    {
        if (maxHealth > 0)
        {
            maxHealth -= GetMaxHealthIncrease();
            if (upgradeLevel >= 2)
            {
                upgradeLevel--;
            }
            
            SavePlayerStats();
            UpdateUpgradeSlider();
        }
    }

    public void NeftHealth()
    {
        if (maxHealth > 0)
        {
            maxHealth -= GetMaxHealthIncrease();
            if (upgradeLevel >= 2)
            {
                upgradeLevel--;
            }

            UpdateUpgradeSlider();
            UpdateHealthText();
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
        if (upgradeCostText != null)
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
    public void Respawn()
    {
        Vector3 nearestRespawnPoint = FindNearestRespawnPoint();
        transform.position = nearestRespawnPoint;
        health = maxHealth / 2;
        UpdateHealthUI();
    }
    private Vector3 FindNearestRespawnPoint()
    {
        Vector3 nearestPoint = respawnPoint;
        float minDistance = Vector3.Distance(transform.position, respawnPoint);
        foreach (var point in respawnPoints)
        {
            float distance = Vector3.Distance(transform.position, point);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestPoint = point;
            }
        }
        return nearestPoint;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("RespawnPoint"))
        {
            Vector3 newRespawnPoint = collision.transform.position;
            respawnPoints.Add(newRespawnPoint);
        }
    }

    public void UpdateHealthText()
    {
        updateHealthText.text = $"Health: {maxHealth}";
    }
}
