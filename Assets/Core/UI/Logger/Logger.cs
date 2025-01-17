/**
* Author:
* - Diro Baloska
**/
using UnityEngine;

public class Logger : MonoBehaviour
{
    private string _log;
    private GameObject _toggleConsole;
    private bool _shouldShowConsole;

    void Awake() {
        _shouldShowConsole = false;
    }
    void OnEnable() {
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable() {
        Application.logMessageReceived -= HandleLog;
    }

    void OnGUI() {
        // #if !UNITY_EDITOR
        if(_shouldShowConsole) _log = GUI.TextArea(new Rect(5, Screen.height - 205, Screen.width - 10, Screen.height - 20), _log);
        // #endif
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void HandleLog(string condition, string stackTrace, LogType type) {
        _log = type.ToString() + ": " + condition + "\n" + _log;
    }

    public void ToggleLog() {
        _shouldShowConsole = !_shouldShowConsole;
    }
}
