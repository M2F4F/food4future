
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TransitionMask : MonoBehaviour
{
    [SerializeField] private float _duration;
    [SerializeField] private Color _startColor;
    [SerializeField] private Color _endColor;
    private Image _image;
    void OnEnable() {
        StartButton.onStartTransition += StartTransitionHandler;
    }

    void OnDisable() {
        StartButton.onStartTransition -= StartTransitionHandler;

    }
    // Start is called before the first frame update
    void Start()
    {
        _image = gameObject.GetComponent<Image>();
    }
    
    
    private void StartTransitionHandler()
    {
        StartCoroutine(Transition());
    }

    IEnumerator Transition() {
        float elapsedTime = 0f;

        while(elapsedTime < _duration) {
            this._image.color = Color.Lerp(_startColor, _endColor, elapsedTime / _duration);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        };
        
    }
}
