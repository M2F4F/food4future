using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using System.Collections.Generic;

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

    // UI
    public Slider lightLevelSlider;
    public Slider temperatureSlider;
    public Slider silinitySlider;
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
                                //lightLevelSlider.minValue = float.Parse(lightLevelRangeList[0].Split(',')[0]) / 100;
                                //lightLevelSlider.maxValue = float.Parse(lightLevelRangeList[lightLevelRangeList.Count - 1].Split(',')[0]) / 100;
                            }
                            break;
                        case "tempLevel":
                            {
                                cells.RemoveAt(0);
                                temperaturLevelRangeList = cells;
                                //temperatureSlider.minValue = int.Parse(temperaturLevelRangeList[0].Split(',')[0]);
                                //temperatureSlider.maxValue = int.Parse(temperaturLevelRangeList[temperaturLevelRangeList.Count - 1].Split(',')[0]);
                            }
                            break;
                        case "salinity":
                            {
                                cells.RemoveAt(0);
                                salinityLevelRangeList = cells;
                                //silinitySlider.minValue = float.Parse(salinityLevelRangeList[0].Split(',')[0]) / 100;
                                //silinitySlider.maxValue = float.Parse(salinityLevelRangeList[salinityLevelRangeList.Count - 1].Split(',')[0]) / 100;
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

    private void calcScore()
    {
        // calc score with chosen values
        // set in relation to the maximum score (calculated onEnable())
        onVariableChange?.Invoke(score);
    }

    public void SetLightLevel(float value)
    {
        lightLevel = int.Parse(lightLevelRangeList[(int) value].Split(',')[0]);
        UpdateLightText();
        calcScore();
    }
    public void SetTemperature(float value)
    {
        temperatureLevel = int.Parse(temperaturLevelRangeList[(int) value].Split(',')[0]);
        UpdateTemperatureText();
        calcScore();
    }
    public void SetSalinity(float value)
    {
        salinityLevel = int.Parse(salinityLevelRangeList[(int) value].Split(',')[0]);
        UpdateSalinityText();
        calcScore();
    }
    private void UpdateLightText()
    {
        if (lightText != null)
        {
            Debug.Log(lightLevel);
            lightText.text = lightLevel + "%";
        }
    }
    private void UpdateTemperatureText()
    {
        if (temperatureText != null)
        {
            temperatureText.text = temperatureLevel + " °C";
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
