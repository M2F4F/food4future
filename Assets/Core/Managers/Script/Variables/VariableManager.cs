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
        // Read CSV-Data
        // Fill data into suitable data-structure
        // Use data to calculate Score
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
            temperatureText.text = temperatureLevel.ToString() + "°C";
        }
    }

    private void UpdateLightText()
    {
        if (lightText != null)
        {
            lightText.text = string.Format("{0:P1}", lightLevel);
        }
    }
}
