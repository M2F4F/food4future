using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoAnimator : MonoBehaviour
{
    [SerializeField] private float _duration;
    [SerializeField] private float _delay;
    private float _screenWidth;
    private float _screenHeight;
    // Start is called before the first frame update
    void Start()
    {
        _screenWidth = Screen.width;
        _screenHeight = Screen.height;
        Debug.Log(_screenWidth);
        Debug.Log(_screenHeight);
        StartCoroutine(MoveLogo());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator MoveLogo() {
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        yield return new WaitForSeconds(_delay);
        float elapsedTime = 0f;

        while(elapsedTime < _duration) {
            Vector2 targetPosition = new Vector2(-(_screenWidth / 2) + rectTransform.sizeDelta.x * transform.localScale.x / 2, (_screenHeight / 2) - rectTransform.sizeDelta.y * transform.localScale.y / 2);
            Debug.Log(targetPosition);
            Vector2 target = Vector2.Lerp(this.transform.localPosition, targetPosition, elapsedTime / _duration);
            this.transform.localPosition = target;
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }
}
