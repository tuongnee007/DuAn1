using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Portial : MonoBehaviour
{
    public bool playerInTrigger = false;
    public GameObject infoPanel;
    public void Start()
    {
        infoPanel.gameObject.SetActive(false);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {           
            playerInTrigger = true;
            infoPanel.gameObject.SetActive(true);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerInTrigger = false;
            infoPanel.gameObject.SetActive(false);
        }
    }
    public void Update()
    {
        if (playerInTrigger && Input.GetKeyDown(KeyCode.F))
        {
            SceneManager.LoadScene(2);
        }
    }
}
