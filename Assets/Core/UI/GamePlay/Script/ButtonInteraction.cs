/**
* Collaborator:
* - Diro Baloska 
* - Yosua Sentosa 
**/
using UnityEngine;
using UnityEngine.UI;


public class ButtonInteraction : MonoBehaviour
{
    [SerializeField] private SO_Information description;
    [SerializeField] private InformationLabel m_label;

    public delegate void OnARButtonInteraction(InformationLabel m_label, SO_Information description);
    public static OnARButtonInteraction onARButtonInteraction;
    
    void Start() {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        onARButtonInteraction?.Invoke(m_label, description);
    }
}
