using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace StateMachine {
    public class InitSelectionScreenState : State
    {
        public override string StateName {get;} = "InitSelectionScreenState"; 
        private GameObject m_selectionUI;
        private AsyncOperationHandle<GameObject> m_instantiateOperation;

        public override void OnEnter()
        {
            Debug.Log("Entering: " + StateName);
            m_instantiateOperation = Addressables.InstantiateAsync("UI_Selection.prefab", new Vector3(0,0,0), Quaternion.identity);
            this.Subscribe();  
        }

        public override void OnExit()
        {
            Debug.Log("Exiting: " + StateName);
            Addressables.Release(m_instantiateOperation);
            this.Unsubscribe();  
        }

        public override void Subscribe()
        {
            m_instantiateOperation.Completed += InstantiateCompletedHandler;
        }

        public override void Unsubscribe()
        {
            m_instantiateOperation.Completed -= InstantiateCompletedHandler;
        }

        private void InstantiateCompletedHandler(AsyncOperationHandle<GameObject> handle)
        {
            m_selectionUI = handle.Result;
            // GameStateManager.StateChange(new )
        }
    }
}