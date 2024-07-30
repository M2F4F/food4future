using UnityEngine;
using UnityEngine.UI;

public class ChangeLightSlider : MonoBehaviour
{
    public delegate void OnChangeLight();
    public static event OnChangeLight onChangeLight;

    public Slider lightSlider;

    void Start()
    {
        if (lightSlider != null)
        {
            lightSlider.onValueChanged.AddListener(delegate { NotifyLightChange(); });
        }
    }

    void NotifyLightChange()
    {
        if (onChangeLight != null)
        {
            onChangeLight.Invoke();
        }
    }
}
