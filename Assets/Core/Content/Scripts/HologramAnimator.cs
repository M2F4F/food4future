using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HologramAnimator : MonoBehaviour
{
    [SerializeField] private Vector3 _humanSize;
    [SerializeField] private float _duration;
    private GameObject pyramid;
    private Vector3 _hologramSize;
        
    void Awake() {
        pyramid = gameObject.transform.GetChild(0).gameObject;
        _hologramSize = gameObject.transform.localScale;
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
        StartCoroutine(SpawnLevel());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ZoomnInTransition() {
        pyramid.SetActive(false);
        StartCoroutine(ScaleToHumanSize());
    }

    private void ZoomOutTransition() {
        StartCoroutine(ScaleToHologram());
    }

    private IEnumerator ScaleToHumanSize()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        float currentTime = 0f;

        while (currentTime < _duration)
        {
            currentTime += Time.deltaTime;
            gameObject.transform.localScale = Vector3.Lerp(currentScale, _humanSize, currentTime / _duration);
            yield return null;
        }
    }

    private IEnumerator ScaleToHologram()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        float currentTime = 0f;

        while (currentTime < _duration)
        {
            currentTime += Time.deltaTime;
            gameObject.transform.localScale = Vector3.Lerp(currentScale, _hologramSize, currentTime / _duration);
            yield return null;
        }
        pyramid.SetActive(true);
    }

    private IEnumerator SpawnLevel() {
        // TODO: Spawn Level pop up
        yield return null;
    }
}
