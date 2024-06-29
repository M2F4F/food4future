/**
    Author: Diro Baloska
    Collaborator: 
*/
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using StateMachine;

namespace StateMachine {
    public class ProductionTestState : State
    {
        public override string StateName { get; }= "ProductionTestState";
        private Transform m_transform;
        private GameObject m_production;
        private string m_anchorName;
        private AsyncOperationHandle<GameObject> m_instantiateHandler;
        private bool _shouldDestroySelectionUI;

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
            if(_shouldDestroySelectionUI) {
                GameObject.Destroy(KindergartenState.m_selectionUI);
                Addressables.Release(KindergartenState.m_selectionUIInstantiateHandler);
            } 
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

        private void NextState() {
            this._shouldDestroySelectionUI = false;
            GameStateManager.StateChange(new StressTestState(m_transform, m_anchorName));
        }
        private void PrevState() {
            this._shouldDestroySelectionUI = false;
            GameStateManager.StateChange(new ProductionTestState(m_transform, m_anchorName));
        }
    }
}
