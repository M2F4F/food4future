using UnityEngine;
using UnityEngine.UI;

public class DialogueButton : MonoBehaviour
{
    public delegate void OnDialogueButton();
    public static event OnDialogueButton onDialogueButton;

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
        onDialogueButton?.Invoke();
        button.enabled = false;
    }

    private void ActivateButton() {
        
    }
}
