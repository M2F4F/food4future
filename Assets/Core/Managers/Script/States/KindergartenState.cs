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
    public override string StateName { get; } = "KindergartenState";
    private Transform m_transform;
    private GameObject m_kindergarten;
    private GameObject m_selectionUI;
    private string m_anchorName;
    private AsyncOperationHandle<GameObject> m_instantiateHandler;
    private AsyncOperationHandle<GameObject> m_selectionUIInstantiateHandler;

    public KindergartenState(Transform transform, string anchor) {
        this.m_transform = transform;
        this.m_anchorName = anchor;
    }

    public override void OnEnter()
    {
        Debug.Log("Entering: " + this.StateName);
        this.m_selectionUIInstantiateHandler = Addressables.InstantiateAsync("UI_Selection.prefab", new Vector3(0,0,0), Quaternion.identity);
        this.m_instantiateHandler = Addressables.InstantiateAsync("Kindergarten.prefab", new Vector3(this.m_transform.position.x, this.m_transform.position.y - 0.5f, this.m_transform.position.z + 1), Quaternion.identity);
        this.Subscribe(); 
    }

    public override void OnExit()
    {
        this.Unsubscribe();
        GameObject.Destroy(this.m_kindergarten);
        GameObject.Destroy(this.m_selectionUI);
        Addressables.Release(this.m_instantiateHandler);
        Addressables.Release(this.m_selectionUIInstantiateHandler);

    }

    public override void Subscribe()
    {
        m_instantiateHandler.Completed += AddressableSpawnCompleteHandler;
        m_selectionUIInstantiateHandler.Completed += SelectionSpawnCompleteHandler;
    }

    public override void Unsubscribe()
    {
        m_selectionUIInstantiateHandler.Completed -= SelectionSpawnCompleteHandler;
        m_instantiateHandler.Completed -= AddressableSpawnCompleteHandler;
    }

    private void AddressableSpawnCompleteHandler(AsyncOperationHandle<GameObject> handle)
    {
        Debug.Log("Handle complete instantiate");
        this.m_kindergarten = handle.Result;
        this.m_kindergarten.GetComponent<FollowAnchor>().SetAnchor(this.m_anchorName);
    }
    private void SelectionSpawnCompleteHandler(AsyncOperationHandle<GameObject> handle)
    {
        this.m_selectionUI = handle.Result;
    }
}
