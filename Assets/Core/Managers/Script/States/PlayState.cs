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
        GameObject gameobject = new GameObject("Anchor");
        gameobject.transform.position = new Vector3(0,2,0);
        gameobject.transform.rotation = Quaternion.identity;
        AlgaeSimulationTrackedHandler(gameobject.transform, "Anchor");
        #endif
    }

    public override void OnExit() {
        Debug.Log("Exiting: " + this.stateName);
        this.Unsubscribe();
        onPlayStateExit?.Invoke();
    }

    public override void Subscribe()
    {
        ARCameraManager.onKindergartenImageTrackAdded += AlgaeSimulationTrackedHandler;
        ARCameraManager.onStressTestImageTrackAdded += StressTestTrackedHandler;
        ARCameraManager.onProductionTestImageTrackAdded += ProductionTestTrackedHandler;
    }

    public override void Unsubscribe()
    {
        ARCameraManager.onKindergartenImageTrackAdded -= AlgaeSimulationTrackedHandler;
        ARCameraManager.onStressTestImageTrackAdded -= StressTestTrackedHandler;
        ARCameraManager.onProductionTestImageTrackAdded -= ProductionTestTrackedHandler;
    }

    private void AlgaeSimulationTrackedHandler(Transform transform, string anchor)
    {
        GameStateManager.StateChange(new KindergartenState(transform, anchor));
    }

    private void StressTestTrackedHandler(Transform transform, string anchor)
    {
        GameStateManager.StateChange(new StressTestState(transform, anchor));
    }

    private void ProductionTestTrackedHandler(Transform transform, string anchor)
    {
        GameStateManager.StateChange(new ProductionTestState(transform, anchor));
    }
}