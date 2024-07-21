using System.Collections;
using UnityEngine;
using TMPro;
using System.Linq;

public class VirtualMonitor : MonoBehaviour
{
    [SerializeField] private TMP_Text floatingMonitorText;
    private GameObject statusCube = null;
    private static readonly string POINTS_FOR_COUNTER = "Points";
    private static readonly Color STATUS_BAD = new(1.0f, 0.0f, 0.0f);
    private static readonly Color STATUS_BETTER = new(1.0f, 0.5f, 0.0f);
    private static readonly Color STATUS_GOOD = new(0.25f, 0.5f, 0.0f);
    private static readonly Color STATUS_PERFECT = new(0.0f, 1.0f, 0.0f);

    void Awake()
    {
        statusCube = transform.GetChild(0).GetChild(3).gameObject;
    }
    void OnEnable()
    {
        VariableManager.onVariableChange += SetScore;
        statusCube.GetComponent<Renderer>().material.color = new Color(0.4f, 0.6f, 0.9f);
    }

    void OnDisable()
    {
        VariableManager.onVariableChange -= SetScore;
    }
    // Start is called before the first frame update
    void Start()
    {
        // this.gameObject.transform.LookAt(GameObject.Find("Main Camera").transform);
        //StartCoroutine(LookAtCamera());
        // this.gameObject.transform.rotation = new Quaternion()
    }

    IEnumerator LookAtCamera()
    {
        this.gameObject.transform.LookAt(GameObject.Find("Main Camera").transform);
        this.gameObject.transform.rotation = Quaternion.Euler(0, this.gameObject.transform.eulerAngles.y, 0);
        yield return new WaitForEndOfFrame();
        StartCoroutine(LookAtCamera());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SetScore(int score, int maxScore, int[] calcScoreArray)
    {
        floatingMonitorText.text = score.ToString() + " " + POINTS_FOR_COUNTER;
        var stopCalculation = calcScoreArray.Contains(1);
        // Analyse calcScoreArray
        if (stopCalculation)
        {
            statusCube.GetComponent<Renderer>().material.color = STATUS_BAD;
        }
        else
        {
            UpdateStatus(score, maxScore);
        }
    }

    private void UpdateStatus(int score, int maxScore)
    {

        var percent = score / (float)maxScore;
        switch (percent)
        {
            case float p when p <= 0.60f:
                statusCube.GetComponent<Renderer>().material.color = STATUS_BAD;
                break;
            case float p when p > 0.60f && p <= 0.80f:
                statusCube.GetComponent<Renderer>().material.color = STATUS_BETTER;
                break;
            case float p when p > 0.80f && p <= 0.98f:
                statusCube.GetComponent<Renderer>().material.color = STATUS_GOOD;
                break;
            case float p when p == 1f:
                statusCube.GetComponent<Renderer>().material.color = STATUS_PERFECT;
                break;
        }
    }
}
