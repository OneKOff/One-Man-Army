using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    
    private void Update()
    {
        transform.rotation = Camera.main.transform.rotation;
    }

    public void SetMaxValue(float value)
    {
        slider.maxValue = value;
        slider.value = value;
    }

    public void UpdateBar(int amount)
    {
        slider.value -= amount;
    }
}
