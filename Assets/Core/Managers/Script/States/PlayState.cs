/**
    Author: Diro Baloska
    Collaborator: 
*/
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class PlayState : State {

    public override string StateName { get; } = "PlayState";

    public delegate void OnPlayState();
    public static event OnPlayState onPlayState;

    public delegate void OnPlayStateExit();
    public static event OnPlayStateExit onPlayStateExit;
    private AsyncOperationHandle<GameObject> _playUI;

    public override void OnEnter() {
        Debug.Log("Entering: " + this.StateName);
        this.Subscribe();
        onPlayState?.Invoke();
        _playUI = Addressables.InstantiateAsync("UI_Play.prefab", new Vector3(0,0,0), Quaternion.identity);
        // #if UNITY_EDITOR
        // GameObject gameobject = new GameObject("Anchor");
        // gameobject.transform.position = new Vector3(0,2.5f,0);
        // gameobject.transform.rotation = Quaternion.identity;
        // onImageTrackedHandler(gameobject.transform, "Anchor");
        // #endif
    }

    public override void OnExit() {
        Debug.Log("Exiting: " + this.StateName);
        this.Unsubscribe();
        onPlayStateExit?.Invoke();
    }

    public override void Subscribe()
    {
        ARCameraManager.onImageTracked += onImageTrackedHandler;
    }

    public override void Unsubscribe()
    {
        ARCameraManager.onImageTracked -= onImageTrackedHandler;
    }

    private void onImageTrackedHandler(Transform transform, string anchor)
    {
        GameStateManager.StateChange(new NarativeState(transform, anchor));
    }
}