using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInteraction : MonoBehaviour
{
    [SerializeField] private SO_Information m_information;
    [SerializeField] private Enum_Informations m_label;

    public delegate void OnARButtonInteraction(string objectName, string description);
    public static OnARButtonInteraction onARButtonInteraction;
    
    public void OnClick()
    {
        onARButtonInteraction?.Invoke(m_information.objectName, m_information.description);
    }
}
