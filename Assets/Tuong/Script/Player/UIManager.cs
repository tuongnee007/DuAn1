using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text scoreText;
    public Text livesText;
    public void UpdateScore(int socre)
    {
        scoreText.text = "Score: " + socre;
    } 
    public void UpdateLives(int lives)
    {
        livesText.text = "Lives: " + lives;
    }
}
