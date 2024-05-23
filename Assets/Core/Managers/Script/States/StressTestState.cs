/**
    Author: Diro Baloska
    Collaborator: 
*/
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


public class StressTestState : State
{
    public new readonly string stateName = "StressTestState";
    private Transform m_transform;
    private GameObject m_stressTest;
    private AsyncOperationHandle<GameObject> m_instantiateHandler;

    public StressTestState(Transform transform) {
        this.m_transform = transform;
    }

    public override void OnEnter()
    {
        Debug.Log("Entering: " + this.stateName);
        // TODO: Change Addressables address
        this.m_instantiateHandler = Addressables.InstantiateAsync("StressTest.prefab", new Vector3(this.m_transform.position.x, this.m_transform.position.y - 0.5f, this.m_transform.position.z + 1), Quaternion.identity);
        this.Subscribe();
    }

    public override void OnExit()
    {
        this.Unsubscribe();
        GameObject.Destroy(this.m_stressTest);
        Addressables.Release(this.m_instantiateHandler);
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
        this.m_stressTest = handle.Result;
    }
}
