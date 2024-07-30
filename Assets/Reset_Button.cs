using System.IO;
using UnityEngine;

public class Reset_Button : MonoBehaviour
{
    public delegate void OnGameReset();
    public static event OnGameReset onGameReset;
    public void OnClick()
    {
        File.WriteAllText(Path.Combine(Application.persistentDataPath, "Scores"), "0,0,0,0");
        Debug.Log("Reset Game");
        onGameReset?.Invoke();
        //TODO set all slider to value 0 (Invoke something to variable manager instance
        //or get all sliders in scene and set them to value 0
    }
}
