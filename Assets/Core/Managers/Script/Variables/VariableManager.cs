using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using CsvHelper;
using UnityEditor;
using Unity.VisualScripting;

public class ScoreRow
{
    public int PhaseNr { get; set; }
    public int s1 {  get; set; }
    public int s2 { get; set; }
    public int s3 { get; set; }
    public int s4 { get; set; }
    public int Score { get; set; }
}
public class VariableManager : MonoBehaviour
{
    // Range data from CSV-File
    public TextAsset textAsset;
    private List<string> lightLevelRangeList;
    private List<string> temperaturLevelRangeList;
    private List<string> salinityLevelRangeList;
    private List<string> phValueRangeList;

    private int[] TotalValueArray = Enumerable.Repeat(0, 4).ToArray();

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
    private int PhaseNrForPersistence = 0;

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
    public static event OnVariableChange OnVariableChangeEvent;

    private void OnEnable()
    {
        // Read CSV-Data
        ReadData();

        // Set maxScore for the dataSet - maxScore per stage
        CalculateMaxValuePerSceen();

        // Set old values from disk
        if (File.Exists("Assets/Core/Resources/Scores.csv"))
        {
            SetExistingValue();
        }
        
        UpdateTemperatureText();
        UpdateLightText();
        UpdateSalinityText();
        UpdatePhValueText();
    }

    private void OnDisable()
    {
        SaveScoreIntoCsv();
    }

    private void ReadData()
    {
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
                            {
                                phValueSlider.maxValue = phValueRangeList.Count - 1;
                            }
                        }
                        break;
                }
            }
        }
    }

    private void CalculateMaxValuePerSceen()
    {
        if (phValueSlider && salinitySlider && !lightLevelSlider && !temperatureSlider)
        {
            maxScore = 0;
            maxScore += CalcMaxValue(salinityLevelRangeList) * 3 + CalcMaxValue(phValueRangeList) * 2;
            PhaseNrForPersistence = 1;
        }
        else if (!phValueSlider && !salinitySlider && lightLevelSlider && temperatureSlider)
        {
            maxScore = 0;
            maxScore += CalcMaxValue(lightLevelRangeList) * 5 + CalcMaxValue(temperaturLevelRangeList) * 7;
            PhaseNrForPersistence = 2;
        }
        else
        {
            maxScore = 0;
            maxScore += CalcMaxValue(lightLevelRangeList) * 5 +
                    CalcMaxValue(temperaturLevelRangeList) * 7 +
                    CalcMaxValue(salinityLevelRangeList) * 3 +
                    CalcMaxValue(phValueRangeList) * 2;
            PhaseNrForPersistence = 3;
        }
    }

    private void SetExistingValue()
    {
        using (var reader = new StreamReader("Assets/Core/Resources/Scores.csv"))
        using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            csvReader.Read();
            csvReader.ReadHeader();
            while (csvReader.Read())
            {
                var record = csvReader.GetRecord<ScoreRow>();

                switch (record.PhaseNr)
                {
                    case 1:
                        {
                            salinityScore = record.s1;
                            phScore = record.s2;
                            break;
                        }
                    case 2:
                        {
                            lightScore = record.s3;
                            temperatureScore = record.s4;
                            break;
                        }
                    case 3:
                        {
                            salinityScore = record.s1;
                            phScore = record.s2;
                            lightScore = record.s3;
                            temperatureScore = record.s4;
                            break;
                        }
                }
                if (record.PhaseNr == PhaseNrForPersistence)
                {
                    switch (PhaseNrForPersistence)
                    {
                        case 1:
                            {
                                int scoreCountS1 = salinityScore / 3;
                                int indexForValueS1 = salinityLevelRangeList.FindIndex(0, x => x.Contains("," + scoreCountS1.ToString()));
                                if (indexForValueS1 != -1) salinitySlider.value = indexForValueS1;

                                int scoreCountS2 = phScore / 2;
                                int indexForValueS2 = phValueRangeList.FindIndex(0, x => x.Contains("," + scoreCountS2.ToString()));
                                if (indexForValueS2 != -1) phValueSlider.value = indexForValueS2;

                                score = record.s1 + record.s2;
                                break;
                            }
                        case 2:
                            {
                                int scoreCountS3 = lightScore / 5;
                                int indexForValueS3 = lightLevelRangeList.FindIndex(0, x => x.Contains("," + scoreCountS3.ToString()));
                                if (indexForValueS3 != -1) lightLevelSlider.value = indexForValueS3;

                                int scoreCountS4 = temperatureScore / 7;
                                int indexForValueS4 = temperaturLevelRangeList.FindIndex(0, x => x.Contains("," + scoreCountS4.ToString()));
                                if (indexForValueS4 != -1) temperatureSlider.value = indexForValueS4;

                                score = record.s3 + record.s4;
                                break;
                            }
                        case 3:
                            {
                                salinityScore = record.s1;
                                int scoreCountS1 = salinityScore / 3;
                                int indexForValueS1 = salinityLevelRangeList.FindIndex(0, x => x.Contains("," + scoreCountS1.ToString()));
                                if (indexForValueS1 != -1) salinitySlider.value = indexForValueS1;

                                phScore = record.s2;
                                int scoreCountS2 = phScore / 2;
                                int indexForValueS2 = phValueRangeList.FindIndex(0, x => x.Contains("," + scoreCountS2.ToString()));
                                if (indexForValueS2 != -1) phValueSlider.value = indexForValueS2;

                                lightScore = record.s3;
                                int scoreCountS3 = lightScore / 5;
                                int indexForValueS3 = lightLevelRangeList.FindIndex(0, x => x.Contains("," + scoreCountS3.ToString()));
                                if (indexForValueS3 != -1) lightLevelSlider.value = indexForValueS3;

                                temperatureScore = record.s4;
                                int scoreCountS4 = temperatureScore / 7;
                                int indexForValueS4 = temperaturLevelRangeList.FindIndex(0, x => x.Contains("," + scoreCountS4.ToString()));
                                if (indexForValueS4 != -1) temperatureSlider.value = indexForValueS4;

                                score = record.s1 + record.s2 + record.s3 + record.s4;
                                break;
                            }
                    }
                    // Object is null if we change to sceen 2
                    OnVariableChangeEvent?.Invoke(score, maxScore, TotalValueArray);
                }
            }
        };
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
    private void CheckForCrossDependecy(float value, int index)
    {
        if (value <= 3)
        {
            TotalValueArray[index] = 1;
        }
        else
        {
            TotalValueArray[index] = 0;
        }
        
        OnVariableChangeEvent?.Invoke(score, maxScore, TotalValueArray);
    }

    public void SaveScoreIntoCsv()
    {
        var ScoreSet = new List<ScoreRow>()
        {
            new() {PhaseNr = 1, s1 = salinityScore, s2 = phScore},
            new() {PhaseNr = 2, s3 = lightScore, s4 = temperatureScore},
            new() {PhaseNr = 3, s1 = salinityScore, s2 = phScore, s3 = lightScore, s4 = temperatureScore}
        };
        using (var writer = new StreamWriter("Assets/Core/Resources/Scores.csv"))
        using (var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            csvWriter.WriteRecords(ScoreSet);
        }
    }
    public void SetLightLevel(float value)
    {
        if (lightLevelRangeList != null)
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
            CheckForCrossDependecy(lightScore / 5, 0);
        }
    }
    public void SetTemperature(float value)
    {
        if (temperaturLevelRangeList != null)
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
            CheckForCrossDependecy(temperatureScore / 7, 1);
        }
    }
    public void SetSalinity(float value)
    {
        if (salinityLevelRangeList != null)
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
            CheckForCrossDependecy(salinityScore / 3, 2);
        }
    }
    public void SetPhValue(float value)
    {
        if (phValueRangeList != null)
        {
            // Reset score by old value
            score -= phScore;
            // Get new values
            string[] phTuple = phValueRangeList[(int)value].Split(',');
            phLevel = float.Parse(phTuple[0], CultureInfo.InvariantCulture.NumberFormat);
            phScore = int.Parse(phTuple[1]) * 2;
            // Update text
            UpdatePhValueText();
            score += phScore;
            // Invoke new score
            CheckForCrossDependecy(phScore / 2, 3);
        }
    }
    private void UpdateLightText()
    {
        if (lightText != null)
        {
            lightText.text = lightLevel + "µ mol/m²/Tag";
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
