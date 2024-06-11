using UnityEngine;
using TMPro;

public class SliderHandler : MonoBehaviour
{
    [SerializeField]
    private TMP_Text textToChange;

    public void OnSliderValueChanged(float value)
    {
        Debug.Log("Slider value changed: " + value);
        textToChange.text = value.ToString("F2"); 
    }
}
