using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using TMPro;
using UnityEngine.UI;
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
    public void TestFlipDirection ()
    {
        var go = new GameObject("PlayerFlipTest");
        var player = go.AddComponent<Player>();


        player.transform.localScale = Vector3.one;
        player.Sim
    }
}
