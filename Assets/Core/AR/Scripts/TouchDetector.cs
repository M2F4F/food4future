using UnityEngine;
using UnityEngine.InputSystem;

public class TouchDetector : MonoBehaviour
{
    private ARInputActions inputActions;
    private InputAction touchPressAction;

    void Awake()
    {
        inputActions = new ARInputActions();
        touchPressAction = inputActions.ARMaps.TouchPress;
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
            Debug.Log("User is touching the screen at position: " + touchPosition);
        }
    }
}
