using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour
{
    public delegate void OnExitButton();
    public static event OnExitButton onExitButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonPress() {
        Debug.Log("Clicked");
        onExitButton?.Invoke();
    }
}
