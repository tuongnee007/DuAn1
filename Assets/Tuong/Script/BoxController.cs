using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    public GameObject rewardPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Vector2 contactPoint = collision.transform.position;
            Vector2 boxTomt = (Vector2)transform.position - GetComponent<BoxCollider2D>().size / 2;
            if(contactPoint.y < boxTomt.y)
            {
                Instantiate(rewardPrefab, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}
