using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NarativeSlideshow : MonoBehaviour
{
    [SerializeField] private string[] _texts;
    [SerializeField] private TMP_Text _counter;
    private TMP_Text _textHolder;
    private int _index;

    public delegate void OnRenderDone();
    public static event OnRenderDone onRenderDone;
    
    void OnEnable() {
        NextButton.onNextButton += NextButtonHandler;
    }

    void OnDisable() {
        NextButton.onNextButton -= NextButtonHandler;
    }


    // Start is called before the first frame update
    void Start()
    {
        _index = 0;
        _textHolder = GetComponent<TMP_Text>();
        _counter.text = _index + 1 + " / " + _texts.Length;
        StartCoroutine(RenderText());
    }


    IEnumerator RenderText() {
        _textHolder.text = "";
        for(int position = 0; position < _texts[_index].Length; position++) {
            _textHolder.text += _texts[_index].Substring(position, 1);
            yield return new WaitForEndOfFrame();
        }
        onRenderDone?.Invoke();
    }


    private void NextButtonHandler()
    {
        _index++;
        if(_index == _texts.Length) _index = 0;
        _counter.text = _index + 1 + " / " + _texts.Length;
        StartCoroutine(RenderText());
    }
}
