using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPC : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TMP_Text dialogueText;
    public TMP_Text text2;
    public string[] dialogueLines;
    public int currentLineIndex = 0;
    public bool isPlayerInRange = false;

    public void Start()
    {
        dialoguePanel.SetActive(false); 
        text2.gameObject.SetActive(false);
    }

    public void Update()
    {
        if(isPlayerInRange && Input.GetKeyDown(KeyCode.F))
        {
            if (!dialoguePanel.activeSelf)
            {
                PauseManager.TooglePause();
                dialoguePanel.SetActive(true);
                ShowDiaLogue();
            }
            else
            {
                NextdialogueLine();
            }
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
            text2.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            text2.gameObject.SetActive(false);
            isPlayerInRange = false;
            dialoguePanel.SetActive(false);
            currentLineIndex = 0;
        }
    }
    public void ShowDiaLogue()
    {
        if (dialogueLines.Length > 0)
        {
            dialogueText.text = dialogueLines[currentLineIndex];
        }
    }
    public void NextdialogueLine()
    {
        currentLineIndex++;
        if(currentLineIndex < dialogueLines.Length)
        {
            dialogueText.text = dialogueLines[currentLineIndex];
        }
        else
        {
            PauseManager.TooglePause();
            dialoguePanel.SetActive(false);
            currentLineIndex = 0;
        }
    }
}
