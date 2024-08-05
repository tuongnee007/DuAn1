using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
public class SettingPanel : MonoBehaviour
{
    public GameObject settingPanel;
    //public Slider volumeSlider;

    private void Start()
    {
        settingPanel.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            settingPanel.gameObject.SetActive(true);
        }
    }

    public void Replay()
    {
        SceneManager.LoadScene("Map 1");
    }

    public void Exit()
    {
        Application.Quit();
    }
   
}
