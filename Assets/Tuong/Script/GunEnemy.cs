using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class GunEnemy : MonoBehaviour
{
    public Transform shootingPoint;
    public GameObject buttlePrefab;

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Instantiate(buttlePrefab, shootingPoint.position, transform.rotation);
        }
    }
}
