using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FireflyFollowPlayer : MonoBehaviour
{
    public Light2D fireflyLight;
    private void Update()
    {
        if (fireflyLight != null)
        {
            fireflyLight.transform.position = transform.position;
            fireflyLight.intensity = 1f + Mathf.PingPong(Time.time, 0.5f);
        }
    }
}
