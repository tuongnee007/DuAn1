using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoatatingTrapIDle : MonoBehaviour
{
    public float damage;
    public float knockbackForce;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (transform.position.y < collision.transform.position.y - 0.8f)
            {
                collision.transform.SetParent(transform);
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Health health = FindObjectOfType<Health>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }
        }

        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 knockbackDirection = (collision.transform.position - transform.forward).normalized;
            rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
        }
    }
}
