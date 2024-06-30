/**
    Author: Diro Baloska
    Collaborator: 
*/
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace StateMachine {

    public class KindergartenState : State
    {
        public override string StateName { get; } = "KindergartenState";
        private Transform m_transform;
        private GameObject m_kindergarten;
        public static GameObject m_selectionUI;
        private string m_anchorName;
        private AsyncOperationHandle<GameObject> m_instantiateHandler;
        public static AsyncOperationHandle<GameObject> m_selectionUIInstantiateHandler;
        private bool _shouldDestroySelectionUI;

        public KindergartenState(Transform transform, string anchor) {
            this.m_transform = transform;
            this.m_anchorName = anchor;
            this._shouldDestroySelectionUI = true;
        }

        public override void OnEnter()
        {
            Debug.Log("Entering: " + this.StateName);
            KindergartenState.m_selectionUIInstantiateHandler = Addressables.InstantiateAsync("UI_Selection.prefab", new Vector3(0,0,0), Quaternion.identity);
            this.m_instantiateHandler = Addressables.InstantiateAsync("Kindergarten.prefab", new Vector3(this.m_transform.position.x, this.m_transform.position.y - 0.5f, this.m_transform.position.z + 1), Quaternion.identity);
            this.Subscribe(); 
        }

        public override void OnExit()
        {
            Debug.Log("Exiting: " + this.StateName);
            this.Unsubscribe();
            GameObject.Destroy(this.m_kindergarten);
            Debug.Log("Should destroy UI: " + this._shouldDestroySelectionUI);
            if(this.m_instantiateHandler.IsValid()) Addressables.Release(this.m_instantiateHandler);
            if(_shouldDestroySelectionUI) {
                GameObject.Destroy(KindergartenState.m_selectionUI);
                Addressables.Release(KindergartenState.m_selectionUIInstantiateHandler);
            } 
        }

        public override void Subscribe()
        {
            m_instantiateHandler.Completed += AddressableSpawnCompleteHandler;
            m_selectionUIInstantiateHandler.Completed += SelectionSpawnCompleteHandler;
            NextPhaseButton.onNextPhase += NextState;
            PrevPhaseButton.onPrevPhase += PrevState;
        }

        public override void Unsubscribe()
        {
            m_selectionUIInstantiateHandler.Completed -= SelectionSpawnCompleteHandler;
            m_instantiateHandler.Completed -= AddressableSpawnCompleteHandler;
            NextPhaseButton.onNextPhase -= NextState;
            PrevPhaseButton.onPrevPhase -= PrevState;
        }

        private void AddressableSpawnCompleteHandler(AsyncOperationHandle<GameObject> handle)
        {
            Debug.Log("Handle complete instantiate");
            this.m_kindergarten = handle.Result;
            this.m_kindergarten.GetComponent<FollowAnchor>().SetAnchor(this.m_anchorName);
        }
        private void SelectionSpawnCompleteHandler(AsyncOperationHandle<GameObject> handle)
        {
            KindergartenState.m_selectionUI = handle.Result;
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
