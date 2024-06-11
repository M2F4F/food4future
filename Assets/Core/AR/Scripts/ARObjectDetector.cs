using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using TMPro;

public class ARObjectDetector : MonoBehaviour
{
    private ARRaycastManager raycastManager;
    private Camera arCamera;
    private ARInputActions inputActions;
    private InputAction touchPressAction;

    void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
        arCamera = Camera.main;

        inputActions = new ARInputActions();
        touchPressAction = inputActions.ARMaps.TouchPress; // Correctly referencing ARMaps action map

    }

    void OnEnable()
    {
        touchPressAction.Enable();
    }

    void OnDisable()
    {
        touchPressAction.Disable();
    }

    void Update()
    {
        if (touchPressAction.triggered)
        {
            Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
            // Debug.Log("User touched the screen at position: " + touchPosition);

            if (!IsPointerOverUI(touchPosition))
            {
                Ray ray = arCamera.ScreenPointToRay(touchPosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    // Debug.Log("Raycast hit: " + hit.collider.name);
                    ARInteractable interactable = hit.collider.GetComponent<ARInteractable>();

                    if (interactable != null)
                    {   
                        // Debug.Log("AR Object Clicked: " + interactable.gameObject.name);
                        interactable.OnClick();
                    }
                }
                else
                {
                    // Debug.Log("Raycast did not hit any object");
                }
            }
        }
    }

    private bool IsPointerOverUI(Vector2 touchPosition)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current)
        {
            position = touchPosition
        };
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
    }
}
