using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class NarativeSlideshow : MonoBehaviour
{
    [SerializeField] private SO_LocalText[] _texts;
    [SerializeField] private TMP_Text _counter;
    [SerializeField] private Vector3 _panelPopupPosition;
    [SerializeField] private Vector3 _nextButtonPopupPosition;
    private TMP_Text _textHolder;
    public int _pageIndex { get; private set; }
    private Coroutine _coroutine;
    private Coroutine _moveCoroutine;
    public delegate void OnRenderDone();
    public static event OnRenderDone onRenderDone;
    public delegate void OnPageChange(int pageNumber);
    public static event OnPageChange onPageChange;
    
    private GameObject _parent;
    private Vector3 _parentInitPos;
    private GameObject _nextButton;
    private Vector3 _nextButtonInitPos;

    void OnEnable() {
        DialogueButton.onDialogueButton += DialogueButtonHandler;
        LanguageManager.onLanguageChange += LanguageChangeHandler;
        Reset_Button.onGameReset += ResetButtonHandler;
    }

    void OnDisable() {
        DialogueButton.onDialogueButton -= DialogueButtonHandler;
        LanguageManager.onLanguageChange -= LanguageChangeHandler;
        Reset_Button.onGameReset -= ResetButtonHandler;
    }

    void Awake() {
        _pageIndex = 0;
        _textHolder = GetComponent<TMP_Text>();
        _counter.text = _pageIndex + 1 + " / " + _texts.Length;
        _parent = transform.parent.gameObject;
        _parentInitPos = _parent.transform.localPosition;
        _nextButton = _parent.transform.parent.GetChild(2).gameObject;
        _nextButtonInitPos = _nextButton.transform.localPosition;
    }

    // Start is called before the first frame update
    void Start()
    {
        _coroutine = StartCoroutine(RenderText(GetText(PlayerPrefs.GetString("lang", "de"))));
    }


    private void DialogueButtonHandler()
    {
        // Skip
        if(_coroutine != null) {
            StopCoroutine(_coroutine);
            _coroutine = null;
            _textHolder.text = GetText(PlayerPrefs.GetString("lang", "de"));
            StartCoroutine(WaitToMovePanel());
            StartCoroutine(TriggerEventWithDelay());
            return;
        }

        // If reached the last page
        if(_pageIndex == _texts.Length - 1) return;
        
        if(_moveCoroutine != null) StopCoroutine(_moveCoroutine);
        _pageIndex++;
        onPageChange?.Invoke(_pageIndex + 1);

        _counter.text = _pageIndex + 1 + " / " + _texts.Length;
        _coroutine = StartCoroutine(RenderText(GetText(PlayerPrefs.GetString("lang", "de"))));
    }

    private void LanguageChangeHandler(string lang) {
        if(_coroutine != null) StopCoroutine(_coroutine);
        _coroutine = StartCoroutine(RenderText(GetText(lang)));
    }

    private void ResetButtonHandler() {
        // Check if coroutine is not null or not the last page, then do nothing.
        if(_coroutine != null || _pageIndex <= 0) return;

        _pageIndex = 0;
        _moveCoroutine = StartCoroutine(MoveBackPanel());
        
        _counter.text = _pageIndex + 1 + " / " + _texts.Length;
        _coroutine = StartCoroutine(RenderText(GetText(PlayerPrefs.GetString("lang", "de"))));
    }

    private string GetText(string lang) {
        if(lang == "en") return _texts[_pageIndex].english;
        return _texts[_pageIndex].deutsch;
    }

    IEnumerator TriggerEventWithDelay() {
        yield return new WaitForEndOfFrame();
        onRenderDone?.Invoke();
        yield return null;
    }

    IEnumerator RenderText(string text) {
        _textHolder.text = "";
        for(int position = 0; position < text.Length; position++) {
            _textHolder.text += text.Substring(position, 1);
            yield return new WaitForEndOfFrame();
        }
        _coroutine = null;

        onRenderDone?.Invoke();
        StartCoroutine(WaitToMovePanel());
    }

    private IEnumerator MovePanel() {
        float elapsedTime = 0.0f;
        const float duration = 0.75f;
        RectTransform parentRectransform = _parent.GetComponent<RectTransform>();
        RectTransform nextButtonRectTransform = _nextButton.GetComponent<RectTransform>();
        Vector3 from = parentRectransform.anchoredPosition;
        Vector3 buttonFrom = nextButtonRectTransform.anchoredPosition;
        while (elapsedTime < duration) {
            float progress = elapsedTime / duration;
            parentRectransform.anchoredPosition =  Vector3.Lerp(from, _panelPopupPosition, progress);
            nextButtonRectTransform.anchoredPosition = Vector3.Lerp(buttonFrom, _nextButtonPopupPosition, progress);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }

    private IEnumerator WaitToMovePanel() {
        if(_pageIndex == _texts.Length - 1) {
            yield return new WaitForSeconds(0.1f);
            _moveCoroutine = StartCoroutine(MovePanel());
        }
    }

    private IEnumerator MoveBackPanel() {
        float elapsedTime = 0.0f;
        const float duration = 0.75f;
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
