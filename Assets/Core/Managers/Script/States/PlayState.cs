/**
    Author: Diro Baloska
    Collaborator: 
*/
using UnityEngine;

public class PlayState : State {

    public override string StateName { get; } = "PlayState";

    public delegate void OnPlayState();
    public static event OnPlayState onPlayState;

    public delegate void OnPlayStateExit();
    public static event OnPlayStateExit onPlayStateExit;

    public override void OnEnter() {
        Debug.Log("Entering: " + this.StateName);
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
        Debug.Log("Exiting: " + this.StateName);
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