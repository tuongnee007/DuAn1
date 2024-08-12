using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AudioManager2 : MonoBehaviour
{

    public Slider volumeSlider; 
    public AudioSource[] audioSources; 

    private void Start()
    {
        if (audioSources.Length > 0)
        {
            volumeSlider.value = audioSources[0].volume;
        }
        volumeSlider.onValueChanged.AddListener(ChangeVolume);
    }

    public void ChangeVolume(float value)
    {
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.volume = value;
        }
    }
}


