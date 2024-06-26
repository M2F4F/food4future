/**
    Author: Diro Baloska,
    Collaborator: 
*/
using System.Collections;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    [SerializeField] private float _waitDuration;

    public delegate void OnStartButton();
    public static event OnStartButton onStartButton;
    public static event OnStartButton onStartTransition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonPress() {
        StartCoroutine(WaitThenInvoke());
    }

    IEnumerator WaitThenInvoke() {
        onStartTransition?.Invoke();
        yield return new WaitForSeconds(_waitDuration);
        onStartButton?.Invoke();
    }
}
