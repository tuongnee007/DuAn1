using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using TMPro;

public class H_TestPlayMode
{
    private Health health;
    [SetUp]
    public void SetUp()
    {
        var newobj = new GameObject();
        health = newobj.AddComponent<Health>();

        health.healthSlider = new GameObject("Slider").AddComponent<Slider>();
        health.healthText = new GameObject("Text").AddComponent<TextMeshProUGUI>();       
        health.anim = new GameObject().AddComponent<Animator>();
        health.upgradeSlider = new GameObject("UpgradeSlider").AddComponent<Slider>();
        
        health.upgradeCostText = new GameObject("UpgradeText").AddComponent<TextMeshProUGUI>();
        health.updateHealthText = new GameObject("UpdateHealthText").AddComponent<TextMeshProUGUI>();
        health.gameoverPanel = new GameObject("GameOverPanel");        
        health.takedamage = new GameObject("Particles").AddComponent<ParticleSystem>();
        health.takeDamage = new GameObject("AudioSource").AddComponent<AudioSource>();

        health.maxHealth = 100f;
        health.health = 100f;
        health.healthUpdateCost = 75;
        health.upgradeLevel = 1;
        health.maxUpgradeLevel = 10;        
    }

    [UnityTest]
    public IEnumerator TakeDamage_ReduceHealthAndUpdateUI()
    {
        health.health = 100f;
        health.maxHealth = 100f;
        health.TakeDamage(20f);
        yield return null;
        Assert.Less(health.health,100);
        Assert.AreEqual("Health: 80%", health.healthText.text);
    }
    [UnityTest]
    public IEnumerator DoubleDeath()
    {
        health.hasDieonce = false;
        health.gameoverPanel.SetActive(false );
        health.health = 10;
        health.maxHealth = 10;
        health.TakeDamage(20);
        yield return null;
        Assert.IsFalse(health.gameoverPanel.activeSelf);
        health.TakeDamage(100);
        yield return null;
        Assert.IsTrue(health.gameoverPanel.activeSelf);
    }
    [UnityTest]
    public IEnumerator Health_Slider()
    {
        health.healthSlider.value = 1f;
        health.health = 100;
        health.maxHealth = 100;
        yield return null;  
        Assert.AreEqual(1f, health.healthSlider.value);
        health.TakeDamage(50);
        yield return null;
        Assert.AreEqual(0.5f, health.healthSlider.value);
    }

    [UnityTest]
    [Category("PC")]
    public IEnumerator TakeDamage_PC()
    {
        health.hasDieonce = false;
        health.health = 10;
        health.gameoverPanel.SetActive(false);
        health.TakeDamage(20);
        yield return null;
        Assert.AreEqual(health.maxHealth / 2, health.health);
    }
    [UnityTest]
    [Category("Mobile")]
    public IEnumerator TakeDamage_Mobile()
    {
        health.hasDieonce = false;
        health.health = 10;
        health.gameoverPanel.SetActive(false);
        health.TakeDamage(20);
        yield return null;
        Assert.AreEqual(health.maxHealth / 2, health.health);
    }

    [UnityTest]
    [Category("PC")]
    public IEnumerator GameOver_PC()
    {
        health.hasDieonce = true;
        health.health = 10;
        health.gameoverPanel.SetActive(false);
        health.TakeDamage(20); 
        yield return null;

        Assert.IsTrue(health.gameoverPanel.activeSelf);
    }
    [UnityTest]
    [Category("Mobile")]
    public IEnumerator GameOver_Mobile()
    {
        health.hasDieonce = true;
        health.health = 10;
        health.gameoverPanel.SetActive(false);
        health.TakeDamage(20); 
        yield return null;
        Assert.IsTrue(health.gameoverPanel.activeSelf);
    }

}
