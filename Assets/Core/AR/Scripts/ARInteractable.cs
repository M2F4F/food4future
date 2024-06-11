using UnityEngine;

public class ARInteractable : MonoBehaviour
{
    public PopupController popupController;

    // Custom behavior when the AR object is clicked
    public void OnClick()
    {
        Debug.Log("AR Interactable Object Clicked: " + gameObject.name);

        // Check if popupController is valid
        if (popupController == null)
        {
            Debug.LogError("PopupController is not assigned to ARInteractable: " + gameObject.name);
            return;
        }

        // Implement custom logic here, such as showing details or performing actions
        popupController.ShowPopup(gameObject.name);
    }
}
