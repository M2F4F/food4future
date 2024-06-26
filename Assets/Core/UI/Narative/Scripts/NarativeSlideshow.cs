using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarativeSlideshow : MonoBehaviour
{
    [SerializeField] private GameObject[] _texts;
    private int _index;
    // Start is called before the first frame update
    void Start()
    {
        _index = 0;
    }

    IEnumerator RenderText() {
        
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
