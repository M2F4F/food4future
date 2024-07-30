using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseMoveBack : MonoBehaviour
{
    [SerializeField, Tooltip("Duration of closing animation")] private float _animationDuration;
    public GameObject _closeMoveBackButton { get; set;}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator CloseMoveBackCoroutine() {
        float elapsedTime = 0;
        Vector3 originalSize = this.transform.localScale;
        while (elapsedTime <= _animationDuration) {
            this.transform.localScale = Vector3.Lerp(originalSize, new Vector3(0, originalSize.y, originalSize.z), elapsedTime / _animationDuration);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        gameObject.SetActive(false);
        _closeMoveBackButton.gameObject.SetActive(false);
        yield return null;
    }
}
