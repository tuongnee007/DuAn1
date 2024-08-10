using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingDagger : MonoBehaviour
{
    public GameObject dagertPrefab;
    public Transform throwPoint;
    public float throwForce;
    public AudioSource dartsAudio;
    private bool isFacingRight = true;
    private bool enoughTime = true;
    public float time;
    private void Awake()
    {
        dartsAudio.Stop();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && enoughTime)
        {
            ThrowDagger();
            dartsAudio.Play();
            StartCoroutine(EnoughTime());
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            isFacingRight = false;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            isFacingRight = true;
        }
    }
    private void ThrowDagger()
    {
        GameObject dagger = Instantiate(dagertPrefab, throwPoint.position, throwPoint.rotation);
        Rigidbody2D rb = dagger.GetComponent<Rigidbody2D>();
        Vector2 direction = isFacingRight ? throwPoint.right : -throwPoint.right;
        rb.AddForce(direction * throwForce, ForceMode2D.Impulse);
        dagger.transform.localScale = new Vector2(isFacingRight? 1:  -1, 1);
    }
    private IEnumerator EnoughTime()
    {
        enoughTime = false;
        yield return new WaitForSeconds(time);
        enoughTime = true;
    }
}
