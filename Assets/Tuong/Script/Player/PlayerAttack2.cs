using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack2 : MonoBehaviour
{
    public Transform attackEnemy;
    public LayerMask whatIsEnemies;
    private Animator anim;
    public float attackRangeX;
    public float attackRangeY;
    public float baseDamage = 5f;
    public float criticalChance = 0.1f;
    public float criticalMultiplier = 2.0f;
    public float randomFactor = 0.1f;
    private bool isAttacking = false;

    // Điểm
    public TMP_Text scoreText;
    public float score { get; private set; } 
    // Nâng cấp
    public float damageUpdateCost = 75;
    public int upgradeLevel = 1;
    public int maxUpgradeLevel = 10;
    // UI
    public Slider upgradeSlider;
    public TMP_Text upgradeCostText;
    //Hiện sát thương
    public GameObject damagePopupPrefab;
    private void Start()
    {
        anim = GetComponent<Animator>();
        LoadScore();
        UpdateScore();
        LoadPlayerStats();
        UpdateUpgradeSlider();
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
            if (enemyHealth != null)
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
        score += point;
        UpdateScore();
        SaveScore();
    }

    private void UpdateScore()
    {
        scoreText.text = score.ToString();
    }

    private void SaveScore()
    {
        PlayerPrefs.SetFloat("PlayerScore", score);
        PlayerPrefs.Save();
    }

    private void LoadScore()
    {
        score = PlayerPrefs.GetFloat("PlayerScore", 0);
    }

    private float CalculateDamage()
    {
        float damage = baseDamage * Random.Range(1 - randomFactor, 1 + randomFactor);
        bool isCritical = Random.value < criticalChance;
        if (isCritical)
        {
            damage *= criticalMultiplier;
        }
        return damage;
    }

    public void UpdateDamage()
    {
        if (upgradeLevel >= maxUpgradeLevel)
        {
            return;
        }
        float cost = GetUpgradeCost();
        if (score >= cost)
        {
            baseDamage += GetBaseDamageIncrease();
            score -= cost;
            upgradeLevel++;
            UpdateScore();
            SaveScore();
            SavePlayerStats();
            UpdateUpgradeSlider();
        }
    }

    public void DamageReduction()
    {
        if (baseDamage > 0)
        {
            baseDamage -= GetBaseDamageIncrease();
            if(upgradeLevel > 1)
            {
                upgradeLevel--;
            }
            SavePlayerStats();
            UpdateUpgradeSlider();
        }
    }

    private void SavePlayerStats()
    {
        PlayerPrefs.SetFloat("PlayerDamage", baseDamage);
        PlayerPrefs.SetInt("UpgradeLevel", upgradeLevel);
    }

    private void LoadPlayerStats()
    {
        baseDamage = PlayerPrefs.GetFloat("PlayerDamage", 10f);
        upgradeLevel = PlayerPrefs.GetInt("UpgradeLevel", 1);
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
            return damageUpdateCost * upgradeLevel;
        }
        else if (upgradeLevel == 4 || upgradeLevel == 5)
        {
            return damageUpdateCost * 3;
        }
        else if (upgradeLevel >= 6 && upgradeLevel <= 8)
        {
            return damageUpdateCost * 5;
        }
        else if (upgradeLevel == 9)
        {
            return damageUpdateCost * 7;
        }
        else if (upgradeLevel == 10)
        {
            return damageUpdateCost * 8;
        }
        return damageUpdateCost;
    }

    private float GetBaseDamageIncrease()
    {
        return 5f;
    }
}
