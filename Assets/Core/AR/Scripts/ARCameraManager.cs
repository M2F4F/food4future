using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARCameraManager : MonoBehaviour
{
    [SerializeField] [Tooltip("Image Library to be detected by Unity")] private XRReferenceImageLibrary m_serializedLibrary;
    private ARTrackedImageManager m_imageTrackingManager;

    public delegate void OnImageTrackAdded(Transform transform);
    public static event OnImageTrackAdded onAlgaeImageTrackAdded;

    void Awake() {
        this.m_imageTrackingManager = gameObject.GetComponent<ARTrackedImageManager>();
    }

    void OnEnable() {
        PlayState.onPlayState += EnableImageTracking;
        m_imageTrackingManager.trackedImagesChanged += TrackedImageHandler;
    }

    

    void OnDisable() {
        PlayState.onPlayState -= EnableImageTracking;
        m_imageTrackingManager.trackedImagesChanged -= TrackedImageHandler;
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

    private void TrackedImageHandler(ARTrackedImagesChangedEventArgs args)
    {
        foreach(var image in args.added) {
            Debug.Log("ARCameraManager: TrackedImageHandler() added: " + image.name);
            if(image.referenceImage.name == "AlgaeSimulation") onAlgaeImageTrackAdded?.Invoke(image.transform);
        }
        foreach(var image in args.updated) {
            
        }
        foreach(var image in args.removed) {
            Debug.Log("ARCameraManager: TrackedImageHandler() removed: " + image.name);
        }
    }
}
