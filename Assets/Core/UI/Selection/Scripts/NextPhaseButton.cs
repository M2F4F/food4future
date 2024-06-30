using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextPhaseButton : MonoBehaviour
{
    public delegate void OnNextPhase();
    public static event OnNextPhase onNextPhase;

    public void OnClick() {
        onNextPhase?.Invoke();
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
