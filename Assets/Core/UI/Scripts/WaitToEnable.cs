using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaitToEnable : MonoBehaviour
{
    [SerializeField] private float _enableIn;
    private Button _button;

    void Awake() {
        _button = GetComponent<Button>();
        _button.enabled = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnableButton());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    IEnumerator EnableButton() {
        yield return new WaitForSeconds(_enableIn);
        _button.enabled = true;
    }
}
