/**
* Collaborator:
* - Diro Baloska
* - Yosua Sentosa 
**/
using UnityEngine;

[CreateAssetMenu(fileName = "FileName", menuName = "Display Information")]
public class SO_Information : ScriptableObject
{
    public string objectName;
    [TextArea(10, 20)]
    public string description;
    [TextArea(10, 20)]
    public string EnglishDescription;

}
