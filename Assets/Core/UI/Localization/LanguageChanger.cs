using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LanguageChanger : MonoBehaviour
{
    [SerializeField] private SO_LocalText _localText;
    private TMP_Text _text;

    void Awake() {
        _text = GetComponent<TMP_Text>();    
    }

    void OnEnable(){
        LanguageManager.onLanguageChange += LanguageChangeHandler;
        string lang = PlayerPrefs.GetString("lang", "de");
        if(lang == "de") {
            _text.text = _localText.deutsch;
            return;
        }
        _text.text = _localText.english; 
        return;
    }
    
    void OnDisable() {
        LanguageManager.onLanguageChange -= LanguageChangeHandler;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void LanguageChangeHandler(string lang){
        if(lang == "de") {
            _text.text = _localText.deutsch;
            return;
        }
        _text.text = _localText.english; 
        return;
    }
}
