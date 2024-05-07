using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARSessionManager : MonoBehaviour
{
    [SerializeField]
    private ARSession m_arSession;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Here");
        StartCoroutine(CheckARSession());
    }

    IEnumerator CheckARSession() {
        if ((ARSession.state == ARSessionState.None) ||
            (ARSession.state == ARSessionState.CheckingAvailability))
        {
            Debug.Log("Here");
            yield return ARSession.CheckAvailability();
        }

        switch (ARSession.state)
        {
            case ARSessionState.Ready : m_arSession.enabled = true; break;
            case ARSessionState.Unsupported : /* TODO: Show on UI */break;
            default: break;
        }
        
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
