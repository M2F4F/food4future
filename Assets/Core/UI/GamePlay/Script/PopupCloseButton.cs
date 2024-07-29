/**
* Collaborator:
* - Diro Baloska 
* - Yosua Sentosa 
* - Gerrit Behrens
**/
using System.Collections;
using System.IO;
using UnityEngine;

public class PopupCloseButton : MonoBehaviour
{
    
    // Start is called before the first frame update
    public void OnClick() {
        this.transform.parent.gameObject.SetActive(false);
    }
}
