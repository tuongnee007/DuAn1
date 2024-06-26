using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject SettingPanel;

    private void Start()
    {
        SettingPanel.SetActive(false);
    }

    public void OpenSetting()
    {
        SettingPanel.SetActive(true);
    }

    public void CloseSetting()
    {
        SettingPanel.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void ChangeScene(int ID)
    {
        SceneManager.LoadScene(ID);
    } 
}
