using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static bool IsPaused { get; private set; } = false;
    public static void TooglePause()
    {
        IsPaused = !IsPaused;
        Time.timeScale = IsPaused ? 0 : 1;
    }
}
