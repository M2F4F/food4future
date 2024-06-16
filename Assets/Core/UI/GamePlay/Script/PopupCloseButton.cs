/**
* Collaborator:
* - Diro Baloska 
* - Yosua Sentosa 
**/
using UnityEngine;

public class PopupCloseButton : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnClick() {
        this.transform.parent.gameObject.SetActive(false);
    }
}
