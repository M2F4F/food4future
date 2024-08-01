using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;
using System;

public class VirtualMonitor : MonoBehaviour
{
    [SerializeField] private TMP_Text floatingMonitorText;
    public Slider progressBar;
    private Image fillArea;
    public float fillSpeed = 0.5f;
    private float targetProgress = 0;
    //private GameObject statusCube = null;
    private static string pointCounter;
    private static readonly Color STATUS_BAD = new(0.85f, 0.25f, 0.25f);
    private static readonly Color STATUS_BETTER = new(1.0f, 0.5f, 0.0f);
    private static readonly Color STATUS_GOOD = new(0.25f, 0.5f, 0.0f);
    private static readonly Color STATUS_PERFECT = new(0.0f, 1.0f, 0.0f);

    void OnEnable()
    {
        VariableManager.OnVariableChangeEvent += SetScore;
        LanguageManager.onLanguageChange += HandleLanguageChange;
        pointCounter = PlayerPrefs.GetString("lang", "de") == "de" ? "Punkte" : "Points";
        if(progressBar != null)
        {
            fillArea = progressBar.gameObject.transform.Find("Fill Area").Find("ColorfullFill").GetComponent<Image>();
        }
    }

    void OnDisable()
    {
        VariableManager.OnVariableChangeEvent -= SetScore;
        LanguageManager.onLanguageChange -= HandleLanguageChange;
    }

    void Update()
    {
        float barvalue = (float) Math.Round(progressBar.value, 2);
        if(barvalue != targetProgress)
        {
            if (barvalue < targetProgress)
            {
                progressBar.value += fillSpeed * Time.deltaTime;
            }
            else if (barvalue > targetProgress)
            {
                progressBar.value -= fillSpeed * Time.deltaTime;
            }    
        }
        
    }

    

    private void HandleLanguageChange(string lang)
    {
        if(lang == "de") {
            VirtualMonitor.pointCounter = "Punkte";
            floatingMonitorText.text = floatingMonitorText.text.Replace("Points", "Punkte");
            return;
        }
        VirtualMonitor.pointCounter = "Points";
        floatingMonitorText.text = floatingMonitorText.text.Replace("Punkte", "Points");
    }

    private void SetScore(int score, int maxScore, int[] calcScoreArray)
    {
        floatingMonitorText.text = score.ToString() + " " + VirtualMonitor.pointCounter;
        var stopCalculation = calcScoreArray.Contains(1);
        var percent = score / (float)maxScore;
        if (percent != progressBar.value)
        {
            SetProgress(percent);
        }
        // Analyse calcScoreArray
        if (stopCalculation)
        {
            fillArea.color = STATUS_BAD;
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
                fillArea.color = STATUS_BAD;
                break;
            case float p when p > 0.60f && p <= 0.80f:
                fillArea.color = STATUS_BETTER;
                break;
            case float p when p > 0.80f && p <= 0.98f:
                fillArea.color = STATUS_GOOD;
                break;
            case float p when p == 1f:
                fillArea.color = STATUS_PERFECT;
                break;
        }
    }

    private void SetProgress(float newProgress)
    {
        targetProgress = (float) Math.Round(newProgress, 2);
    }
}
