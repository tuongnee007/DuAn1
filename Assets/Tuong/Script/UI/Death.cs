using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("Player"))
        {
            Health health = FindObjectOfType<Health>();

            if (health != null)
            {
                health.TakeDamage(10000);
            }
        }
    }
}
