using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIAnimator : MonoBehaviour
{
    [SerializeField] private Type _type;
    [SerializeField] private GameObject _activateTarget;
    [SerializeField] private bool _loop;
    [SerializeField] private float _delay;
    [SerializeField] private float _duration;
    [SerializeField] private Vector3 _endPosition;
    [SerializeField] private Vector3 _endScale;
    [SerializeField] private Color _startColor;
    [SerializeField] private Color _endColor;
    [SerializeField] private Texture2D[] _textures;

    enum Type {
        TRANSLATE,
        SCALE,
        SLIDESHOW,
        TOGGLE_ACTIVATE,
        TOGGLE_VISIBILITY
    }
    private Vector3 _startPosition;
    private Vector3 _startScale;
    
    void Awake() {
        _startPosition = transform.localPosition;
        _startScale = transform.localScale;
    }
    void Start()
    {
        if(_type.CompareTo(Type.TRANSLATE) == 0) StartCoroutine(Translate());
        if(_type.CompareTo(Type.SLIDESHOW) == 0) StartCoroutine(Slideshow(0));
        if(_type.CompareTo(Type.SCALE) == 0) StartCoroutine(Scale());
        if(_type.CompareTo(Type.TOGGLE_ACTIVATE) == 0) StartCoroutine(ToggleActivate());
        if(_type.CompareTo(Type.TOGGLE_VISIBILITY) == 0) StartCoroutine(ToggleVisibility());
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnDisable() {
        StopCoroutine(Translate());
        StopCoroutine(Slideshow(0));
        StopCoroutine(Scale());
    }

    IEnumerator Translate() {
        yield return new WaitForSeconds(_delay);
        
        float elapsedTime = 0f;
        while(elapsedTime < _duration) {
            transform.localPosition = Vector3.Lerp(_startPosition, _endPosition, elapsedTime / _duration);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        yield return null;
    }

    IEnumerator Scale() {
        yield return new WaitForSeconds(_delay);
        
        float elapsedTime = 0f;
        while(elapsedTime < _duration) {
            transform.localScale = Vector3.Lerp(_startScale, _endScale, elapsedTime / _duration);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        if(_loop) StartCoroutine(Scale());
    }

    IEnumerator Slideshow(int index) {
        yield return new WaitForSeconds(_delay);
        if(index >= _textures.Length) index = 0;
        gameObject.GetComponent<RawImage>().texture = _textures[index];
        index++;
        StartCoroutine(Slideshow(index));
    }

    IEnumerator ToggleActivate() {
        yield return new WaitForSeconds(_delay);
        _activateTarget.gameObject.SetActive(!_activateTarget.gameObject.activeSelf);
        yield return null;
    }

    IEnumerator ToggleVisibility() {
        Image imageComponent = gameObject.GetComponent<Image>();
        TMP_Text tmpComponent = gameObject.GetComponent<TMP_Text>();
        // Debug.Log(imageComponent);
        // gameObject.GetComponent<Image>().color = Color.blue;
        yield return new WaitForSeconds(_delay);
        
        float elapsedTime = 0f;
        while(elapsedTime < _duration) {
            if(imageComponent != null)
                imageComponent.color = Vector4.Lerp(_startColor, _endColor, elapsedTime / _duration);
            if(tmpComponent != null)
                tmpComponent.color = Vector4.Lerp(_startColor, _endColor, elapsedTime / _duration);
            // Debug.Log(imageComponent.color);
            
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        if(imageComponent != null) imageComponent.color = _endColor;
        if(tmpComponent != null) tmpComponent.color = _endColor;

        yield return null;
    }
}
