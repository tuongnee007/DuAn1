using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateManager : MonoBehaviour
{
    public GameObject player;
    public GameObject updatePanel;
    private void Start()
    {
        updatePanel.SetActive(false);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            updatePanel.SetActive(!updatePanel.activeSelf);
        }
    }
    public void UpdateDamage()
    {
        PlayerAttack2 attack = player.GetComponent<PlayerAttack2>();
        attack.UpdateDamage();
    }
    public void damageReduction()
    {
        PlayerAttack2 attack = player.GetComponent<PlayerAttack2>();
        attack.DamageReduction();
    }

    public void UpdateHealth()
    {
        Health health = player.GetComponent<Health>();
        health.UpgradeHealth();
    }
}
