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
    public float diem = 2f;
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
        HealthPlayer healthPlayer = FindObjectOfType<HealthPlayer>();
        if(collision.gameObject.tag == "Player")
        {
            healthPlayer.TakeDamage(damage);
        }
    }

    public void TakeDamage(float damage)
    {

        health -= damage;
        PlayerAttack playerAttack = FindObjectOfType<PlayerAttack>();
        if(health <= 0)
        {
            playerAttack.AddScore(diem);
            Destroy(gameObject);
        }
    }
}
