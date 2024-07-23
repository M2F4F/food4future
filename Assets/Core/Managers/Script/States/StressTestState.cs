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
        public string m_anchorName;
        private AsyncOperationHandle<GameObject> m_instantiateHandler;
        private bool _shouldDestroySelectionUI;
        
        public StressTestState(Transform transform, string anchor) {
            this.m_transform = transform;
            this.m_anchorName = anchor;
            _shouldDestroySelectionUI = true;
        }

        public override void OnEnter()
        {
            Debug.Log("Entering: " + this.StateName);
            this.m_instantiateHandler = Addressables.InstantiateAsync("StressTest.prefab", new Vector3(this.m_transform.position.x, this.m_transform.position.y - 1f, this.m_transform.position.z), Quaternion.identity);
            this.Subscribe();
        }

        public override void OnExit()
        {
            this.Unsubscribe();
            GameObject.Destroy(this.m_stressTest);
            Addressables.Release(this.m_instantiateHandler);
            if(_shouldDestroySelectionUI) {
                GameObject.Destroy(KindergartenState.m_selectionUI);
                Addressables.Release(KindergartenState.m_selectionUIInstantiateHandler);
            } 
        }

        public override void Subscribe()
        {
            m_instantiateHandler.Completed += AddressableSpawnCompleteHandler;
            NextPhaseButton.onNextPhase += NextState;
            PrevPhaseButton.onPrevPhase += PrevState;
        }

        public override void Unsubscribe()
        {
            m_instantiateHandler.Completed -= AddressableSpawnCompleteHandler;
            NextPhaseButton.onNextPhase -= NextState;
            PrevPhaseButton.onPrevPhase -= PrevState;
        }

        private void AddressableSpawnCompleteHandler(AsyncOperationHandle<GameObject> handle)
        {
            Debug.Log("Virtual Monitor Instantiated");
            this.m_stressTest = handle.Result;
            this.m_stressTest.GetComponent<FollowAnchor>().SetAnchor(this.m_anchorName);
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
