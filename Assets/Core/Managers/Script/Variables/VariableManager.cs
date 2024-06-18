using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using UnityEditor.Rendering;

public class VariableManager : MonoBehaviour
{
    // Range data from CSV-File
    private List<string> lightLevelRangeList;
    private List<string> temperaturLevelRangeList;
    private List<string> salinityLevelRangeList;

    // Values
    public int temperatureLevel;
    public int lightLevel;
    public int salinityLevel;

    // internal score
    private int score = 0;
    private int lightScore = 0;
    private int temperatureScore = 0;
    private int salinityScore = 0;

    // UI
    public Slider lightLevelSlider;
    public Slider temperatureSlider;
    public Slider salinitySlider;
    public TMP_Text temperatureText;
    public TMP_Text lightText;
    public TMP_Text salinityText;

    public delegate void OnVariableChange(int score);
    public static event OnVariableChange onVariableChange;

    private void Start()
    {
        // Read CSV-Data
        using (var reader = new StreamReader("Assets/Core/Data/ModelData.csv"))
        {
            while (reader.EndOfStream == false)
            {
                var content = reader.ReadLine();
                var cells = content.Split(';').ToList();
                if (RowHasData(cells) && cells != null)
                {
                    switch (cells.First())
                    {
                        case "lightLevel":
                            {
                                cells.RemoveAt(0);
                                lightLevelRangeList = cells;
                                lightLevelSlider.maxValue = lightLevelRangeList.Count - 1;
                            }
                            break;
                        case "tempLevel":
                            {
                                cells.RemoveAt(0);
                                temperaturLevelRangeList = cells;
                                temperatureSlider.maxValue = temperaturLevelRangeList.Count - 1;
                            }
                            break;
                        case "salinity":
                            {
                                cells.RemoveAt(0);
                                salinityLevelRangeList = cells;
                                salinitySlider.maxValue = salinityLevelRangeList.Count - 1;
                            }
                            break;
                    }
                }
            }
        }

        // Fill data into suitable data-structure
        // Use data to calculate Score

        UpdateTemperatureText();
        UpdateLightText();
        UpdateSalinityText();
    }

    static bool RowHasData(List<string> cells)
    {
        return cells.Any(x => x.Length > 0);
    }
    public void SetLightLevel(float value)
    {
        // Reset score by old value
        score -= lightScore;
        // Get new values
        string[] lightTuple = lightLevelRangeList[(int) value].Split(',');
        lightLevel = int.Parse(lightTuple[0]);
        lightScore = int.Parse(lightTuple[1]);
        // Update text
        UpdateLightText();
        // Adjust score by new value
        score += lightScore;
        // Invoke new score
        onVariableChange?.Invoke(score);
    }
    public void SetTemperature(float value)
    {
        // Reset score by old value
        score -= temperatureScore;
        // Get new values
        string[] temperatureTuple = temperaturLevelRangeList[(int) value].Split(',');
        temperatureLevel = int.Parse(temperatureTuple[0]);
        temperatureScore = int.Parse(temperatureTuple[1]);
        // Update text
        UpdateTemperatureText();
        // Adjust score by new values
        score += temperatureScore;
        // Invoke new score
        onVariableChange?.Invoke(score);
    }
    public void SetSalinity(float value)
    {
        // Reset score by old value
        score -= salinityScore;
        // Get new values
        string[] salinityTuple = salinityLevelRangeList[(int) value].Split(',');
        salinityLevel = int.Parse(salinityTuple[0]);
        salinityScore = int.Parse(salinityTuple[1]);
        // Update text
        UpdateSalinityText();
        // Adjust score by new values
        score += salinityScore;
        // Invoke new score
        onVariableChange?.Invoke(score);
    }
    private void UpdateLightText()
    {
        if (lightText != null)
        {
            lightText.text = lightLevel + "%";
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
}
