/**
    Author: Diro Baloska
    Collaborator: 
*/
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


public class KindergartenState : State
{
    public new readonly string stateName = "KindergartenState";
    private Transform m_transform;
    private GameObject m_kindergarten;
    private AsyncOperationHandle<GameObject> m_instantiateHandler;

    public KindergartenState(Transform transform) {
        this.m_transform = transform;
    }

    public override void OnEnter()
    {
        Debug.Log("Entering: " + this.stateName);
        this.m_instantiateHandler = Addressables.InstantiateAsync("Kindergarten.prefab", new Vector3(this.m_transform.position.x, this.m_transform.position.y - 0.5f, this.m_transform.position.z + 1), Quaternion.identity);
        this.Subscribe();
    }

    public override void OnExit()
    {
        this.Unsubscribe();
        GameObject.Destroy(this.m_kindergarten);
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
        this.m_kindergarten = handle.Result;
    }
}
