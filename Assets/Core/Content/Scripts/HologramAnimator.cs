using System;
using System.Collections;
using UnityEngine;

public class HologramAnimator : MonoBehaviour
{
    [SerializeField] private Vector3 _hologramSize;
    [SerializeField] private float _duration;
    [SerializeField] private float _rotationSpeed;
    private GameObject pyramid;
    private Vector3 _humanSize;
    private Vector3 _originalRotation;
    private Vector3 _lastRotation;
    private bool _shouldStopRotating;
    private GameObject _level;
    private Vector3 _levelHologramPosition;
    private FollowAnchor _followAnchor;
        
    void Awake() {
        pyramid = gameObject.transform.GetChild(0).gameObject;
        _humanSize = gameObject.transform.localScale;
        _originalRotation = gameObject.transform.eulerAngles;
        _shouldStopRotating = false;
        _followAnchor = gameObject.GetComponent<FollowAnchor>();
        try {
            _level = transform.GetChild(1).gameObject;
            _levelHologramPosition = _level.transform.localPosition;
        } catch (UnityException) {
            Debug.Log("No level is found");
        }
    }

    void OnEnable() {
        SelectSceneButton.onSelectSceneButton += ZoomnInTransition;
        UnselectScene.onUnselectBackButton += ZoomOutTransition;
    }
    
    void OnDisable() {
        SelectSceneButton.onSelectSceneButton -= ZoomnInTransition;
        UnselectScene.onUnselectBackButton -= ZoomOutTransition;
    }
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = _hologramSize;
        StartCoroutine(SpawnLevel());
    }

    // Update is called once per frame
    void Update()
    {
        if(!_shouldStopRotating) {
            gameObject.transform.eulerAngles = new Vector3(_originalRotation.x, gameObject.transform.eulerAngles.y + _rotationSpeed * Time.deltaTime, _originalRotation.z);
            if(gameObject.transform.eulerAngles.y < -360) gameObject.transform.eulerAngles = new Vector3(_originalRotation.x, gameObject.transform.eulerAngles.y + 360, _originalRotation.z);
            if(gameObject.transform.eulerAngles.y > 360) gameObject.transform.eulerAngles = new Vector3(_originalRotation.x, gameObject.transform.eulerAngles.y - 360, _originalRotation.z);    
        }
    }

    private void RotateAnimation() {

    }

    private void ZoomnInTransition() {
        pyramid.SetActive(false);
        _shouldStopRotating = true;
        _lastRotation = _level.transform.eulerAngles;
        _level.transform.localPosition = Vector3.zero;
        StartCoroutine(ScaleToHumanSize());
    }

    private void ZoomOutTransition() {
        _shouldStopRotating = false;
        StartCoroutine(ScaleToHologram());
        _level.transform.localPosition = _levelHologramPosition;
    }

    private IEnumerator ScaleToHumanSize()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        Vector3 currentRotation = gameObject.transform.eulerAngles;
        Vector3 pyramidScale = pyramid.transform.localScale;
        float currentTime = 0f;

        while (currentTime < _duration)
        {
            currentTime += Time.deltaTime;
            gameObject.transform.localScale = Vector3.Lerp(currentScale, _humanSize, currentTime / _duration);
            transform.eulerAngles = Vector3.Lerp(currentRotation, _followAnchor.anchor.transform.eulerAngles, currentTime / _duration);
            yield return null;
        }
    }

    private IEnumerator ScaleToHologram()
    {
        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, _hologramSize.y, gameObject.transform.localScale.z);
        Vector3 currentScale = gameObject.transform.localScale;
        float currentTime = 0f;
        Vector3 startingRotation = transform.eulerAngles;

        while (currentTime < _duration)
        {
            currentTime += Time.deltaTime;
            gameObject.transform.localScale = Vector3.Lerp(currentScale, _hologramSize, currentTime / _duration);
            transform.eulerAngles = Vector3.Lerp(startingRotation, _lastRotation, currentTime / _duration);
            yield return null;
        }
        pyramid.SetActive(true);
    }

    private IEnumerator SpawnLevel() {
        // TODO: Spawn Level pop up
        transform.localScale = Vector3.zero;
        float currentTime = 0f;

        while(currentTime < 0.2f) {
            gameObject.transform.localScale = Vector3.Lerp(Vector3.zero, new Vector3(_hologramSize.x + 0.05f, _hologramSize.y + 0.05f, _hologramSize.z + 0.05f), currentTime / 0.2f);
            currentTime += Time.deltaTime;
            yield return null;
        }
        StartCoroutine(PopDown());
        yield return null;
    }

    private IEnumerator PopDown() {
        Vector3 fromScale = transform.localScale;
        float currentTime = 0f;

        while(currentTime < 0.1f) {
            gameObject.transform.localScale = Vector3.Lerp(fromScale, _hologramSize, currentTime / 0.1f);
            currentTime += Time.deltaTime;
            yield return null;
        }
        yield return null;
    }
}
