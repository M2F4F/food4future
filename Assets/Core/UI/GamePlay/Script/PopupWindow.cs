/**
* Collaborator:
* - Diro Baloska 
* - Yosua Sentosa 
**/

using UnityEngine;
using TMPro;

public class PopupWindow : MonoBehaviour
{
    [SerializeField] private InformationLabel m_label;

    private GameObject m_textPanel;
    private TMP_Text m_objectName;

    void Awake()
    {
        m_objectName = transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>();
        m_textPanel = transform.GetChild(0).gameObject;
    }

    void OnEnable()
    {
        ButtonInteraction.onARButtonInteraction += PopupWindowHandler;
    }

    void OnDisable()
    {
        ButtonInteraction.onARButtonInteraction -= PopupWindowHandler;
    }

    /**
     * <summary>Show pop up window when information button is triggered</summary>
     * 
     */
    private void PopupWindowHandler(InformationLabel label, string objectName, string description)
    {
        if(m_label.CompareTo(label) != 0) return;
        m_objectName.text = objectName;
        m_textPanel.SetActive(true);
    }
}
