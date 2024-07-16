using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupOpenVarController : MonoBehaviour
{
    public GameObject canvas;
    public GameObject panel;

    // Method to set the Canvas active
    public void ShowCanvas()
    {
        if (canvas != null)
        {
            canvas.SetActive(true);
            if(panel) panel.SetActive(true);
        }
        else
        {
            Debug.LogError("Canvas is not assigned in the inspector.");
        }
    }
}

