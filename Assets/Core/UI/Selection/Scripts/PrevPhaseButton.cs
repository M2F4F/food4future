using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrevPhaseButton : MonoBehaviour
{
    public delegate void OnPrevPhase();
    public static event OnPrevPhase onPrevPhase;

    public void OnClick() {
        onPrevPhase?.Invoke();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
