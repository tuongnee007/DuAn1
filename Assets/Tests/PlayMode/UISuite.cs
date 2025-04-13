using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using NUnit.Framework;

public class UISuite : InputTestFixture
{
    Mouse mouse;
    public override void Setup()
    {
        base.Setup();
        SceneManager.LoadScene("Tuong/Scene/Menu");
        mouse = InputSystem.AddDevice<Mouse>();
    }
    [UnityTest]
    public IEnumerator TestGameStart()
    {
        var playerButton = GameObject.Find("PlayGame");

        if (playerButton == null)
        {
            Debug.LogError("Không tìm thấy nút Play button");
            yield break;  
        }

        var sceneGame = SceneManager.GetActiveScene().name;
        Assert.That(sceneGame, Is.EqualTo("Menu"));

        CliclUI(playerButton);

        yield return new WaitUntil(() => SceneManager.GetActiveScene().name != "Menu");

        sceneGame = SceneManager.GetActiveScene().name;
        Assert.That(sceneGame, Is.EqualTo("Map 1"));
    }
    public void CliclUI(GameObject uiElement)
    {
        var camera = Camera.main;
        if (camera == null)
        {
            Debug.LogError("Không tìm thấy Camera!");
            return;
        }

        var screenPos = camera.WorldToScreenPoint(uiElement.transform.position);
        Set(mouse.position, screenPos);
        Click(mouse.leftButton);
    }
    [UnityTest]
    public IEnumerator TestVolumeSetting()
    { 
        var volumeSlider = GameObject.Find("Volumehehe").GetComponent<Slider>();
        volumeSlider.value = 0.5f;
        Assert.AreEqual(0.5f,volumeSlider.value);
        yield return null;
    }
    //[UnityTest]
    //public IEnumerator TestGraphicsResolution()
    //{
    //    var resoulutionDropdown = GameObject.Find("ResolutionDropdown").GetComponent<Dropdown>();
    //    resoulutionDropdown.value = 2;

    //    Assert.AreEqual(2,resoulutionDropdown.value);
    //    yield return null;
    //}
}
