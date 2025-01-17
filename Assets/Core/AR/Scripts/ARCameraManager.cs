/**
* Collaborator:
* - Diro Baloska
**/
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using StateMachine;

public class ARCameraManager : MonoBehaviour
{
    [SerializeField] [Tooltip("Image Library to be detected by Unity")] private XRReferenceImageLibrary m_serializedLibrary;
    [SerializeField] [Tooltip("Prefab to be spawned when image is detected")] private GameObject m_anchor;
    private ARTrackedImageManager m_imageTrackingManager;

    public delegate void OnImageTrackAdded(Transform transform, string anchor);
    public static event OnImageTrackAdded onImageTracked;
    private GameStateManager _gameStateManager;

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
        GameObject gameStateManager = GameObject.Find("GameStateManager");
        this._gameStateManager = GameStateManager._gameStateManager;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void EnableImageTracking() {
        this.m_imageTrackingManager.enabled = true;
        this.m_imageTrackingManager.referenceLibrary = this.m_serializedLibrary;
        this.m_imageTrackingManager.trackedImagePrefab = this.m_anchor;
    }

    private void DisableImageTracking() {
        this.m_imageTrackingManager.enabled = false;
    }

    private void TrackedImageHandler(ARTrackedImagesChangedEventArgs args)
    {
        foreach(var image in args.added) {
            if(_gameStateManager.state.StateName != "PlayState") return;
            onImageTracked?.Invoke(image.transform, image.name);
            
            // Debug.Log("ARCameraManager: TrackedImageHandler() added: " + image.name);
            // switch(image.referenceImage.name) {
            //     case "Kindergarten":
            //         onKindergartenImageTrackAdded?.Invoke(image.transform, image.name);
            //         break;
            //     case "StressTest":
            //         onStressTestImageTrackAdded?.Invoke(image.transform, image.name);
            //         break;
            //     case "ProductionTest":
            //         onProductionTestImageTrackAdded?.Invoke(image.transform, image.name);
            //         break;
            //     default: break;
            // }
        }

        foreach(var image in args.updated) {
            
        }

        foreach(var image in args.removed) {
            Debug.Log("ARCameraManager: TrackedImageHandler() removed: " + image.name);
        }
    }
}
