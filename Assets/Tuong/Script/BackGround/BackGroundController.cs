using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BackGroundController : MonoBehaviour
{
    private float startPos, Length;
    public GameObject cam;
    public float parallaxEffect;
    private void Start()
    {
        startPos = transform.position.x;
        Length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void FixedUpdate()
    {
        float distance = cam.transform.position.x * parallaxEffect;
        float movement = cam.transform.position.x * (1-parallaxEffect);

        if(movement > startPos + Length)
        {
            startPos += Length;
        }
        else if(movement < startPos - Length)
        {
            startPos -= Length;
        }
    }
}
