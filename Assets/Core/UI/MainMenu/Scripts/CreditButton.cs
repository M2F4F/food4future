/**
    Author: Diro Baloska
    Collaborator: 
*/
using UnityEngine;

public class CreditButton : MonoBehaviour
{
    public delegate void OnCreditButton();
    public static event OnCreditButton onCreditButton;
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
        onCreditButton?.Invoke();
    }
}
