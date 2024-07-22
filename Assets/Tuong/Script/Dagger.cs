using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger : MonoBehaviour
{
    public LayerMask WhatIsEnemy;
    public float damage;
    public AudioSource daggerAudio;
    private void Awake()
    {
        daggerAudio.Stop();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(1 < collision.gameObject.layer & WhatIsEnemy != 0)
        {
            EnemyHealth healthEnemy = collision.gameObject.GetComponent<EnemyHealth>();
            if(healthEnemy != null)
            {
                daggerAudio.Play();
                healthEnemy.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
     
    }
}
