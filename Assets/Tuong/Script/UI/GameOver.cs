using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void RePlay(int level)
    {
        SceneManager.LoadScene(level);
    }
    public void Home(int level2)
    {
        SceneManager.LoadScene(level2);
    }
}
