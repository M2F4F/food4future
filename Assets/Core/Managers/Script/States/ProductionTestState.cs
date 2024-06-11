/**
    Author: Diro Baloska
    Collaborator: 
*/
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


public class ProductionTestState : State
{
    public override string StateName { get; }= "ProductionTestState";
    private Transform m_transform;
    private GameObject m_production;
    private string m_anchorName;
    private AsyncOperationHandle<GameObject> m_instantiateHandler;

    public ProductionTestState(Transform transform, string anchor) {
        this.m_transform = transform;
        this.m_anchorName = anchor;
    }

    public override void OnEnter()
    {
        Debug.Log("Entering: " + this.StateName);
        // TODO: Change Addressables address
        this.m_instantiateHandler = Addressables.InstantiateAsync("Production.prefab", new Vector3(this.m_transform.position.x, this.m_transform.position.y - 0.5f, this.m_transform.position.z + 1), Quaternion.identity);
        this.Subscribe();
    }

    public override void OnExit()
    {
        this.Unsubscribe();
        GameObject.Destroy(this.m_production);
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
        this.m_production = handle.Result;
    }
}
