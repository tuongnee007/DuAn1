using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderMovement : MonoBehaviour
{
    private float vertical;
    public float speed;
    private bool isLadder;
    [SerializeField] private Rigidbody2D rb;
    private bool isClimbing;
    private void Update()
    {
        vertical = Input.GetAxis("Vertical");
        if(isLadder && Mathf.Max(vertical) > 0f)
        {
            isClimbing = true;
        }
    }
    private void FixedUpdate()
    {
        if(isClimbing)
        {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, vertical * speed);
        }
        else
        {
            rb.gravityScale = 4f;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ladder")
        {
            isLadder = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ladder")
        {
            isLadder = false;
            isClimbing = false;
        }
    }
}
