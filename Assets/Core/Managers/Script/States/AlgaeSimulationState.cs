/**
    Author: Diro Baloska
    Collaborator: 
*/
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


public class AlgaeSimulationState : State
{
    public new readonly string stateName = "AlgaeSimulationState";
    private Transform m_transform;
    private GameObject m_algaeSimulationObjects;
    private AsyncOperationHandle<GameObject> m_instantiateHandler;

    public AlgaeSimulationState(Transform transform) {
        this.m_transform = transform;
    }

    public override void OnEnter()
    {
        Debug.Log("Entering Algae Simulation State");
        this.m_instantiateHandler = Addressables.InstantiateAsync("Assets/Core/AlgaeSimulation/VirtualMonitor/FloatingMonitor.prefab", this.m_transform.position, Quaternion.identity);
        this.Subscribe();
    }

    public override void OnExit()
    {
        this.Unsubscribe();
    }

    public override void Subscribe()
    {
        m_instantiateHandler.Completed += AddressableSpawnCompleteHandler;
    }

    public override void Unsubscribe()
    {
        m_instantiateHandler.Completed -= AddressableSpawnCompleteHandler;
    }

    private void AddressableSpawnCompleteHandler(AsyncOperationHandle<GameObject> handle)
    {
        Debug.Log("Virtual Monitor Instantiated");
        this.m_algaeSimulationObjects = handle.Result;
    }
}
