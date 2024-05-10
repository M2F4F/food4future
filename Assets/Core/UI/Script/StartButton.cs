/**
    Author: Diro Baloska,
    Collaborator: 
*/
using UnityEngine;

public class StartButton : MonoBehaviour
{

    public delegate void OnStartButton();
    public static event OnStartButton onStartButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonPress() {
        onStartButton?.Invoke();
    }
}
