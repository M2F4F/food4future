using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TemperaturSlider : MonoBehaviour
{
    public Slider temp_Slider;
    // Start is called before the first frame update
    void Start()
    {
        temp_Slider.minValue = 1;
        temp_Slider.maxValue = 24;
        Debug.Log(temp_Slider);
        Debug.Log("slider");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
