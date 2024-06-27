using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    public int health = 100;
    public float speed;
    private float dazedTime;
    public float startDazedTime;

    private Animator anim;
    public GameObject bloodEffect;

    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("walk", true);
    }

    private void Update()
    {
        if(dazedTime <= 0)
        {
            speed = 5f;
        }
        else
        {
            speed = 0f;
            dazedTime -= Time.deltaTime;
        }     
        transform.Translate(Vector2.left * speed * Time.deltaTime); 
    }

    public void TakeDamage(int damage)
    {
        Instantiate(bloodEffect, transform.position, Quaternion.identity);
        health -= damage;
        if (health <= 0)
        {
            anim.SetTrigger("death");
            Destroy(gameObject, 2f);
        }
    }  
}
