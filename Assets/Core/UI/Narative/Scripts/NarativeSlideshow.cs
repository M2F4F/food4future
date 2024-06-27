using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class NarativeSlideshow : MonoBehaviour
{
    [SerializeField] private LocalText[] _texts;
    [SerializeField] private TMP_Text _counter;
    [SerializeField] private Vector3 _panelPopupPosition;
    [SerializeField] private Vector3 _nextButtonPopupPosition;
    private TMP_Text _textHolder;
    private int _index;
    private Coroutine _coroutine;
    private Coroutine _moveCoroutine;
    public delegate void OnRenderDone();
    public static event OnRenderDone onRenderDone;
    
    private GameObject _parent;
    private Vector3 _parentInitPos;
    private GameObject _nextButton;
    private Vector3 _nextButtonInitPos;

    void OnEnable() {
        NextButton.onNextButton += NextButtonHandler;
        LanguageManager.onLanguageChange += LanguageChangeHandler;
    }

    void OnDisable() {
        NextButton.onNextButton -= NextButtonHandler;
        LanguageManager.onLanguageChange -= LanguageChangeHandler;
    }

    void Awake() {
        _index = 0;
        _textHolder = GetComponent<TMP_Text>();
        _counter.text = _index + 1 + " / " + _texts.Length;
        _parent = transform.parent.gameObject;
        _parentInitPos = _parent.transform.localPosition;
        _nextButton = _parent.transform.parent.GetChild(2).gameObject;
        Debug.Log(_nextButton);
        _nextButtonInitPos = _nextButton.transform.localPosition;
    }

    // Start is called before the first frame update
    void Start()
    {
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

        if(_index == _texts.Length - 1) {
            yield return new WaitForSeconds(0.5f);
            _moveCoroutine = StartCoroutine(MovePanel());
        }
    }


    private void NextButtonHandler()
    {
        if(_moveCoroutine != null) StopCoroutine(_moveCoroutine);
        _index++;

        // If reached the last page
        if(_index == _texts.Length) {
            _index = 0;
            _moveCoroutine = StartCoroutine(MoveBackPanel());
        }

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

    private IEnumerator MovePanel() {
        float elapsedTime = 0.0f;
        const float duration = 0.75f;
        Vector3 from = _parent.transform.localPosition;
        Vector3 buttonFrom = _nextButton.transform.localPosition;
        while (elapsedTime < duration) {
            float progress = elapsedTime / duration;
            _parent.transform.localPosition = Vector3.Lerp(from, _panelPopupPosition, progress);
            _nextButton.transform.localPosition = Vector3.Lerp(buttonFrom, _nextButtonPopupPosition, progress);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }

    private IEnumerator MoveBackPanel() {
        float elapsedTime = 0.0f;
        const float duration = 1.0f;
        Vector3 from = _parent.transform.localPosition;
        Vector3 buttonFrom = _nextButton.transform.localPosition;
        while (elapsedTime < duration) {
            float progress = elapsedTime / duration;
            _parent.transform.localPosition = Vector3.Lerp(from, _parentInitPos, progress);
            _nextButton.transform.localPosition = Vector3.Lerp(buttonFrom, _nextButtonInitPos, progress);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }
}
