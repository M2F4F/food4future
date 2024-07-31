/**
* Collaborator:
* - Diro Baloska 
* - Yosua Sentosa 
**/

using UnityEngine;
using TMPro;
using System;

public class PopupWindow : MonoBehaviour
{
    [SerializeField] private InformationLabel m_label;

    private GameObject m_textPanel;
    private TMP_Text m_title;
    private TMP_Text m_subtitle;
    private TMP_Text m_informationBody;

    private SO_Information m_description;

    void Awake()
    {
        m_textPanel = transform.GetChild(0).gameObject;
        m_title = m_textPanel.transform.GetChild(0).GetComponent<TMP_Text>();
        m_subtitle = m_textPanel.transform.GetChild(1).GetComponent<TMP_Text>();
        m_informationBody = m_textPanel.transform.GetChild(2).GetComponent<TMP_Text>();
        
    }

    void OnEnable()
    {
        ButtonInteraction.onARButtonInteraction += PopupWindowHandler;
        LanguageManager.onLanguageChange += FillText;
    }

    void OnDisable()
    {
        LanguageManager.onLanguageChange -= FillText;
    }

    void OnDestroy() {
        ButtonInteraction.onARButtonInteraction -= PopupWindowHandler;
    }

    void Start() {
        m_textPanel.transform.parent.gameObject.SetActive(false);
    }

    /**
     * <summary>Show pop up window when information button is triggered</summary>
     * 
     */
    private void PopupWindowHandler(InformationLabel label, SO_Information description)
    {
        Debug.Log("Debuggin in popupwindowHandler "+label);
        if(m_label.CompareTo(label) != 0) return;
        m_description = description;

        this.FillText(PlayerPrefs.GetString("lang", "de"));

        m_textPanel.transform.parent.gameObject.SetActive(true);
    }

    private void FillText(string lang) {
        try {
            if (lang == "de") {
                m_title.text = m_description.title.deutsch;
                m_subtitle.text = m_description.subtitle.deutsch;
                m_informationBody.text = m_description.content.deutsch;
                return;
            }

            m_title.text = m_description.title.english;
            m_subtitle.text = m_description.subtitle.english;
            m_informationBody.text = m_description.content.english;
            return;
        } catch (NullReferenceException e) {
            Debug.Log(e.ToString());
            Debug.Log(transform.parent.gameObject.name);
        }
    }
}
