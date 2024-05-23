using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kindergarten : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.transform.LookAt(GameObject.Find("Main Camera").transform);
        // this.gameObject.transform.rotation = new Quaternion()
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
