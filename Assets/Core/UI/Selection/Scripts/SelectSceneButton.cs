using System;
using System.Collections;
using System.Collections.Generic;
using StateMachine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectSceneButton : MonoBehaviour
{
    [SerializeField] private SO_LocalText[] _phase;
    private TMP_Text _text;
    private string _gameStateName;
    private string _lang;

    private Coroutine scaleCoroutine;

    public delegate void OnSelectSceneButton();
    public static event OnSelectSceneButton onSelectSceneButton;

    void Awake()
    {
        _text = transform.GetChild(0).GetComponent<TMP_Text>();
        _gameStateName = "KindergartenState";
    }

    void OnEnable()
    {
        LanguageManager.onLanguageChange += LanguageChangeHandler;
        GameStateManager.onGameStateChange += StateChangeHandler;
    }

    void OnDisable()
    {
        GameStateManager.onGameStateChange -= StateChangeHandler;
        LanguageManager.onLanguageChange -= LanguageChangeHandler;
    }

    void Start()
    {
        if (PlayerPrefs.GetString("lang", "de") == "de") _text.text = _phase[0].deutsch;
        if (PlayerPrefs.GetString("lang", "de") == "en") _text.text = _phase[0].english;
        _gameStateName = "KindergartenState";

        gameObject.GetComponent<Button>().onClick.AddListener(OnSelectButtonClicked);
    }

    private void LanguageChangeHandler(string lang)
    {
        _lang = lang;
        if (_gameStateName == "KindergartenState")
        {
            if (lang == "de")
            {
                _text.text = _phase[0].deutsch;
                return;
            }
            _text.text = _phase[0].english;
            return;
        }
        if (_gameStateName == "StressTestState")
        {
            if (lang == "de")
            {
                _text.text = _phase[1].deutsch;
                return;
            }
            _text.text = _phase[1].english;
            return;
        }
        if (_gameStateName == "ProductionTestState")
        {
            if (lang == "de")
            {
                _text.text = _phase[2].deutsch;
                return;
            }
            _text.text = _phase[2].english;
            return;
        }
    }

    private void StateChangeHandler(string stateName)
    {
        _gameStateName = stateName;
        if (stateName == "KindergartenState")
        {
            if (_lang == "de")
            {
                _text.text = _phase[0].deutsch;
                return;
            }
            _text.text = _phase[0].english;
            return;
        }
        if (stateName == "StressTestState")
        {
            if (_lang == "de")
            {
                _text.text = _phase[1].deutsch;
                return;
            }
            _text.text = _phase[1].english;
            return;
        }
        if (stateName == "ProductionTestState")
        {
            if (_lang == "de")
            {
                _text.text = _phase[2].deutsch;
                return;
            }
            _text.text = _phase[2].english;
            return;
        }
    }

    public void OnSelectButtonClicked()
    {
        onSelectSceneButton?.Invoke();
        // Deactivate panel
        transform.parent.gameObject.SetActive(false);

        // Deactivate language change button
        transform.parent.parent.GetChild(3).gameObject.SetActive(false);
        
        // Activate back button
        transform.parent.parent.GetChild(1).gameObject.SetActive(true);
        Debug.Log(transform.parent.parent.GetChild(1).gameObject.name);
    }
}
