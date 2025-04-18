using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerTwo : MonoBehaviour
{
    public int Score = 0;
    public int Lives = 3;
    public UIManager uiManager;
    public void CollectItem(int points)
    {
        Score += points;
        if(Score > 100)
        {
            Score = 100;
        }
        UpdateUI();
    }
    public void PlayerDided()
    {
        Lives--;
        UpdateUI();
    }  
    public void PlayerAddDided()
    {
        Lives++;
        UpdateUI();
    }
    public void UpdateUI()
    {
        uiManager.UpdateScore(Score);
        uiManager.UpdateLives(Lives);
    }
    public string CheckGameState()
    {
        if (Score >= 100)
        {
            return "Win";
        }
        else
        {
            return "Lose";
        }
    }
    public void SubtractPoints(int points)
    {
        Score -= points;
    }
}
