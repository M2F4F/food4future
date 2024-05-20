/**
    Author: Diro Baloska
    Collaborator: 
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.AddressableAssets;
using System;
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
        this.Subscribe();
        this.m_instantiateHandler = Addressables.InstantiateAsync("Assets/Core/AlgaeSimulation/VirtualMonitor/FloatingMonitor.prefab", this.m_transform.position, Quaternion.identity);
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
        this.m_algaeSimulationObjects = handle.Result;
    }
}
