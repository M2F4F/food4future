using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectSceneButton : MonoBehaviour
{
    [SerializeField] private LocalText[] _phase;
    private TMP_Text _text;
    private string _gameStateName;

    void Awake() {
        _text = transform.GetChild(0).GetComponent<TMP_Text>();
    }
    void OnEnable() {
        LanguageManager.onLanguageChange += LanguageChangeHanlder;
    }

    void OnDisable() {
        LanguageManager.onLanguageChange -= LanguageChangeHanlder;
    }

    private void LanguageChangeHanlder(string lang)
    {
        if(_gameStateName == "KindergartenState"){
            if(lang == "de") {
                _text.text = _phase[0].deutsch;
                return;
            }
            _text.text = _phase[0].english;
            return;
        }
        if(_gameStateName == "StressTestState"){
            if(lang == "de") {
                _text.text = _phase[1].deutsch;
                return;
            }
            _text.text = _phase[1].english;
            return;
        }
        if(_gameStateName == "ProductionTestState"){
            if(lang == "de") {
                _text.text = _phase[2].deutsch;
                return;
            }
            _text.text = _phase[2].english;
            return;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetString("lang") == "de") _text.text = _phase[0].deutsch;
        if(PlayerPrefs.GetString("lang") == "en") _text.text = _phase[0].english;
        _gameStateName = "KindergartenState";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
