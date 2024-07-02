using UnityEngine;
using UnityEngine.UI;

public class UnselectScene : MonoBehaviour
{
    public delegate void OnUnselectBackButton();
    public static event OnUnselectBackButton onUnselectBackButton;
    // Start is called before the first frame update

    void Start() {
        gameObject.GetComponent<Button>().onClick.AddListener(OnBackButtonClicked);
        gameObject.SetActive(false);
    }

    public void OnBackButtonClicked()
    {
        onUnselectBackButton?.Invoke();

        // Set UI Panel back to true
        gameObject.transform.parent.GetChild(0).gameObject.SetActive(true);

        gameObject.SetActive(false);

    }

}
