/**
    Author: Diro Baloska,
    Collaborator: 
*/
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    [SerializeField] private float _waitDuration;
    private Button _button;

    public delegate void OnStartButton();
    public static event OnStartButton onStartButton;
    public static event OnStartButton onStartTransition;


    void Awake() {
        _button = GetComponent<Button>();
    }


    // Start is called before the first frame update
    void Start()
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
