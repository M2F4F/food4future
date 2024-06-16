using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;

public class VariableManager : MonoBehaviour
{
    public int temperatureLevel;
    public Slider temperatureSlider;
    public float lightLevel;
    public Slider lightLevelSlider;
    public TMP_Text temperatureText;
    public TMP_Text lightText;

    private void Start()
    {
        // Read CSV-Data
        using (var reader = new StreamReader(""))
        // Fill data into suitable data-structure
        // Use data to calculate Score

        // Set clamp for temperature slider
        temperatureSlider.minValue = 3;
        temperatureSlider.maxValue = 24;

        // Set clmap for light level slider
        lightLevelSlider.minValue = 0.2f;
        lightLevelSlider.maxValue = 0.7f;
      
        UpdateTemperatureText();
        UpdateLightText();
    }
    public void SetLightLevel(float value)
    {
        lightLevel = value;
        UpdateLightText();
    }
    public void SetTemperature(float value)
    {
        temperatureLevel = (int) value;
        UpdateTemperatureText();
    }
    private void UpdateLightText()
    {
        if (lightText != null)
        {
            lightText.text = string.Format("{0:P0}", lightLevel);
        }
    }
    private void UpdateTemperatureText()
    {
        if (temperatureText != null)
        {
            temperatureText.text = temperatureLevel + " °C";
        }
    }
}
