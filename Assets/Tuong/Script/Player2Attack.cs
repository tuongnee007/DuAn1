using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Attack : MonoBehaviour
{
    public Animator anim;
    public int combo;
    public bool atacando;
    public AudioSource audio;
    public AudioClip[] sondio;
    private void Start()
    {
        audio = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    private void ComboAttack()
    {
        if(Input.GetKeyDown(KeyCode.F) && !atacando)
        {
            atacando = true;
            anim.SetTrigger("" + combo);
            audio.clip = sondio[combo];
            audio.Play();
        }
    }
    public void StartCombo()
    {
        atacando=false;
        if(combo < 3)
        {
            combo++;
        }
    }
    public void Finish()
    {
        atacando = false;
        combo = 0;
    }
}
