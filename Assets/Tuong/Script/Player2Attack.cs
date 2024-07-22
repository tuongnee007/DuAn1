using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Attack : MonoBehaviour
{
    public Animator anim;
    public int combo;
    public bool atacando;
    public AudioClip[] sondio;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void ComboAttack()
    {
        if(Input.GetKeyDown(KeyCode.F) && !atacando)
        {
            atacando = true;
            anim.SetTrigger("" + combo);
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
