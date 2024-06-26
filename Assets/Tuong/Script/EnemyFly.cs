using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFly : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] Vector2 moveDirection = new Vector2(1f, 0.25f);
    [SerializeField] GameObject rightCheck, roofCheck, groundCheck;
    [SerializeField] Vector2 rightChecksize, roofCheckSize, groundCheckSize;
    [SerializeField] LayerMask groundLayer, platform;
    [SerializeField] bool goingUp = true;

    private bool touchedGround, touchedRoof, touchedRight;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
    }

    private void Update()
    {
        HitLogic();
    }

    private void FixedUpdate()
    {
        rb.velocity = moveDirection * moveSpeed;
    }

    void HitLogic()
    {
        touchedRight = HitDector(rightCheck, rightChecksize, (groundLayer | platform));
        touchedRoof = HitDector(roofCheck, roofCheckSize, (groundLayer | platform));
        touchedGround = HitDector(groundCheck, groundCheckSize, (groundLayer | platform));

        if (touchedRight)
        {
            Flip();
        }
        if(touchedRoof && goingUp)
        {
            ChangeYDirection();
        }
        if(touchedGround && goingUp)
        {
            ChangeYDirection();
        }
    }

    bool HitDector (GameObject gameObject, Vector2 size, LayerMask layer)
    {
        return Physics2D.OverlapBox(gameObject.transform.position, size, 0f, layer);
    }

    void ChangeYDirection()
    {
        moveDirection.y = -moveDirection.y;
        goingUp = !goingUp;
    }
    void Flip()
    {
        transform.Rotate(new Vector2(0, 180));
        moveDirection.x = -moveDirection.x;
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(groundCheck.transform.position, groundCheckSize);
        Gizmos.DrawWireCube(roofCheck.transform.position, roofCheckSize);
        Gizmos.DrawWireCube(rightCheck.transform.position, rightChecksize);

    }
}
