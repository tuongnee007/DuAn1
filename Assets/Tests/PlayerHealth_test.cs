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
        player.updateHealthText = god.AddComponent<TextMeshProUGUI>();
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
    [Test]
    public void EnemyChassePlayer()
    {
        var newobj = new GameObject();
        newobj.AddComponent<Rigidbody2D>();
        newobj.AddComponent<Animator>();        
        var enemy = newobj.AddComponent<EnemyFlying>();
        var player = new GameObject();
        player.tag = "Player";
        enemy.Speed = 5f;
        enemy.boxX = 10;
        enemy.boxY = 10;
        enemy.transform.position= Vector2.zero;
        player.transform.position = Vector2.right * 5f;
        enemy.Start();
        enemy.Update();
        Assert.AreNotEqual(enemy.transform.position, Vector2.zero);
    }
    [Test]
    public void EnemyReturnToStart()
    {
        var newobj = new GameObject();
        newobj.AddComponent<Animator>();
        var enemy = newobj.AddComponent<EnemyFlying>();
        var player = new GameObject();
        player.tag = "Player";
        enemy.Speed = 5f;
        enemy.boxX = 2f;
        enemy.boxY = 2f;
        enemy.transform.position = Vector2.zero;
        player.transform.position = Vector2.right * 10f;
        Assert.AreEqual(enemy.transform.position, Vector3.MoveTowards(Vector2.zero, Vector2.zero, enemy.Speed * Time.deltaTime));
    }
    [Test]
    public void EnemyAttack()
    {
        var newobj = new GameObject();
        newobj.AddComponent<Animator>();
        var enemy = newobj.AddComponent<EnemyFlying>();
        var player = new GameObject();
        player.tag = "Player";
        enemy.Speed = 5f;
        enemy.boxX = 10f;
        enemy.boxY = 10f;                
        newobj.transform.position = Vector2.zero;
        player.transform.position = Vector2.right * 1.5f;

        enemy.Start();
        enemy.Update();

        Assert.IsTrue(enemy.isAttacking);
    }
    [Test]
    public void EnemyRotationPlayer()
    {
        var newobj = new GameObject();
        newobj.AddComponent<Animator>();
        var enemy = newobj.AddComponent<EnemyFlying>();
        var Player = new GameObject();
        Player.tag = "Player";

        enemy.transform.position = new Vector3(5, 0, 0);
        Player.transform.position = new Vector3(0, 0, 0);

        enemy.Start();
        enemy.Update();

        Assert.AreEqual(Quaternion.Euler(0, 0, 0), enemy.transform.rotation);
    }
    [Test]  
    public void EnemyDead()
    {
        var newobj = new GameObject();
        newobj.AddComponent<Animator>();
        var enemy = newobj.AddComponent<EnemyFlying>();
        var player = new GameObject();
        player.tag = "Player";

        enemy.isDead = true;
        enemy.Start();
        enemy.Update();        
        Assert.IsTrue(enemy.isDead);
    }
}