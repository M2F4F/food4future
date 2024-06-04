using UnityEngine;
using TMPro;

public class TextChanger : MonoBehaviour
{
    [SerializeField]
    private TMP_Text textToChange;

    public void ChangeTextOnClick()
    {
        Debug.Log("TextChanger clicked");
        if (textToChange != null)
        {
            textToChange.text = "newText";
        }
    }
}
