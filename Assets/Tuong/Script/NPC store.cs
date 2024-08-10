using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCstore : MonoBehaviour
{
    private bool right = false;
    private Animator anim;
    public GameObject storePanel;
    private bool playerInRange = false;
    public GameObject player;
    private void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(Waitime());
        storePanel.SetActive(false);
    }

    private IEnumerator Waitime()
    {
        while(true)
        {
            right = true;
            anim.SetBool("left", right);
            yield return new WaitForSeconds(5);
            right = false;
            anim.SetBool("left", right);
            yield return new WaitForSeconds(5);
        }    
    }
    private void Update()
    {
        if(playerInRange && Input.GetKeyDown(KeyCode.J))
        {
            storePanel.SetActive(!storePanel.activeSelf);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
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
}
