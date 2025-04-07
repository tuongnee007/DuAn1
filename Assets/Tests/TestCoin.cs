using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Experimental.GlobalIllumination;
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
        platform.speed = 20f;

        platform.Start();
        Assert.AreEqual(Point1.position, platform.transform.position);

        platform.Update();
        Assert.AreNotEqual(Point1.position, platform.transform.position);

        Assert.Less(Vector3.Distance(platform.transform.position, Point2.position), 5f);
    }

}
