using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DisplayEndResult : MonoBehaviour
{
    private int result1 = 0;
    private int result2 = 0;
    private int result3 = 0;

    public TMP_Text result1Text;
    public TMP_Text result2Text;
    public TMP_Text result3Text;

    //TODO: change maxScore hardCode
    private int maxScore1 = 55;
    private int maxScore2 = 0;
    private int maxScore3 = 0;

    void OnEnable()
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
        result1Text.text = "Runde 1: " + result1 + "/" + maxScore1;
        result2Text.text = "Runde 2: " + result2 + "/" + maxScore2;
        result3Text.text = "Runde 3: " + result3 + "/" + maxScore3;
    }

    static bool RowHasData(List<string> cells)
    {
        return cells.Any(x => x.Length > 0);
    }
}
