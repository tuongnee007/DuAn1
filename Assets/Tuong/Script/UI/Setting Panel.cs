using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
public class SettingPanel : MonoBehaviour
{
    public GameObject settingPanel;
    public GameObject settingAudio;

    private void Start()
    {
        settingPanel.gameObject.SetActive(false);
        settingAudio.SetActive(false);
    }

    public void OpenSetting()
    {
        settingPanel.gameObject.SetActive(true);
    }
    public void CloseSetting()
    {
        settingPanel.gameObject.SetActive(false);
    }

    public void Replay(int level)
    {
        SceneManager.LoadScene(level);
    }

    public void Home(int level)
    {
        SceneManager.LoadScene(level);
    }
    public void Setting()
    {
        settingAudio.SetActive(true);
    }
    public void OpenSettingSlider()
    {
        settingAudio.SetActive(true);
    }

    public void CloseSettingSlider()
    {
        settingAudio.SetActive(false);
    }
    public void Exit()
    {
        Application.Quit();
    }
   
}
