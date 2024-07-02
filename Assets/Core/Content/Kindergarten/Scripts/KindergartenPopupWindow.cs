using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KindergartenPopupWindow : MonoBehaviour
{
    GameObject textPanel;
    TMP_Text objectName;
    // Start is called before the first frame update
    void Start()
    {
        Interactable_WoodLong.onWoodLongInteraction += OnWoodLongInteractionHandler;
        // PopupCloseButton.onPopupWindowCloseButton += OnPopupWindowCloseButtonHandler;
        objectName = transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>();
        textPanel = transform.GetChild(0).gameObject;
        // Debug.Log(transform.GetChild(0).gameObject);
        StartCoroutine(SpawnPopupWindow());
    }

    private void OnPopupWindowCloseButtonHandler()
    {
        objectName.text = "Object Name:";
        textPanel.SetActive(false);
    }

    IEnumerator SpawnPopupWindow() {
        yield return new WaitForSeconds(5);
        OnWoodLongInteractionHandler("Coroutine");
    }

    private void OnWoodLongInteractionHandler(string message)
    {
        objectName.text += message;
        textPanel.SetActive(true);
        // Debug.Log(textPanel.activeSelf);
        // Debug.Log(textPanel.activeInHierarchy);
        
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
