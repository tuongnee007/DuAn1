using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingTrap : MonoBehaviour
{
    public float rotationSpeed;
    public float speed;
    public float damage;
    public float knockbackForce;
    public int startingPoint;
    public Transform[] points;

    private int i;
    private void Start()
    {
        transform.position = points[startingPoint].position;
    }
    private void Update()
    {
        RoteTrap();
        MoveTrap();
    }
    private void MoveTrap()
    {
        if (Vector2.Distance(transform.position, points[i].position) < 0.02f)
        {
            i++;
            if (i == points.Length)
            {
                i = 0;
            }
            FlipDirection();
        }
        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
    }
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
        if(collision.gameObject.tag == "Player")
        {
            Health health = FindObjectOfType<Health>();
            if(health != null)
            {
                health.TakeDamage(damage);
            }
        }

        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
        if(rb != null)
        {
            Vector2 knockbackDirection = (collision.transform.position - transform.forward).normalized;
            rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
        }
    }
    private void RoteTrap()
    {
        transform.Rotate(0,0, rotationSpeed * Time.deltaTime);
    }    
    private void FlipDirection()
    {
        rotationSpeed *= -1;
    }
}
