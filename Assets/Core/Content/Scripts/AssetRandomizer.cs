using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetRandomizer : MonoBehaviour
{
    [SerializeField] float minScale;
    [SerializeField] float maxScale;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.transform.eulerAngles = new Vector3(transform.eulerAngles.x, Random.Range(0, 360), transform.eulerAngles.z);    
        float scale = Random.Range(minScale, maxScale);
        this.gameObject.transform.localScale = new Vector3(scale, scale, scale);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
