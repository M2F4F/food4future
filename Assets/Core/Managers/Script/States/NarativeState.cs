/**
    Author: Diro Baloska
    Collaborator: 
*/
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


public class NarativeState : State
{
    public override string StateName { get; } = "NarativeState";
    private Transform m_transform;
    private GameObject m_uinarative;
    private string m_anchorName;
    private AsyncOperationHandle<GameObject> m_uiInstantiateHandler;
    private AsyncOperationHandle<GameObject> m_instantiateHandler;

    public NarativeState(Transform transform, string anchor) {
        this.m_transform = transform;
        this.m_anchorName = anchor;
    }

    public override void OnEnter()
    {
        Debug.Log("Entering: " + this.StateName);
        this.m_uiInstantiateHandler = Addressables.InstantiateAsync("UI_Narative.prefab", new Vector3(0,0,0), Quaternion.identity);
        this.m_instantiateHandler = Addressables.InstantiateAsync("Narative.prefab", new Vector3(this.m_transform.position.x, this.m_transform.position.y - 0.5f, this.m_transform.position.z + 1), Quaternion.identity);
        this.Subscribe();
    }

    public override void OnExit()
    {
        this.Unsubscribe();
        GameObject.Destroy(this.m_uinarative);
        Addressables.Release(this.m_uiInstantiateHandler);
        Addressables.Release(this.m_uiInstantiateHandler);

    }

    public override void Subscribe()
    {
        m_uiInstantiateHandler.Completed += UIAddressableSpawnCompleteHandler;
        m_instantiateHandler.Completed += AddressableSpawnCompleteHandler;
    }

    public override void Unsubscribe()
    {
        m_uiInstantiateHandler.Completed -= UIAddressableSpawnCompleteHandler;
        m_instantiateHandler.Completed -= AddressableSpawnCompleteHandler;
    }

    private void AddressableSpawnCompleteHandler(AsyncOperationHandle<GameObject> handle)
    {
        this.m_uinarative = handle.Result;
        this.m_uinarative.GetComponent<FollowAnchor>().SetAnchor(this.m_anchorName);
    }

    private void UIAddressableSpawnCompleteHandler(AsyncOperationHandle<GameObject> handle)
    {
        Debug.Log("Handle complete instantiate");
        this.m_uinarative = handle.Result;
    }
}
