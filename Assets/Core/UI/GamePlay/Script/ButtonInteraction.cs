using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInteraction : MonoBehaviour
{
    [SerializeField] private SO_Information m_information;
    [SerializeField] private InformationLabel m_label;

    public delegate void OnARButtonInteraction(InformationLabel m_label, string objectName, string description);
    public static OnARButtonInteraction onARButtonInteraction;
    
    public void OnClick()
    {
        onARButtonInteraction?.Invoke(m_label, m_information.objectName, m_information.description);
    }
}
