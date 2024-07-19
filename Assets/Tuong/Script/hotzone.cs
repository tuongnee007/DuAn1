using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hotzone : MonoBehaviour
{
    private Enemy00 enemy00;
    private bool inRange;
    private Animator anim;

    private void Start()
    {
        enemy00 = GetComponentInParent<Enemy00>();
        anim = GetComponentInParent<Animator>();
    }
    private void Update()
    {
        if (inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            enemy00.Flip();
        } 
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = false;
            gameObject.SetActive(false);
            enemy00.triggerArea.SetActive(true);
            enemy00.inRange = false;
            enemy00.SelectTagert();
        }
    }
}
