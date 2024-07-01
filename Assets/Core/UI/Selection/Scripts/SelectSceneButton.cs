using System;
using System.Collections;
using System.Collections.Generic;
using StateMachine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectSceneButton : MonoBehaviour
{
    [SerializeField] private LocalText[] _phase;
    private TMP_Text _text;
    private string _gameStateName;
    private string _lang;

    public Button buttonBack; // Assign the Back button in the Inspector
    public Button buttonSelect; // Assign the Select button in the Inspector
    public GameObject pKindergartenPrefab; // Assign the P_Kindergarten Prefab in the Inspector
    public GameObject pyramid; // Assign the Pyramid GameObject in the Inspector
    public GameObject panel; // Assign the Panel GameObject in the Inspector
    private GameObject pKindergartenInstance;
    private Coroutine scaleCoroutine;

    void Awake()
    {
        _text = transform.GetChild(0).GetComponent<TMP_Text>();
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

    void Start()
    {
        if (PlayerPrefs.GetString("lang") == "de") _text.text = _phase[0].deutsch;
        if (PlayerPrefs.GetString("lang") == "en") _text.text = _phase[0].english;
        _gameStateName = "KindergartenState";

        buttonSelect.onClick.AddListener(OnSelectButtonClicked);
        buttonBack.onClick.AddListener(OnBackButtonClicked);

        // Initially hide the Back button
        buttonBack.gameObject.SetActive(false);

        // Find the instantiated clone of P_Kindergarten
        pKindergartenInstance = GameObject.Find("P_Kindergarten(Clone)");
        if (pKindergartenInstance == null)
        {
            Debug.LogError("P_Kindergarten clone not found in the scene.");
        }
    }

    public void OnSelectButtonClicked()
    {
        if (pKindergartenInstance == null) return;

        Debug.Log("Select phase");

        // buttonSelect.gameObject.SetActive(false);
        buttonBack.gameObject.SetActive(true);


        // Scale P_Kindergarten to 1 over 1 second
        if (scaleCoroutine != null) StopCoroutine(scaleCoroutine);
        scaleCoroutine = StartCoroutine(ScaleOverTime(pKindergartenInstance, Vector3.one, 1f, true, panel, false));
    }

    public void OnBackButtonClicked()
    {
        if (pKindergartenInstance == null) return;

        // Show the Select, Left, and Right buttons and hide the Back button
        // buttonSelect.gameObject.SetActive(true);
        buttonBack.gameObject.SetActive(false);
        panel.SetActive(true);

        // Show the pyramid before starting the animation
        pyramid.SetActive(true);

        // Scale P_Kindergarten to 0.1 over 1 second
        if (scaleCoroutine != null) StopCoroutine(scaleCoroutine);
        scaleCoroutine = StartCoroutine(ScaleOverTime(pKindergartenInstance, new Vector3(0.1f, 0.1f, 0.1f), 1f, false, null, true));
    }

    private IEnumerator ScaleOverTime(GameObject target, Vector3 toScale, float duration, bool hidePyramidAfter, GameObject panelToDeactivate, bool activatePanelAfter)
    {
        Vector3 originalScale = target.transform.localScale;
        float currentTime = 0f;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            target.transform.localScale = Vector3.Lerp(originalScale, toScale, currentTime / duration);
            yield return null;
        }

        target.transform.localScale = toScale;

        // Hide the pyramid if needed
        if (hidePyramidAfter)
        {
            pyramid.SetActive(false);
        }

        // Deactivate the panel if provided
        if (panelToDeactivate != null)
        {
            panelToDeactivate.SetActive(false);
        }

        // Activate the panel if needed
        if (activatePanelAfter)
        {
            panel.SetActive(true);
        }
    }

    void Update()
    {

    }
}
