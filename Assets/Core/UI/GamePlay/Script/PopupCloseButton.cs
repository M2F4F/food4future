using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupCloseButton : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnClick() {
        this.transform.parent.gameObject.SetActive(false);
    }
}
