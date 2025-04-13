using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void LoadGame(int level)
    {
        SceneManager.LoadScene(level);
    }
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}

