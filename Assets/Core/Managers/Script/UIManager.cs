using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject m_mainMenu;

    void Awake() {
        
    }

    void OnEnable() {
        Debug.Log("On Enable");
        MenuState.onMenuState += SpawnMainMenu;
        MenuState.onMenuStateExited += DespawnMainMenu;
    }

    void OnDisable() {
        MenuState.onMenuState -= SpawnMainMenu;
        MenuState.onMenuStateExited -= DespawnMainMenu;

    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnMainMenu() {
        Debug.Log("Setting Main Menu to active");
        m_mainMenu.SetActive(true);
    }

    private void DespawnMainMenu() {
        m_mainMenu.SetActive(false);
    }
}
