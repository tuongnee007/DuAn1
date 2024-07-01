using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPlayer : MonoBehaviour
{
    public float health;
    public float maxHealth;
    private SpriteRenderer spriteRenderer;
    public Color damageColor = Color.red;
    public float flashDuration = 0.3f;
    public int flashCount = 3;
    private void Start()
    {
        health = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(FlashDamage());
        }
    }

    public IEnumerator FlashDamage()
    {
        Color originalColor = spriteRenderer.color;
        for(int i = 0; i < flashCount; i++)
        {
            spriteRenderer.color = damageColor;
            yield return new WaitForSeconds(flashDuration);
            spriteRenderer.color = originalColor;
            yield return new WaitForSeconds(flashDuration);
        }
    }
}
