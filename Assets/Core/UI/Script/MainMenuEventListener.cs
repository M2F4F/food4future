
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject _mainMenu;
    // Start is called before the first frame update
    void Start()
    {
        MenuState.onMenuStateExited += Disable;    
    }

    private void Disable() {
        _mainMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
