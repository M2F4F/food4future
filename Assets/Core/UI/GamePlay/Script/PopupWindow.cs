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
    private TMP_Text m_title;
    private TMP_Text m_subtitle;
    private TMP_Text m_informationBody;

    private SO_Information m_description;

    void Awake()
    {
        m_textPanel = transform.GetChild(0).gameObject;
        m_textPanel.SetActive(false);
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
        ButtonInteraction.onARButtonInteraction -= PopupWindowHandler;
        LanguageManager.onLanguageChange -= FillText;
    }

    void Start() {
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

        m_textPanel.SetActive(true);
    }

    private void FillText(string lang) {
        if (lang == "de") {
            m_title.text = m_description.title.deutsch;
            m_subtitle.text = m_description.subtitle.deutsch;
            m_informationBody.text = m_description.subtitle.deutsch;
            return;
        }

        m_title.text = m_description.title.english;
        m_subtitle.text = m_description.subtitle.english;
        m_informationBody.text = m_description.subtitle.english;
        return;
    }
}
