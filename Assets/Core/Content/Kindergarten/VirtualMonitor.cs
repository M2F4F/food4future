using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;

public class VirtualMonitor : MonoBehaviour
{
    [SerializeField] private TMP_Text floatingMonitorText;
    public Slider progressBar;
    public GameObject fillArea;
    public float fillSpeed = 0.5f;
    private float targetProgress = 0;
    //private GameObject statusCube = null;
    private static readonly string POINTS_FOR_COUNTER = "Punkte";
    private static readonly Color STATUS_BAD = new(1.0f, 0.0f, 0.0f);
    private static readonly Color STATUS_BETTER = new(1.0f, 0.5f, 0.0f);
    private static readonly Color STATUS_GOOD = new(0.25f, 0.5f, 0.0f);
    private static readonly Color STATUS_PERFECT = new(0.0f, 1.0f, 0.0f);

    void OnEnable()
    {
        VariableManager.OnVariableChangeEvent += SetScore;
        if(progressBar != null)
        {
            fillArea = GameObject.Find("ColurfullFill");
        }
    }

    void OnDisable()
    {
        VariableManager.OnVariableChangeEvent -= SetScore;
    }

    void Update()
    {
        if ( progressBar.value > targetProgress)
        {
            progressBar.value += fillSpeed * Time.deltaTime;
        }
        else
        {
            progressBar.value -= fillSpeed * Time.deltaTime;
        }
    }

    private void SetScore(int score, int maxScore, int[] calcScoreArray)
    {
        floatingMonitorText.text = score.ToString() + " " + POINTS_FOR_COUNTER;
        var stopCalculation = calcScoreArray.Contains(1);
        var percent = score / (float)maxScore;
        if (percent != progressBar.value)
        {
            SetProgress(percent);
        }
        // Analyse calcScoreArray
        if (stopCalculation)
        {
            fillArea.GetComponent<Renderer>().material.color = STATUS_BAD;
        }
        else
        {
            UpdateStatus(percent);
        }
    }

    private void UpdateStatus(float percent)
    {
        switch (percent)
        {
            case float p when p <= 0.60f:
                fillArea.GetComponent<Renderer>().material.color = STATUS_BAD;
                break;
            case float p when p > 0.60f && p <= 0.80f:
                fillArea.GetComponent<Renderer>().material.color = STATUS_BETTER;
                break;
            case float p when p > 0.80f && p <= 0.98f:
                fillArea.GetComponent<Renderer>().material.color = STATUS_GOOD;
                break;
            case float p when p == 1f:
                fillArea.GetComponent<Renderer>().material.color = STATUS_PERFECT;
                break;
        }
    }

    private void SetProgress(float newProgress)
    {
        targetProgress = newProgress;
        Debug.Log("Set to: " + newProgress);
    }
}
