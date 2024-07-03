using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public float apple;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Health health = FindFirstObjectByType<Health>();
            health.AddHealth(apple);
            Destroy(gameObject);
        }      
    }
}
