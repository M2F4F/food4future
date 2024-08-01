/**
    Author: Diro Baloska
    Collaborator: 
*/
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace StateMachine {
    public class PlayState : State {

        public override string StateName { get; } = "PlayState";

        public delegate void OnPlayState();
        public static event OnPlayState onPlayState;

        public delegate void OnPlayStateExit();
        public static event OnPlayStateExit onPlayStateExit;
        private AsyncOperationHandle<GameObject> _playUiInstantiator;
        private GameObject _playUI;

        public override void OnEnter() {
            Debug.Log("Entering: " + this.StateName);
            _playUiInstantiator = Addressables.InstantiateAsync("UI_Play.prefab", new Vector3(0,0,0), Quaternion.identity);
            this.Subscribe();
            onPlayState?.Invoke();
        }

        public override void OnExit() {
            Debug.Log("Exiting: " + this.StateName);
            this.Unsubscribe();
            onPlayStateExit?.Invoke();
            GameObject.Destroy(_playUI);
        }

        public override void Subscribe()
        {
            ARCameraManager.onImageTracked += onImageTrackedHandler;
            _playUiInstantiator.Completed += SuccessfulInstantiateHandler;
        }

        public override void Unsubscribe()
        {
            ARCameraManager.onImageTracked -= onImageTrackedHandler;
            _playUiInstantiator.Completed -= SuccessfulInstantiateHandler;
        }

        private void onImageTrackedHandler(Transform transform, string anchor)
        {
            #if UNITY_EDITOR
            GameStateManager.StateChange(new NarativeState(transform, anchor));
            #else
            GameStateManager.StateChange(new NarativeState(transform, anchor));
            #endif
        }

        private void SuccessfulInstantiateHandler(AsyncOperationHandle<GameObject> handle)
        {
            _playUI = handle.Result;
            
            #if UNITY_EDITOR
            // GameObject gameobject = new GameObject("Anchor");
            // gameobject.AddComponent<ARAnchor>();
            // gameobject.transform.position = new Vector3(1f,2.3f,-1.35f);
            // gameobject.transform.rotation = Quaternion.identity;
            // onImageTrackedHandler(gameobject.transform, "Anchor");
            #endif
        }
    }
}