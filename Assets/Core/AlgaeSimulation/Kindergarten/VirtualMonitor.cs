using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualMonitor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // this.gameObject.transform.LookAt(GameObject.Find("Main Camera").transform);
        StartCoroutine(LookAtCamera());
        // this.gameObject.transform.rotation = new Quaternion()
    }

    IEnumerator LookAtCamera() {
        this.gameObject.transform.LookAt(GameObject.Find("Main Camera").transform);
        this.gameObject.transform.rotation = Quaternion.Euler(0, this.gameObject.transform.eulerAngles.y, 0);
        yield return new WaitForEndOfFrame();
        StartCoroutine(LookAtCamera());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
