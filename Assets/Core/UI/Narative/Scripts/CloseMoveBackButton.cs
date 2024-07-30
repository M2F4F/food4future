using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseMoveBackButton : MonoBehaviour
{
    private GameObject _moveBackPanel;

    void Awake() {
        _moveBackPanel = transform.parent.GetChild(3).gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(this.OnCloseMoveBack);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCloseMoveBack() {
        Debug.Log("Clicked");
        StartCoroutine(_moveBackPanel.GetComponent<CloseMoveBack>().CloseMoveBackCoroutine());
    }
}
