using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAreaCheck2 : MonoBehaviour
{
    private Enemy00 Enemy00;
    private void Awake()
    {
        Enemy00 = GetComponentInParent<Enemy00>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            Enemy00.tagert = collision.transform;
            Enemy00.inRange = true;
            Enemy00.hotZone.SetActive(true);
        }
    }
}
