using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public static gameManager Instance { get; private set; }
    private int enemyDeathCount = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void IncreaseEnemyDeathCount()
    {
        enemyDeathCount++;
        Debug.Log("Enemies killed: " + enemyDeathCount);
    }

    public int GetEnemyDeathCount()
    {
        return enemyDeathCount;
    }

    public TMP_Text enemyDeathCountText;
    private void Update()
    {
        if (enemyDeathCountText != null)
        {
            enemyDeathCountText.text = enemyDeathCount.ToString();
        }
    }
}


