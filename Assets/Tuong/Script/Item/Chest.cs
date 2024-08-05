using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation.VisualScripting;

public class Chest : MonoBehaviour
{
    public Item[] possibleItems;
    public Transform spawnPoint;
    private Animator anim;
    private bool isOpend = false;
    private bool isPlayerInRange = false;
    public GameObject infoText;
    public GameObject player;
    public AudioSource audioChest;
    private void Start()
    {
        anim = GetComponentInParent<Animator>();
        infoText.gameObject.SetActive(false);
        audioChest.Stop();
    }
    public void OpenChest()
    {
        if(isOpend) return;
        isOpend = true;
        int itemCount = GetRandomItemCount();
        for(int i = 0; i < itemCount; i++)
        {
            int randomIndex = Random.Range(0, possibleItems.Length);
            Item randomItem = possibleItems[randomIndex];
            GameObject itemInstance = Instantiate(randomItem.itemPrefab, spawnPoint.position, Quaternion.identity);

            Rigidbody2D rb = itemInstance.GetComponent<Rigidbody2D>();
            if (rb == null)
            {
                rb = itemInstance.AddComponent<Rigidbody2D>();
            }
            float foceX = Random.Range(-2f, 2f);
            float foceY = Random.Range(2f, 5f);
            rb.AddForce(new Vector2(foceX, foceY), ForceMode2D.Impulse);
            StartCoroutine(MoveItemTowardPlayer(itemInstance));
        }      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            isPlayerInRange = true;
            infoText.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isPlayerInRange = false;
            infoText.gameObject.SetActive(false);
        }
    }
    private int GetRandomItemCount()
    {
        float rool = Random.Range(0f, 100f);
        if (rool <= 8f) return 6;
        if (rool <= 23f) return 5;
        if (rool <= 53f) return 4;
        if (rool <= 118f) return 3;
        if (rool <= 198f) return 2;
        return 1;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && !isOpend && isPlayerInRange)
        {
            audioChest.Play();
            anim.SetTrigger("Open");
            OpenChest();
        }
    }
    private IEnumerator MoveItemTowardPlayer(GameObject item)
    {
        while(item != null && Vector2.Distance(item.transform.position, player.transform.position) > 0.1f)
        {
            if(item != null)
            {
                item.transform.position = Vector2.MoveTowards(item.transform.position, player.transform.position, Time.deltaTime * 5);
                yield return null;
            }          
        }
        if(item != null)
        {
            item.SetActive(false);
        }
    }
}
