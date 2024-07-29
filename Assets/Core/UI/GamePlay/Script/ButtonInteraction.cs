/**
* Collaborator:
* - Diro Baloska 
* - Yosua Sentosa 
**/
using UnityEngine;


public class ButtonInteraction : MonoBehaviour
{
    [SerializeField] private SO_Information description;
    [SerializeField] private InformationLabel m_label;

    public delegate void OnARButtonInteraction(InformationLabel m_label, SO_Information description);
    public static OnARButtonInteraction onARButtonInteraction;
    
    public void OnClick()
    {
        onARButtonInteraction?.Invoke(m_label, description);
    }
}
