using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Trap_suriken : MonoBehaviour
{
    private float speed = 5f;
    public float speedmove = 5f;
    public Transform pointA;
    public Transform pointB;
    private Vector3 pointDf;
    void Start()
    {
        pointDf = pointA.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, pointDf, speedmove * Time.deltaTime);
        if(Vector3.Distance(transform.position, pointDf) < 0.1f)
        {
            if(transform.position==pointA.position)
            {
                pointDf = pointB.position;
            }
            else
            {
                pointDf = pointA.position;
            }
        }
    }
    public void FixedUpdate()
    {
        transform.Rotate(0, 0 , speed);
    }
    //Xử lý va chạm
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("Scene1");
        }
    }
}
