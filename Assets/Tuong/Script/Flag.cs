using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    private Animator anim;
    public AudioSource startAudio;
    private bool hasPlayedAudio = false;
    private void Start()
    {
        anim = GetComponent<Animator>();
        startAudio.Stop();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            anim.SetTrigger("start");
            anim.SetTrigger("end");
            if (!hasPlayedAudio)
            {
                startAudio.Play();
                hasPlayedAudio = true;
            }
        }
    }
}
