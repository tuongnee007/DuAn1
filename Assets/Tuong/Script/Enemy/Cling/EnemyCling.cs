using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyCling : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject[] wayPoints;
    public float damage;
    int nextWayPoint = 1;
    float distToPoint;
    public float health = 5;
    public float score = 2f;
    private void Update()
    {
        Move();
    }
    private void Move()
    {
        distToPoint = Vector2.Distance(transform.position, wayPoints[nextWayPoint].transform.position);

        transform.position = Vector2.MoveTowards(transform.position, wayPoints[nextWayPoint].transform.position, moveSpeed * Time.deltaTime);

        if(distToPoint <= 0.2)
        {
            TakeTurn();
        }
    }

    private void TakeTurn()
    {
        Vector3 currRot = transform.eulerAngles;
        currRot.z += wayPoints[nextWayPoint].transform.eulerAngles.z;
        transform.eulerAngles = currRot;
        ChooseNextWayPoint();
    }
    private void ChooseNextWayPoint()
    {
        nextWayPoint++;
        if(nextWayPoint == wayPoints.Length)
        {
            nextWayPoint = 0;
        }
    }

    private void OnTriggerEnter2D (Collider2D collision)
    {
        Health health = FindObjectOfType<Health>();
        if(collision.gameObject.tag == "Player")
        {
            health.TakeDamage(damage);
        }
    }
    public void TakeDamage(float damage)
    {     
        health -= damage;
        PlayerAttack playerAttack = FindObjectOfType<PlayerAttack>();
        if(health <= 0)
        {
            playerAttack.AddScore(score);
            Destroy(gameObject);
        }
        PlayerAttack2 playerAttack2 = FindObjectOfType<PlayerAttack2>();
        if(playerAttack2 != null)
        {
            playerAttack2.AddScore(score);
            Destroy(gameObject);
        }
    }
}
