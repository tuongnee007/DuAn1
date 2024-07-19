using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwitch : MonoBehaviour
{
    public Player player1;
    public Player player2;
    public bool player1Active = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            SwitchPlayer();
        }
    }

    public void SwitchPlayer()
    {
        if (player1Active)
        {
            player1.enabled = false;
            player2.enabled = true;
            player1Active = false;
        }
        else
        {
            player1.enabled = true;
            player2.enabled = false;
            player1Active = true;
        }
    }
}
