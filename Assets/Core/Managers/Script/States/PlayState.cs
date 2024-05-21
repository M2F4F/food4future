/**
    Author: Diro Baloska
    Collaborator: 
*/
using UnityEngine;

public class PlayState : State {

    public new readonly string stateName = "PlayState";

    public delegate void OnPlayState();
    public static event OnPlayState onPlayState;

    public delegate void OnPlayStateExit();
    public static event OnPlayStateExit onPlayStateExit;

    public override void OnEnter() {
        Debug.Log("Entering: " + this.stateName);
        this.Subscribe();
        onPlayState?.Invoke();

        #if UNITY_EDITOR
        GameObject gameobject = new GameObject("anchor");
        gameobject.transform.position = new Vector3(0,2,0);
        gameobject.transform.rotation = Quaternion.identity;
        AlgaeSimulationTrackedHandler(gameobject.transform);
        #endif
    }

    public override void OnExit() {
        Debug.Log("Exiting: " + this.stateName);
        this.Unsubscribe();
        onPlayStateExit?.Invoke();
    }

    public override void Subscribe()
    {
        ARCameraManager.onAlgaeImageTrackAdded += AlgaeSimulationTrackedHandler;

        // throw new NotImplementedException();
    }

    public override void Unsubscribe()
    {
        ARCameraManager.onAlgaeImageTrackAdded -= AlgaeSimulationTrackedHandler;
        // throw new NotImplementedException();
    }

    private void AlgaeSimulationTrackedHandler(Transform transform)
    {
        GameStateManager.StateChange(new AlgaeSimulationState(transform));
    }
}