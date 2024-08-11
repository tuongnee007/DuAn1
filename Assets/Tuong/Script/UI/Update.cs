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
    public void healthReduction()
    {
        Health health = player.GetComponent<Health>();
        health.healthDelete();
    }

    public void UpdateSpeed()
    {
        Player player2 = player.GetComponent<Player>(); 
        player2.UpgradeSpeed();
    }
    public void BloodSpeed()
    {
        Player player2 = player.GetComponent<Player>();
        player2.hypotension();
    }

    public void HackUpdateDamage()
    {

        PlayerAttack2 attack = player.GetComponent<PlayerAttack2>();
        attack.HackUpdateDamage();
    }

    public void NEftDamage()
    {

        PlayerAttack2 attack = player.GetComponent<PlayerAttack2>();
        attack.NeftDamage();
    }
    public void HackUpdateSpeed()
    {

        Player attack = player.GetComponent<Player>();
        attack.HackUpdateSpeed();
    }
    public void NeftSpeed()
    {

        Player attack = player.GetComponent<Player>();
        attack.BloodSpeed();
    }
    public void HackUpdateHealth()
    {
        Health health = player.GetComponent<Health>();
        health.HackUpdateHealth();
    }
    public void NeftHealth()
    {
        Health health = player.GetComponent<Health>();
        health.NeftHealth();
    }
}
