/**
    Author: Diro Baloska
    Collaborator: 
*/
using System.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using StateMachine;

public class ARSessionManager : MonoBehaviour
{
    private ARSession m_arSession;

    public delegate void OnARUnsupported();
    public event OnARUnsupported onARUnsupported;

    void Awake() {
        m_arSession = this.GetComponent<ARSession>();
        m_arSession.enabled = false;
    }

    void OnEnable() {
        PlayState.onPlayState += ToggleARSession;
        PlayState.onPlayStateExit += ToggleARSession;
    }

    void OnDisable() {
        PlayState.onPlayState -= ToggleARSession; 
        PlayState.onPlayStateExit -= ToggleARSession; 
    }
    
    // Start is called before the first frame update
    void Start()
    {
        // StartCoroutine(CheckARSession());
    }

    IEnumerator CheckARSession() {
        if ((ARSession.state == ARSessionState.None) ||
            (ARSession.state == ARSessionState.CheckingAvailability))
        {
            yield return ARSession.CheckAvailability();
        }

        switch (ARSession.state)
        {
            case ARSessionState.Ready : Debug.Log("AR Session ready to use"); m_arSession.enabled = true; break;
            case ARSessionState.Unsupported : Debug.Log("Unsupported"); break;
            case ARSessionState.NeedsInstall : Debug.Log("Needs install"); StartCoroutine(ARSession.Install()); break;
            default: break;
        }
        
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ToggleARSession() {
        if(m_arSession.enabled) m_arSession.enabled = !m_arSession.enabled;
        StartCoroutine(CheckARSession());
    }
}
