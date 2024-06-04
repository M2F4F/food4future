using UnityEngine;

public class ARInteractable : MonoBehaviour
{
    // Custom behavior when the AR object is clicked
    public void OnClick()
    {
        Debug.Log("AR Interactable Object Clicked: " + gameObject.name);
        // Implement custom logic here, such as showing details or performing actions
    }
}
