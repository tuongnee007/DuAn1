using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth_test
{        
    [Test]
    public void TextTakeDamage()
    {
        var god = new GameObject();
        var player = god.AddComponent<Health>();   
        player.healthSlider = god.AddComponent<Slider>();
        player.healthText = god.AddComponent<TextMeshProUGUI>();
        player.gameoverPanel = god;
        player.takedamage = god.AddComponent<ParticleSystem>();
        player.takeDamage = god.AddComponent<AudioSource>();
        player.updateHealthText = god.AddComponent <TextMeshProUGUI>();
        player.health = 250;
        player.TakeDamage(1f);
        Assert.AreEqual(249, player.health);
    }
    [Test]  
    public void TextNPC()
    {
        var newobj = new GameObject();
        var npc = newobj.AddComponent<NPC>();
        npc.dialoguePanel = newobj;
        npc.dialogueText = newobj.AddComponent<TextMeshProUGUI>();
        npc.text2 = newobj.GetComponent<TextMeshProUGUI>();
        npc.dialogueLines = new string[] { "Hello, player!" };
        var player = newobj.AddComponent<BoxCollider2D>();   
        player.isTrigger = true;
        player.tag = "Player";
        npc.OnTriggerEnter2D(player);
        Assert.AreEqual(true,npc.isPlayerInRange);
    }
    [Test]
    public void EnemycoliisionPlayer()
    {
        var newobj = new GameObject();
        var enemy = newobj.AddComponent<EnemyCling>();        
        var player = newobj.AddComponent<Health>();
        player.healthSlider = newobj.AddComponent<Slider>();
        player.healthText = newobj.AddComponent<TextMeshProUGUI>();
        player.gameoverPanel = newobj;
        player.takedamage = newobj.AddComponent<ParticleSystem>();
        player.takeDamage = newobj.AddComponent<AudioSource>();
        var obj = new GameObject();
        player.updateHealthText = obj.AddComponent<TextMeshProUGUI>();
        player.health = 100;
        enemy.damage = 10;
        var colision = newobj.AddComponent<BoxCollider2D>();
        colision.isTrigger = true;
        colision.tag = "Player";
        enemy.OnTriggerEnter2D(colision);
        Assert.AreEqual(90,player.health);
    }
    [Test]
    public void EnemyMove()
    {
        var newobj = new GameObject();
        var enemy = newobj.AddComponent<EnemyCling>();
        var waypoint1 = new GameObject();
        waypoint1.transform.position = new Vector3(5,0,0);
        enemy.wayPoints = new GameObject[] {waypoint1 };
        enemy.moveSpeed = 1f;
        enemy.transform.position = Vector3.zero;
        enemy.Move();
        Assert.Greater(enemy.transform.position.x, 0f);
    }
    [Test] 
    public void EnemyNetWayPoint()
    {
        var newobj = new GameObject();
        var enemy = newobj.AddComponent<EnemyCling>();
        var waypoint1 = new GameObject();
        var waypoint2 = new GameObject();
        waypoint1.transform.position = Vector3.zero;
        waypoint2.transform.position = new Vector3(10,0,0);
        enemy.wayPoints = new GameObject[] {waypoint1,waypoint2 };
        enemy.transform.position = Vector3.zero;
        enemy.nextWayPoint = 0;
        enemy.Move();
        Assert.AreEqual(1,enemy.nextWayPoint);
    }
}