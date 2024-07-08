using UnityEngine;
using UnityEngine.UI;

public class LightController : MonoBehaviour
{
    public Light pointLight;
    public Slider lightSlider;

    void OnEnable()
    {
        ChangeLightSlider.onChangeLight += ChangeIntensity;
    }

    void OnDisable()
    {
        ChangeLightSlider.onChangeLight -= ChangeIntensity;
    }

    void ChangeIntensity()
    {
        if (pointLight != null && lightSlider != null)
        {
            pointLight.intensity = lightSlider.value;
        }
    }
}
