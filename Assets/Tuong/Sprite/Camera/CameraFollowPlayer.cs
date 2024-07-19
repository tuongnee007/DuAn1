using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraFollowPlayer : MonoBehaviour
{
    public Transform tagert;
    public float smothSpeed = 0.125f;
    public Vector3 offset;

    private void LateUpdate()
    {
        if(tagert != null)
        {
            Vector3 disirecposition = tagert.position + offset;
            Vector3 smothedPosition = Vector3.Lerp(transform.position, disirecposition, smothSpeed);
            transform.LookAt(tagert);
        }
    }
}
