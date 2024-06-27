using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextButton : MonoBehaviour
{
    public delegate void OnNextButton();
    public static event OnNextButton onNextButton;

    private Button button;

    void OnEnable() {
        NarativeSlideshow.onRenderDone += () => button.enabled = true;
        LanguageManager.onLanguageChange += (string lang) => button.enabled = false;
    }

    void OnDisable() {
        NarativeSlideshow.onRenderDone -= () => button.enabled = true;
        LanguageManager.onLanguageChange -= (string lang) => button.enabled = false;
    }

    void Awake() {
        button = GetComponent<Button>();
    }
    // Start is called before the first frame update
    void Start()
    {
        button.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick() {
        onNextButton?.Invoke();
        button.enabled = false;
    }

    private void ActivateButton() {
        
    }
}
