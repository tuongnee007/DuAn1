using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlying : MonoBehaviour
{
    public float Speed;
    private GameObject player;
    public bool chase;
    public Transform startingPoint;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (player == null)
            return;
        if(chase == true)
        {
            Chase();
        }
        else
        {
            ReturnStartPoint();
        }
        Flip();
    }

    private void ReturnStartPoint()
    {
        transform.position = Vector2.MoveTowards(transform.position, startingPoint.position, Speed * Time.deltaTime);
    }
    private void Chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, Speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, player.transform.position) <= 5);
    }

    private void Flip()
    {
        if(transform.position.x > player.transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0,0,0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
