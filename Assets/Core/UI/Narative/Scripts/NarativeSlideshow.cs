using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NarativeSlideshow : MonoBehaviour
{
    [SerializeField] private LocalText[] _texts;
    [SerializeField] private TMP_Text _counter;
    private TMP_Text _textHolder;
    private int _index;
    private Coroutine _coroutine;
    public delegate void OnRenderDone();
    public static event OnRenderDone onRenderDone;
    
    void OnEnable() {
        NextButton.onNextButton += NextButtonHandler;
        LanguageManager.onLanguageChange += LanguageChangeHandler;
    }

    void OnDisable() {
        NextButton.onNextButton -= NextButtonHandler;
        LanguageManager.onLanguageChange -= LanguageChangeHandler;
    }


    // Start is called before the first frame update
    void Start()
    {
        _index = 0;
        _textHolder = GetComponent<TMP_Text>();
        _counter.text = _index + 1 + " / " + _texts.Length;
        _coroutine = StartCoroutine(RenderText(GetText(PlayerPrefs.GetString("lang"))));
    }


    IEnumerator RenderText(string text) {
        _textHolder.text = "";
        for(int position = 0; position < text.Length; position++) {
            _textHolder.text += text.Substring(position, 1);
            yield return new WaitForEndOfFrame();
        }
        onRenderDone?.Invoke();
        _coroutine = null;
    }


    private void NextButtonHandler()
    {
        _index++;
        if(_index == _texts.Length) _index = 0;
        _counter.text = _index + 1 + " / " + _texts.Length;
        _coroutine = StartCoroutine(RenderText(GetText(PlayerPrefs.GetString("lang"))));
    }

    private void LanguageChangeHandler(string lang) {
        if(_coroutine != null) StopCoroutine(_coroutine);
        _coroutine = StartCoroutine(RenderText(GetText(lang)));
    }

    private string GetText(string lang) {
        if(lang == "en") return _texts[_index].english;
        return _texts[_index].deutsch;
    }
}
