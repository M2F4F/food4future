using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualMonitor : MonoBehaviour
{

    void Awake() {

    }

    void OnEnable() {
        // Subscribe to when algae simulation image is added
        ARCameraManager.onAlgaeImageTrackAdded += imageTrackAddedHandler;
    }

    void OnDisable() {
        ARCameraManager.onAlgaeImageTrackAdded -= imageTrackAddedHandler;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void imageTrackAddedHandler(Transform trackedTransform)
    {
        Debug.Log("Virtual Monitor Spawned");
        this.gameObject.SetActive(true);
        this.gameObject.transform.position = trackedTransform.position;
    }
}
