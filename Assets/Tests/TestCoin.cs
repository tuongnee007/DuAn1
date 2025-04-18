﻿using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Experimental.GlobalIllumination;
using System.Net.WebSockets;
public class TestCoin
{
    [Test]
    public void TestScore()
    {
        var go = new GameObject("TestPlayer");
        var game = go.AddComponent<PlayerAttack2>();

        var textGO = new GameObject("ScoreText");
        var tmpText = textGO.AddComponent<TextMeshProUGUI>();
        game.scoreText = tmpText;

        game.AddScore(2);

        Assert.AreEqual(2, game.score);        
        Assert.AreEqual("2", tmpText.text);    
    }
    [Test]
    public void TestPlayerHealth()
    {
        var go = new GameObject("TestHealth");
        var player = go.AddComponent<Health>();

        player.maxHealth = 500;
        player.health = 500;

        //Mock Slider máu
        var sliderGO = new GameObject("HealthSlider");
        var slider = sliderGO.AddComponent<Slider>();
        player.healthSlider = slider;

        var textGo = new GameObject("HealthText");
        player.healthText = textGo.AddComponent<TextMeshProUGUI>();
        
        var panelGO = new GameObject("GameOverPanel");
        player.gameoverPanel = panelGO;

        player.takedamage = go.AddComponent<ParticleSystem>();

        player.takeDamage = go.AddComponent<AudioSource>();

        var updateTextGo = new GameObject("UpdateHealthText");
        player.updateHealthText = updateTextGo.AddComponent<TextMeshProUGUI>();

        player.TakeDamage(5);
        Assert.AreEqual(495, player.health);
    }

    [Test]
    public void TestAddHealth()
    {
        var go = new GameObject("TestAddHealth");
        var health = go.AddComponent<Health>();

        health.maxHealth = 600;
        health.health = 500;

        health.AddHealth(20);
        Assert.AreEqual(520, health.health);
    }
    [Test]
    public void TestResSpawn()
    {
        var go = new GameObject("ResSpawn");
        var resSpawn = go.AddComponent<Health>();

        resSpawn.maxHealth = 600;
        resSpawn.health = 500;
        resSpawn.transform.position = new Vector3(0,0, 0);  
        
        resSpawn.Respawn();

        Assert.AreEqual(300, resSpawn.health);
    }
    [Test]
    public void TestMovePlatform()
    {
        var go = new GameObject("PlatformMoving");
        var platform = go.AddComponent<MovingPlatform>();

        Transform Point1 = new GameObject("Point1").transform;
        Transform Point2 = new GameObject("Point2").transform;

        Point1.position = new Vector3(0, 0, 0);
        Point2.position = new Vector3(6, 0, 0);

        platform.points = new[] { Point1, Point2 };
        platform.startingPoint = 0;
        platform.speed = 3f;

        platform.Start();
        Assert.AreEqual(Point1.position, platform.transform.position);

        platform.Update();
        Assert.AreNotEqual(Point1.position, platform.transform.position);
    }
    [UnityTest]
    public IEnumerator TestAudioStart()
    {
        var gameObject = new GameObject();

        var audioSource = gameObject.AddComponent<AudioSource>();
        var audioManage = gameObject.AddComponent<AudioManager>();

        audioSource.playOnAwake = false;
        audioSource.Play();

        yield return null;

        Assert.IsFalse(audioSource.isPlaying, "Nguồn âm thanh phải được dừng");
    }
    [UnityTest]
    public IEnumerator TestInfoPanelStart()
    {
        var go = new GameObject("TestInfoPanel");
        var uiController = go.AddComponent<Portial>();

        var infoPanel = new GameObject("InfoPanel");
        infoPanel.SetActive(true);
        uiController.infoPanel = infoPanel;

        uiController.Start();
        yield return null;

        Assert.IsFalse(infoPanel.activeSelf, "InfoPanel phải được ẩn");
    }
    [Test]
    public void TestShowInfoPanel()
    {
        var triggerObj = new GameObject("TriggerObj");
        var trigger = triggerObj.AddComponent<Portial>();//Gắn Porital vào gameObject

        var triggerColider = triggerObj.AddComponent<BoxCollider2D>();// gắn boxcolliider2d vào game object
        triggerColider.isTrigger = true;//Bật istrigger cho gameobject để phát hiện va chạm

        var infoPanel = new GameObject("InfoPanel");
        infoPanel.SetActive(false);// Ẩn InfoPanel 
        trigger.infoPanel = infoPanel;//Gắn infoPanel cho gameObject

        var player = new GameObject("Player");
        player.tag = "Player";//Tạo Player và gắn tag Player
        var playerCollider = player.AddComponent<BoxCollider2D>();//Thêm boxCollider cho player

        trigger.OnTriggerEnter2D(playerCollider);//Gọi hàm TriggerEnter2d khi tham số truyền vào va chạm là playerCollider

        Assert.IsTrue(trigger.playerInTrigger, "Người chơi phải được đánh dấu là đang kích hoạt");
        Assert.IsTrue(infoPanel.activeSelf, "Bảng điều khiển sẽ hiển thị sau khi kích hoạt");
    }
    [Test]
    public void TestCloseInfoPanel()
    {
        var triggerObj = new GameObject("TriggerObj");
        var trigger = triggerObj.AddComponent<Portial>();//Gắn Porital vào gameObject

        var triggerColider = triggerObj.AddComponent<BoxCollider2D>();// gắn boxcolliider2d vào game object
        triggerColider.isTrigger = true;//Bật istrigger cho gameobject để phát hiện va chạm

        var infoPanel = new GameObject("InfoPanel");
        infoPanel.SetActive(true);// Ẩn InfoPanel 
        trigger.infoPanel = infoPanel;//Gắn infoPanel cho gameObject

        var player = new GameObject("Player");
        player.tag = "Player";//Tạo Player và gắn tag Player
        var playerCollider = player.AddComponent<BoxCollider2D>();//Thêm boxCollider cho player

        trigger.OnTriggerExit2D(playerCollider);//Gọi hàm TriggerEnter2d khi tham số truyền vào va chạm là playerCollider

        Assert.IsFalse(trigger.playerInTrigger, "Người chơi phải được đánh dấu là đang kích hoạt");
        Assert.IsFalse(infoPanel.activeSelf, "Bảng điều khiển sẽ hiển thị sau khi kích hoạt");
    }
    [Test]
    public void UpdateHealthText()
    {
        var update = new GameObject();
        var health = update.AddComponent<Health>();

        var text = new GameObject();
        var textUI = text.AddComponent<TextMeshProUGUI>();

        health.updateHealthText = textUI;
        health.maxHealth = 150;
        health.UpdateHealthText();

        Assert.AreEqual("Health: 150",textUI.text);
    }

    //Test hud

    [Test]// Ví trí Slider Player
    public void SliderPosition()
    {
        GameObject sliderGameObject = GameObject.Find("SliderHealthPlayer");
        Assert.IsNotNull(sliderGameObject, "Không tìm thấy Slider");

        RectTransform rt = sliderGameObject.GetComponent<RectTransform>();
        Assert.IsNotNull(rt, "Slider không có Rect transform");
        Assert.AreEqual(new Vector2(0.5f, 0.5f), rt.anchorMin);
        Assert.AreEqual(new Vector2(0.5f, 0.5f), rt.anchorMax);

        Vector2 expectedAnchoeddPos = new Vector2(-778f, 504f);
        Assert.AreEqual(expectedAnchoeddPos, rt.anchoredPosition, "Slider nằm không đúng vị trí");
    }   

    [Test]// Ví trí Image FlashPosition
    public void FlashPosition()
    {
        GameObject imageGameobject = GameObject.Find("Flash");
        Assert.IsNotNull(imageGameobject, "Không tìm thấy Image Flash");

        RectTransform rt = imageGameobject.GetComponent<RectTransform>();
        Assert.IsNotNull(rt, "Slider không có Rect transform");
        Assert.AreEqual(new Vector2(0.5f, 0.5f), rt.anchorMin);
        Assert.AreEqual(new Vector2(0.5f, 0.5f), rt.anchorMax);

        Vector2 expectedAnchoeddPos = new Vector2(-490f, 508f);
        Assert.AreEqual(expectedAnchoeddPos, rt.anchoredPosition, "Slider nằm không đúng vị trí");
    }

    [Test]// Hiển thị % máu player
    public void HealthPlayer()
    {
        var go = new GameObject("Health");
        var health = go.AddComponent<Health>();

        var textGo = new GameObject("HealthText");
        var textUI = textGo.AddComponent<TextMeshProUGUI>();
        
        health.healthText = textUI;

        health.health = 100;
        health.maxHealth = 100;

        health.UpdateHealthUI(); 

        Assert.AreEqual("Health: 100%", textUI.text, "Hiển thị % máu không đúng");
    }

    [Test] // Phát nhạc nền và lặp lại 
    public void PlaySound()
    {
        GameObject sound = GameObject.Find("Audio/SoundManager");
        Assert.IsNotNull(sound, "SoundManager không tồn tại trong Scene");

        AudioSource audioSource = sound.GetComponent<AudioSource>();

        Assert.AreEqual(true, audioSource.playOnAwake, "Nhạc nền không được khi bắt đầu game");
        Assert.AreEqual(true, audioSource.loop, "Nhạc nền không được phát lặp lại");
    }

    [Test] // Phát animation run  khi chạy
    public void PlayAnimationRun()
    {
        GameObject player = GameObject.Find("Player 1");
        Assert.IsNotNull(player, "Player không tồn tại trong Scene");
        Animator animator = player.GetComponent<Animator>();
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(-5f, 0);
        float speed = rb.velocity.magnitude;
        animator.SetBool("run", speed > 0.1f);
        bool isRunning = animator.GetBool("run");
        Assert.AreEqual(true, isRunning, "animation run không chạy khi nhân vật di chuyển");
    }

    [Test] //Mở panelTesting
    public void OpenPanelTesting()
    {
        var button = GameObject.Find("Canvas/Setting Button");
        Assert.IsNotNull(button, "Không có button trong canvas");

        Button button2 = button.GetComponent<Button>();
        Assert.IsNotNull(button2, "Không có Button trong Scene");

        var panel = GameObject.Find("Canvas/Setting Button/Setting Panel");
        Assert.IsNotNull(panel, "Không có Panel trong Scene"); ;

        button2.onClick.Invoke();
        Assert.IsTrue(panel.activeSelf,"Panel không được bật");
    }
}
