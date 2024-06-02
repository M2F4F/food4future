using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VariableManager : MonoBehaviour
{
    public float temperatureLevel;
    public float lightLevel;

    public TMP_Text temperatureText;
    public TMP_Text lightText;

    private void Start()
    {
        UpdateTemperatureText();
        UpdateLightText();
    }

    public void SetTemperature(float value)
    {
        temperatureLevel = value;
        UpdateTemperatureText();
    }

    public void SetLightLevel(float value)
    {
        lightLevel = value;
        UpdateLightText();
    }

    private void UpdateTemperatureText()
    {
        if (temperatureText != null)
        {
            temperatureText.text = "Temperature: " + temperatureLevel.ToString("F2");
        }
    }

    private void UpdateLightText()
    {
        if (lightText != null)
        {
            lightText.text = "Light Level: " + lightLevel.ToString("F2");
        }
    }
}
