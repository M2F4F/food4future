using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VirtualMonitor : MonoBehaviour
{
    [SerializeField] private TMP_Text floatingMonitorText;
    void OnEnable()
    {
        VariableManager.onVariableChange += setScore;
    }

    void OnDisable()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        // this.gameObject.transform.LookAt(GameObject.Find("Main Camera").transform);
        StartCoroutine(LookAtCamera());
        // this.gameObject.transform.rotation = new Quaternion()
    }

    IEnumerator LookAtCamera() {
        this.gameObject.transform.LookAt(GameObject.Find("Main Camera").transform);
        this.gameObject.transform.rotation = Quaternion.Euler(0, this.gameObject.transform.eulerAngles.y, 0);
        yield return new WaitForEndOfFrame();
        StartCoroutine(LookAtCamera());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void setScore(int score) {
        floatingMonitorText.text = score.ToString();
    }
}
