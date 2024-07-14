using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Globalization;

public class VariableManager : MonoBehaviour
{
    // Range data from CSV-File
    public TextAsset textAsset;
    private List<string> lightLevelRangeList;
    private List<string> temperaturLevelRangeList;
    private List<string> salinityLevelRangeList;
    private List<string> phValueRangeList;

    private int[] totalValueArray = Enumerable.Repeat(0, 4).ToArray();

    // Values
    public int temperatureLevel;
    public int lightLevel;
    public int salinityLevel;
    public float phLevel;

    // internal score
    private int score = 0;
    public int maxScore = 0;
    private int lightScore = 0;
    private int temperatureScore = 0;
    private int salinityScore = 0;
    private int phScore = 0;

    // UI
    public Slider lightLevelSlider;
    public Slider temperatureSlider;
    public Slider salinitySlider;
    public Slider phValueSlider;
    public TMP_Text temperatureText;
    public TMP_Text lightText;
    public TMP_Text salinityText;
    public TMP_Text phText;

    public delegate void OnVariableChange(int score, int maxScore, int[] calcScoreArray);
    public static event OnVariableChange onVariableChange;

    private void OnEnable()
    {
        // Read CSV-Data
        TextAsset csv = (TextAsset)Resources.Load("ModelData", typeof(TextAsset));
        string csvText = csv.text;

        List<string> cellStrings = csvText.Split(" ").ToList();
        foreach (var item in cellStrings)
        {
            var cells = item.Split(';').ToList();
            if (RowHasData(cells) && cells != null)
            {
                switch (cells.First())
                {
                    case "lightLevel":
                        {
                            cells.RemoveAt(0);
                            lightLevelRangeList = cells;
                            if (lightLevelSlider != null)
                                lightLevelSlider.maxValue = lightLevelRangeList.Count - 1;
                        }
                        break;
                    case "tempLevel":
                        {
                            cells.RemoveAt(0);
                            temperaturLevelRangeList = cells;
                            if (temperatureSlider != null)
                                temperatureSlider.maxValue = temperaturLevelRangeList.Count - 1;
                        }
                        break;
                    case "salinity":
                        {
                            cells.RemoveAt(0);
                            salinityLevelRangeList = cells;
                            if (salinitySlider != null)
                                salinitySlider.maxValue = salinityLevelRangeList.Count - 1;
                        }
                        break;
                    case "phValue":
                        {
                            cells.RemoveAt(0);
                            phValueRangeList = cells;
                            if (phValueSlider != null)
                                phValueSlider.maxValue = phValueRangeList.Count - 1;
                        }
                        break;
                }
            }
        }

        // Fill data into suitable data-structure
        // Use data to calculate Score

        // Set maxScore for the dataSet
        maxScore =  CalcMaxValue(lightLevelRangeList) * 5 +
                    CalcMaxValue(temperaturLevelRangeList) * 7 +
                    CalcMaxValue(salinityLevelRangeList) * 3+ 
                    CalcMaxValue(phValueRangeList) * 2;

        UpdateTemperatureText();
        UpdateLightText();
        UpdateSalinityText();
    }

    private void OnDisable()
    {
        
    }

    static bool RowHasData(List<string> cells)
    {
        return cells.Any(x => x.Length > 0);
    }

    private int CalcMaxValue(List<string> list)
    {
        var helperList = new List<int>();
        foreach (string val in list)
        {
            helperList.Add(int.Parse(val.Split(',')[1]));
        }
        return helperList.Max();
    }
    private void CheckForCrossDependecy(float value, int index) {
        if(value <= 3)
        {
            totalValueArray[index] = 1;
        } else {
            totalValueArray[index] = 0;
        }
        onVariableChange?.Invoke(score, maxScore, totalValueArray);    
    }
    public void SetLightLevel(float value)
    {
        // Reset score by old value
        score -= lightScore;
        // Get new values
        string[] lightTuple = lightLevelRangeList[(int)value].Split(',');
        lightLevel = int.Parse(lightTuple[0]);
        lightScore = int.Parse(lightTuple[1]) * 5;
        // Update text
        UpdateLightText();
        // Adjust score by new value
        score += lightScore;
        // Invoke new score
        CheckForCrossDependecy(value, 0);       
    }
    public void SetTemperature(float value)
    {
        // Reset score by old value
        score -= temperatureScore;
        // Get new values
        string[] temperatureTuple = temperaturLevelRangeList[(int)value].Split(',');
        temperatureLevel = int.Parse(temperatureTuple[0]);
        temperatureScore = int.Parse(temperatureTuple[1]) * 7;
        // Update text
        UpdateTemperatureText();
        // Adjust score by new values
        score += temperatureScore;
        // Invoke new score
        CheckForCrossDependecy(value, 1); 
    }
    public void SetSalinity(float value)
    {
        // Reset score by old value
        score -= salinityScore;
        // Get new values
        string[] salinityTuple = salinityLevelRangeList[(int)value].Split(',');
        salinityLevel = int.Parse(salinityTuple[0]);
        salinityScore = int.Parse(salinityTuple[1]) * 3;
        // Update text
        UpdateSalinityText();
        // Adjust score by new values
        score += salinityScore;
        // Invoke new score
        CheckForCrossDependecy(value, 2); 
    }
    public void SetPhValue(float value)
    {
        score -= phScore;
        string[] phTuple = phValueRangeList[(int)value].Split(',');
        phLevel = float.Parse(phTuple[0], CultureInfo.InvariantCulture.NumberFormat);
        phScore = int.Parse(phTuple[1]) * 2;
        UpdatePhValueText();
        score += phScore;
        CheckForCrossDependecy(value, 3); 
    }
    private void UpdateLightText()
    {
        if (lightText != null)
        {
            lightText.text = lightLevel + "µ mol";
        }
    }
    private void UpdateTemperatureText()
    {
        if (temperatureText != null)
        {
            temperatureText.text = temperatureLevel + "°C";
        }
    }
    private void UpdateSalinityText()
    {
        if (salinityText != null)
        {
            salinityText.text = salinityLevel + "%";
        }
    }
    private void UpdatePhValueText()
    {
        if (phText != null)
        {
            phText.text = phLevel.ToString("0.00###");
        }
    }
}
