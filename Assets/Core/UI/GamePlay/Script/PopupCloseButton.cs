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

    [SerializeField] private GameObject _target;
    // Start is called before the first frame update
    public void CloseWithoutTarget() {
        this.transform.parent.parent.gameObject.SetActive(false);
    }

    public void CloseWithTarget() {
        _target.SetActive(false);
    }
}
