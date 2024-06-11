using UnityEngine;
using TMPro;

public class PopupController : MonoBehaviour
{
    public GameObject popupWindow; // Assign this in the Inspector
    public GameObject popupPanel;
    public TextMeshProUGUI popupText;

    void Start()
    {
        // Automatically find the popupPanel and popupText within the popupWindow
        if (popupWindow != null)
        {
            popupPanel = popupWindow.transform.Find("PopupPanel").gameObject;
           
            popupText = popupWindow.transform.Find("PopupPanel/ObjectName").GetComponent<TextMeshProUGUI>();
            Debug.Log("START CLICKING OBJECT HERE");
            Debug.Log("POPUPWINDOW  " + popupWindow.name);

            Debug.Log("PopupPanel from the start: " + (popupPanel != null ? popupPanel.name : "null"));
            Debug.Log("PopupText from the start: " + (popupText != null ? popupText.text : "null"));
        }

        // Ensure the popup is hidden at the start
        if (popupPanel != null)
        {
            //popupPanel.SetActive(false);
        }
        else
        {
            Debug.LogError("Popup panel is not assigned or found!");
        }

        if (popupText == null)
        {
            Debug.LogError("Popup text is not assigned or found!");
        }
    }

    public void ShowPopup(string objectName)
    {
        Debug.Log("Showing Popup for: " + gameObject.name);
        if (popupPanel == null || popupText == null)
        {
            Debug.LogError("Popup panel or text is not assigned!");
            return;
        }

        ActivateParent(popupWindow);
        popupText.text = "Object Name: " + objectName;
        popupWindow.SetActive(true);
        popupPanel.SetActive(true);

        // Additional debugging information
        Debug.Log("Popup Name: " + popupPanel.name);
        Debug.Log("PopupPanel activeSelf: " + popupPanel.activeSelf);
        Debug.Log("PopupPanel activeInHierarchy: " + popupPanel.activeInHierarchy);
        Debug.Log("PopupWindow activeInHierarchy: " + popupWindow.activeInHierarchy);
        Debug.Log("PopupText: " + popupText.text);

    }

    public void ClosePopup()
    {
        Debug.Log("Closing Popup for: " + gameObject.name);
        if (popupPanel != null)
        {
            popupPanel.SetActive(false);
        }
    }

    // Helper method to activate all parents of a GameObject
    private void ActivateParent(GameObject obj)
    {
        if (obj == null) return;

        Transform current = obj.transform;
        while (current != null)
        {
            current.gameObject.SetActive(true);
            current = current.parent;
        }
    }
}
