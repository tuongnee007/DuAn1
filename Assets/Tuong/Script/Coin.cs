using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float coin;
    public AudioSource coinAudio;
    private void Start()
    {
        coinAudio.Stop();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerAttack2 playerAttack2 = FindObjectOfType<PlayerAttack2>();
            if(playerAttack2 != null)
            {
                playerAttack2.AddScore(coin);
                coinAudio.Play();
                Destroy(gameObject, 0.5f);
            }
        }
    }
}
