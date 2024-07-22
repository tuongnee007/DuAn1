using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    public TMP_Text textMesh;
    public float destroyTime;
    public float moveSpeed;

    private void Start()
    {
        Destroy(gameObject, destroyTime);
    }
    private void Update()
    {
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
    }
    public void SetDamageText(float damage)
    {
        textMesh.text = damage.ToString();
    }
}
