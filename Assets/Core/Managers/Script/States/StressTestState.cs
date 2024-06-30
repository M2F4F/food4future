/**
    Author: Diro Baloska
    Collaborator: 
*/
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace StateMachine {
    public class StressTestState : State
    {
        public override string StateName { get; } = "StressTestState";
        private Transform m_transform;
        private GameObject m_stressTest;
        private string m_anchorName;
        private AsyncOperationHandle<GameObject> m_instantiateHandler;
        private bool _shouldDestroySelectionUI;
        private bool _isFinishLoaded;
        public StressTestState(Transform transform, string anchor) {
            this.m_transform = transform;
            this.m_anchorName = anchor;
            _shouldDestroySelectionUI = true;
            _isFinishLoaded = false;
        }

        public override void OnEnter()
        {
            Debug.Log("Entering: " + this.StateName);
            // this.m_instantiateHandler = Addressables.InstantiateAsync("PolybiomStage.prefab", new Vector3(this.m_transform.position.x, this.m_transform.position.y - 1f, this.m_transform.position.z), Quaternion.identity);
            this.Subscribe();
        }

        public override void OnExit()
        {
            Debug.Log("Exiting: " + this.StateName);
            this.Unsubscribe();
            // if(this.m_stressTest != null) GameObject.Destroy(this.m_stressTest);
            // if(this.m_instantiateHandler.IsValid()) Addressables.Release(this.m_instantiateHandler);
            if(_shouldDestroySelectionUI) {
                Debug.Log("Destroying");
                GameObject.Destroy(KindergartenState.m_selectionUI);
                Addressables.Release(KindergartenState.m_selectionUIInstantiateHandler);
            } 
        }

        public override void Subscribe()
        {
            // m_instantiateHandler.Completed += AddressableSpawnCompleteHandler;
            NextPhaseButton.onNextPhase += NextState;
            PrevPhaseButton.onPrevPhase += PrevState;
        }

        public override void Unsubscribe()
        {
            // m_instantiateHandler.Completed -= AddressableSpawnCompleteHandler;
            NextPhaseButton.onNextPhase -= NextState;
            PrevPhaseButton.onPrevPhase -= PrevState;
        }

        private void AddressableSpawnCompleteHandler(AsyncOperationHandle<GameObject> handle)
        {
            Debug.Log("Virtual Monitor Instantiated");
            this.m_stressTest = handle.Result;
        }
        private void NextState() {
            _shouldDestroySelectionUI = false;
            GameStateManager.StateChange(new ProductionTestState(m_transform, m_anchorName));
        }
        private void PrevState() {
            _shouldDestroySelectionUI = false;
            GameStateManager.StateChange(new KindergartenState(m_transform, m_anchorName));
        }
    }

}
