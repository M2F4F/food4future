using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogButtonAnim : MonoBehaviour
{
    [SerializeField] private Vector3 _position1;
    [SerializeField] private Vector3 _position2;
    [SerializeField] private float _duration;

    void Awake() {
        DialogueButton.onDialogueButton += Disable;
        NarativeSlideshow.onRenderDone += Enable;
    }
    void OnEnable() {
        LanguageManager.onLanguageChange += ToggleActive;
        StartCoroutine(MoveUp());
    }

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
    }

    void OnDestroy() {
        DialogueButton.onDialogueButton -= Disable;
        NarativeSlideshow.onRenderDone -= Enable;
        StopCoroutine(MoveUp());
        StopCoroutine(MoveDown());
    }

    void OnDisable() {
        LanguageManager.onLanguageChange -= ToggleActive;
    }

    private void Disable() {
        this.gameObject.SetActive(false);
    }

    private void Enable() {
        this.gameObject.SetActive(true);
    }

    IEnumerator MoveUp() {
        float elapsedTime = 0.0f;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        while (elapsedTime < _duration) {
            rectTransform.anchoredPosition = Vector3.Lerp(_position1, _position2, elapsedTime /_duration);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        StartCoroutine(MoveDown());
    }

    IEnumerator MoveDown() {
        float elapsedTime = 0.0f;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        while (elapsedTime < _duration) {
            rectTransform.anchoredPosition = Vector3.Lerp(_position2, _position1, elapsedTime /_duration);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(MoveUp());
    }

    private void ToggleActive(string lang) {
        this.gameObject.SetActive(!this.gameObject.activeSelf);
    }
}
