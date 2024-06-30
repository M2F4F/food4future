using UnityEngine;
using UnityEngine.UI;

public class NextButton : MonoBehaviour
{
    public delegate void OnNextButton();
    public static event OnNextButton onNextButton;

    void OnEnable() {

    }

    void OnDisable() {
        
    }

    void Awake() {

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick() {
        onNextButton?.Invoke();
    }

    private void ActivateButton() {
        
    }
}
