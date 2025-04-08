using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyCling : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject[] wayPoints;
    public float damage;
    public int nextWayPoint = 0;
    float distToPoint;
    public float score = 2f;
    private void Update()
    {
        Move();
    }
    public void Move()
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

    public void OnTriggerEnter2D (Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            var a = collision.GetComponent<Health>();
            a.TakeDamage(damage);          
        }
    }
}
