using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    [Header("Login")]
    public TMP_InputField namePlayer;
    public TMP_InputField passwordPlayer;

    [Header("Regiest")]
    public TMP_InputField enterPlayerName;
    public TMP_InputField enterPassword;
    [Header("Panel")]
    public GameObject LoginPanel;
    public GameObject RegiestPanel;

    public TMP_Text MessageErrorText;
    //public TMP_Text name;
    //public TMP_Text password;

    private void Start()
    {
        RegiestPanel.SetActive(false);
    }
    public void OpenRegiestPanel()
    {
        LoginPanel.SetActive(false);
        RegiestPanel.SetActive(true);
    }
    public void BackToLogin()
    {
        RegiestPanel.SetActive(false); 
        LoginPanel.SetActive(true);
    }
    //Đăng nhập
    public void LoginGame()
    {
        var request = new LoginWithPlayFabRequest
        {
            Username = namePlayer.text,
            Password = passwordPlayer.text,

        };
        PlayFabClientAPI.LoginWithPlayFab(request, OnLoginGameSucces, OnErrorLogin);
    }

    private void OnLoginGameSucces(LoginResult result)
    {
        Debug.Log("Đăng nhập thành công");
        SceneManager.LoadScene(1);
    }

    private void OnErrorLogin(PlayFabError error)
    {
        Debug.Log("Đăng nhập thất bại: " +error.ErrorMessage);
    }
    //Tạo người dùng mới 
    public void newRegistrant()
    {
        var request = new RegisterPlayFabUserRequest
        {
            Username = enterPlayerName.text,
            Password = enterPassword.text,

            RequireBothUsernameAndEmail = false

        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnregiestSucces, OnError);
    }

    private void OnregiestSucces(RegisterPlayFabUserResult result)
    {
        Debug.Log("Đăng kí người dùng mới thành công!");
        UpdatePlayerName(enterPlayerName.text);
        LoginPanel.SetActive(true);
        RegiestPanel.SetActive(false);
    }
    private void OnError(PlayFabError error)
    {
        Debug.Log("Đăng kí thất bại: " + error.ErrorMessage);
    }

    //Gửi tên người dùng lên hệ thống playerFab
    private void UpdatePlayerName(string name)
    {
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = name,
        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, UpdateNameSucces, ErrorUpdateName);
    }

    private void UpdateNameSucces(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log("Cập nhật tên người dùng thành công!");
    }

    private void ErrorUpdateName(PlayFabError error)
    {
        Debug.Log("Cập nhật tên người dùng thất bại: " +error.ErrorMessage);
    }
}
