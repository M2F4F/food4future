using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Localization", menuName = "Localization")]
public class LocalText : ScriptableObject
{
    [TextArea]
    public string english;
    [TextArea]
    public string deutsch;
}
