using System.Collections;
using UnityEngine;
using TMPro;

public class VirtualMonitor : MonoBehaviour
{
    [SerializeField] private TMP_Text floatingMonitorText;
    public GameObject statusCube;
    private static readonly string POINTS_FOR_COUNTER = "Points";
    private static readonly Color STATUS_BAD = new(1.0f, 0.0f, 0.0f);
    private static readonly Color STATUS_BETTER = new(1.0f, 0.5f, 0.0f);
    private static readonly Color STATUS_GOOD = new(0.25f, 0.5f, 0.0f);
    private static readonly Color STATUS_PERFECT = new(0.0f, 1.0f, 0.0f);
    void OnEnable()
    {
        VariableManager.onVariableChange += SetScore;
        statusCube.GetComponent<Renderer>().material.color = new Color(0.4f, 0.6f, 0.9f);
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

    private void SetScore(int score, int maxScore) {
        floatingMonitorText.text = score.ToString() + " " + POINTS_FOR_COUNTER;
        UpdateStatus(score, maxScore);
    }

    private void UpdateStatus(int score, int maxScore)
    {
        var percent = score / (float) maxScore;
        switch(percent)
        {
            case float p when p <= 0.33f: 
                Debug.Log("BAD");
                statusCube.GetComponent<Renderer>().material.color = STATUS_BAD;
                break;
            case float p when p > 0.33f && p <= 0.66f:
                Debug.Log("BETTER");
                statusCube.GetComponent<Renderer>().material.color = STATUS_BETTER;
                break;
            case float p when p > 0.66f && p <= 0.98f:
                Debug.Log("GOOD");
                statusCube.GetComponent<Renderer>().material.color = STATUS_GOOD;
                break;
            case float p when p == 1f:
                Debug.Log("PERFECT");
                statusCube.GetComponent<Renderer>().material.color = STATUS_PERFECT;
                break;
        }
    }
}
