/**
* Collaborator:
* - Diro Baloska 
* - Yosua Sentosa 
**/
using UnityEngine;

public class CloseRound : MonoBehaviour
{
    public GameObject endOverlay;
    // Start is called before the first frame update
    public void OnClick()
    {
        // set overlay active
        this.transform.parent.gameObject.SetActive(false);
    }
}
