using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public float attackDamage;
    public float wait;
    public float wait2;
    public float riseSpeed;
    public float rotationSpeed;
    public int startPosition;
    private bool isMoving = true;
    private bool isDamage = true;
    public Transform[] point;
    private int currentIndex;
    public GameObject debrisEffect;
    public Transform location;
    private void Start()
    {
        transform.position= point[startPosition].position;
        currentIndex = startPosition;
    }
    private void Update()
    {
        if (isMoving)
        {
            MoveTrap();
        }
        Turn();
    }
    private void MoveTrap()
    {
        if (Vector2.Distance(transform.position, point[currentIndex].position) < 0.02)
        {
            StartCoroutine(WaitBeforeNextMove());
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, point[currentIndex].position, riseSpeed *Time.deltaTime);

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && isDamage)
        {
            Health health = collision.GetComponent<Health>();
            if(health != null)
            {
                health.TakeDamage(attackDamage);
                StartCoroutine(WaitToDealDamage());
            }
        }
        if(collision.gameObject.tag == "ground")
        {
            if (debrisEffect != null)
            {
                Transform locationTransform = location.transform;
                GameObject instantiateEfff = Instantiate(debrisEffect, locationTransform.position, Quaternion.identity);
                Destroy(instantiateEfff, 1f);
            }
        }
    }
    private IEnumerator WaitBeforeNextMove()
    {
        isMoving = false;
        yield return new WaitForSeconds(wait);
        currentIndex = (currentIndex + 1) % point.Length;
        isMoving = true;
    }
    private void Turn()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
    private IEnumerator WaitToDealDamage()
    {
        isDamage = false;
        yield return new WaitForSeconds(wait2);
        isDamage = true;
    }
}
