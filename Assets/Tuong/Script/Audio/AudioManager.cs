using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource attackAudio;

    private void Start()
    {
        attackAudio.Stop();
    }
    public void StartVolume()
    {
        attackAudio = GetComponent<AudioSource>();
        attackAudio.volume = 1.0f;
        attackAudio.Play();
    }
}
