using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthEnemy : MonoBehaviour
{
    public Slider sliderEnemy;
    public Color low;
    public Color high;
    public Vector3 offset;

    public void SetHealth(float health, float maxhealth)
    {
        sliderEnemy.gameObject.SetActive(health < maxhealth);
        sliderEnemy.value = health;
        sliderEnemy.maxValue = maxhealth;

        sliderEnemy.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low,high, sliderEnemy.normalizedValue);
    }
    private void Update()
    {
        if(sliderEnemy != null)
        {
            sliderEnemy.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);
        }
    }
}
