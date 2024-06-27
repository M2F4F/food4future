using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogButtonAnim : MonoBehaviour
{
    [SerializeField] private Vector3 _position1;
    [SerializeField] private Vector3 _position2;
    [SerializeField] private float _duration;

    void OnEnable() {
        NarativeSlideshow.onRenderDone += () =>  this.gameObject.SetActive(true);
        NextButton.onNextButton += () => this.gameObject.SetActive(false);
        LanguageManager.onLanguageChange += (string lang) => this.gameObject.SetActive(false);
        StartCoroutine(MoveUp());
    }

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
    }

    void OnDisable() {
        NarativeSlideshow.onRenderDone -= () => this.gameObject.SetActive(true);
        NextButton.onNextButton -= () => this.gameObject.SetActive(false);
        LanguageManager.onLanguageChange -= (string lang) => this.gameObject.SetActive(false);
        StopCoroutine(MoveUp());
        StopCoroutine(MoveDown());
    }

    IEnumerator MoveUp() {
        float elapsedTime = 0.0f;
        while (elapsedTime < _duration) {
            transform.localPosition = Vector3.Lerp(_position1, _position2, elapsedTime /_duration);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        StartCoroutine(MoveDown());
    }

    IEnumerator MoveDown() {
        float elapsedTime = 0.0f;
        while (elapsedTime < _duration) {
            transform.localPosition = Vector3.Lerp(_position2, _position1, elapsedTime /_duration);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(MoveUp());
    }
}
