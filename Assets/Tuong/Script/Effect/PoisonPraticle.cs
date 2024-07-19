using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonPraticle : MonoBehaviour
{
    public float damage;
    public float damageInterval;
    private bool isDamaging = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(!isDamaging)
            {
                StartCoroutine(ApplyDamageRoutine(collision.gameObject));
            }
        }
    }
    private IEnumerator ApplyDamageRoutine(GameObject player)
    {
        isDamaging = true;
        while(true)
        {     
            if(player != null)
            {
                Health health = player.GetComponent<Health>();
                health.TakeDamage(damage);
            }
            yield return new WaitForSeconds(damageInterval);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            StopCoroutine(ApplyDamageRoutine(collision.gameObject));
            isDamaging = false;
        }
    }
}
