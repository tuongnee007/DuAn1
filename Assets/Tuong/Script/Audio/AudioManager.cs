using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;

    private void Start()
    {
        audioSource.Stop();
    }
    public void StartVolume()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 1.0f;
        audioSource.Play();
    }
}
