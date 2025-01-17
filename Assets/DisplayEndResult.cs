/*
 * Author: Gerrit Behrens 
*/

using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;

public class DisplayEndResult : MonoBehaviour
{
    private int result1 = 0;
    private int result2 = 0;
    private int result3 = 0;

    public TMP_Text result1Text;
    public TMP_Text result2Text;
    public TMP_Text result3Text;
    public TMP_Text summaryText;

    //TODO: change maxScore hardCode
    private readonly int maxScore1 = 55;
    private readonly int maxScore2 = 132;
    private readonly int maxScore3 = 187;

    void OnEnable()
    {
        this.ReadData();
        this.SetResultTexts();
        this.SetSummaryText();
    }

    private void ReadData()
    {
        string csvText = File.ReadAllText(Path.Combine(Application.persistentDataPath, "Scores"));

        List<string> csvList = csvText.Split(";").ToList();
        foreach (string row in csvList)
        {
            var cells = row.Split(',').ToList();
            if (RowHasData(cells) && cells != null)
            {
                result1 = int.Parse(cells[0]) + int.Parse(cells[1]);
                result2 = int.Parse(cells[2]) + int.Parse(cells[3]);
                result3 = int.Parse(cells[0]) + int.Parse(cells[1]) + int.Parse(cells[2]) + int.Parse(cells[3]);
            }
        }
    }

    private void SetResultTexts()
    {
        string currentLang = PlayerPrefs.GetString("lang", "de");
        if (currentLang.Equals("de")) {
            result1Text.text = "Kindergarten: " + result1 + "/" + maxScore1;
            result2Text.text = "Stresstest: " + result2 + "/" + maxScore2;
            result3Text.text = "Produktion: " + result3 + "/" + maxScore3;
            return;
        }
        result1Text.text = "Kindergarten: " + result1 + "/" + maxScore1;
        result2Text.text = "Stress test: " + result2 + "/" + maxScore2;
        result3Text.text = "Poduction: " + result3 + "/" + maxScore3;
    }

    private void SetSummaryText()
    {
        var percent = result3 / (float) maxScore3;
        switch (percent)
        {
            case float p when p <= 0.60f:
                summaryText.text = PlayerPrefs.GetString("lang", "de") == "de" ? "Durch die veränderung der Umgebungsparameter sind deine Algen leider gestorben." : "Due to the change in environmental parameters, your algae have unfortunately died.";
                break;
            case float p when p > 0.60f && p <= 0.80f:
                summaryText.text = PlayerPrefs.GetString("lang", "de") == "de" ? "Durch die veränderung der Umgebungsparameter hat sich die Masse der Algen leider nicht genug erhöht." : "Due to the change in environmental parameters, unfortunately your algae doesn't grow much.";
                break;
            case float p when p > 0.80f && p <= 0.98f:
                summaryText.text = PlayerPrefs.GetString("lang", "de") == "de" ? "Durch die veränderung der Umgebungsparameter sind genug Algen für die Weiterverarbeitung gewachsen." : "With the change in environmental parameters, your algae has grown enough for next steps.";
                break;
            case float p when p == 1f:
                summaryText.text = PlayerPrefs.GetString("lang", "de") == "de" ? "Durch die veränderung der Umgebungsparameter hast du die perfekte Menge an Algen produziert." : "With the environmental parameters, you have successed to produce the perfect amount of algae.";
                break;
        }
    }

    static bool RowHasData(List<string> cells)
    {
        return cells.Any(x => x.Length > 0);
    }
}
