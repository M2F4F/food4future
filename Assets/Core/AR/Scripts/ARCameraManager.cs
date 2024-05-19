using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARCameraManager : MonoBehaviour
{
    [SerializeField] [Tooltip("Image Library to be detected by Unity")] private XRReferenceImageLibrary m_serializedLibrary;
    private ARTrackedImageManager m_imageTrackingManager;
    void Awake() {
        this.m_imageTrackingManager = gameObject.GetComponent<ARTrackedImageManager>();
    }

    void OnEnable() {
        PlayState.onPlayState += EnableImageTracking;
    }

    void OnDisable() {
        PlayState.onPlayState -= EnableImageTracking;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void EnableImageTracking() {
        this.m_imageTrackingManager.enabled = true;
        this.m_imageTrackingManager.referenceLibrary = this.m_serializedLibrary;
    }

    private void DisableImageTracking() {
        this.m_imageTrackingManager.enabled = false;
    }
}
