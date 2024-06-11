using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupCloseButton : MonoBehaviour
{
    public delegate void OnPopupWindowCloseButton();
    public static event OnPopupWindowCloseButton onPopupWindowCloseButton;
    // Start is called before the first frame update
    public void OnClick() {
        onPopupWindowCloseButton?.Invoke();
    }
}
