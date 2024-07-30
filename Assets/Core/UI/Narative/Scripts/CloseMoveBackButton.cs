using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseMoveBackButton : MonoBehaviour
{
    private GameObject _moveBackPanel;
    private Coroutine _autoClose;

    void Awake() {
        _moveBackPanel = transform.parent.GetChild(3).gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(this.OnCloseMoveBack);
        _autoClose = StartCoroutine(AutoClose());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable() {
        StopCoroutine(_autoClose);
    }

    private void OnCloseMoveBack() {
        StartCoroutine(_moveBackPanel.GetComponent<CloseMoveBack>().CloseMoveBackCoroutine());
    }

    IEnumerator AutoClose() {
        yield return new WaitForSeconds(10);
        OnCloseMoveBack();
    }
}
