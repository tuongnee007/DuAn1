using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Transform firePosition;
    public GameObject projectile;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(projectile, firePosition.position, firePosition.rotation);
        }
    }
}
