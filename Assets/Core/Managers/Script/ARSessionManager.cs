/**
    Author: Diro Baloska
    Collaborator: 
*/
using System.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARSessionManager : MonoBehaviour
{
    [SerializeField]
    private ARSession m_arSession;

    public delegate void OnARUnsupported();
    public event OnARUnsupported onARUnsupported;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CheckARSession());
    }

    IEnumerator CheckARSession() {
        if ((ARSession.state == ARSessionState.None) ||
            (ARSession.state == ARSessionState.CheckingAvailability))
        {
            yield return ARSession.CheckAvailability();
        }

        switch (ARSession.state)
        {
            case ARSessionState.Ready : m_arSession.enabled = true; break;
            case ARSessionState.Unsupported : onARUnsupported?.Invoke(); break;
            default: break;
        }
        
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
