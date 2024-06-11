/**
* Author:
* - Yosua Sentosa 
**/
using UnityEngine;

public class ARInteractable : MonoBehaviour
{
    //public PopupController popupController;

    public delegate void OnInteraction(string objectName, string description);
    public static OnInteraction onInteraction;

    // Custom behavior when the AR object is clicked
    public virtual void OnClick()
    {
        // Debug.Log("AR Interactable Object Clicked: " + gameObject.name);

        // Check if popupController is valid
        //if (popupController == null)
        //{
        //    // Debug.LogError("PopupController is not assigned to ARInteractable: " + gameObject.name);
        //    return;
        //}

        // Implement custom logic here, such as showing details or performing actions
        //popupController.ShowPopup(gameObject.name);
    }
}
