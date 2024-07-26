using UnityEngine;
using UnityEngine.UI;

public class LightController : MonoBehaviour
{
    public Light[] pointLights;  // Array to hold references to multiple lights
    public Slider lightSlider;

    void OnEnable()
    {
        if (lightSlider != null)
        {
            lightSlider.onValueChanged.AddListener(ChangeIntensity);
        }
    }

    void OnDisable()
    {
        if (lightSlider != null)
        {
            lightSlider.onValueChanged.RemoveListener(ChangeIntensity);
        }
    }

    void ChangeIntensity(float value)
    {
        if (pointLights != null)
        {
            foreach (var light in pointLights)
            {
                light.intensity = value * 1 / 400;
                Debug.Log("Light intensity: " + light.intensity);
            }
        }
    }
}
