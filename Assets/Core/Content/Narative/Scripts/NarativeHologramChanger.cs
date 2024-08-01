using System;
using System.Collections;
using System.Collections.Generic;
using StateMachine;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class NarativeHologramChanger : MonoBehaviour
{
    [SerializeField, Tooltip("Page number up to where the hologram should be shown")] private int _pageCoverage;
    [SerializeField, Tooltip("Addressables' address of the target prefab")] private string _nextHologram;
    NarativeState narativeState;
    private FollowAnchor _followAnchor;
    private NarativeSlideshow _narativeSlideShow;
    private AsyncOperationHandle<GameObject> asyncOperationHandle;

    void Awake() {
        StartCoroutine(AccessNarative());
        narativeState = (NarativeState) GameStateManager._gameStateManager.state;
        _followAnchor = gameObject.GetComponent<FollowAnchor>();
    }

    void OnEnable() {
        NarativeSlideshow.onPageChange += NextPageHandler;
        Reset_Button.onGameReset += ResetNarativeHandler;
    }


    void OnDisable() {
        NarativeSlideshow.onPageChange -= NextPageHandler;
        Reset_Button.onGameReset -= ResetNarativeHandler;
        // asyncOperationHandle.Completed -= NextHologramSpawnHandler;

    }
    
    private void ResetNarativeHandler() {
        asyncOperationHandle = Addressables.InstantiateAsync("P_Narative0.prefab", this.gameObject.transform.position, Quaternion.identity);
        asyncOperationHandle.Completed += NextHologramSpawnHandler;
    }

    IEnumerator AccessNarative() {
        GameObject narativeGameObject;
        do {
            narativeGameObject = GameObject.Find("Text_Narative");
            yield return new WaitForEndOfFrame();
        } while(narativeGameObject == null);

        _narativeSlideShow = narativeGameObject.GetComponent<NarativeSlideshow>();
        yield return null;
    }


    private void NextPageHandler(int pageNumber)
    {
        if(pageNumber == _pageCoverage + 1) {
            asyncOperationHandle = Addressables.InstantiateAsync(_nextHologram, this.gameObject.transform.position, Quaternion.identity);
            asyncOperationHandle.Completed += NextHologramSpawnHandler;
        }
    }

    private void NextHologramSpawnHandler(AsyncOperationHandle<GameObject> handle) {
        narativeState.m_narative = handle.Result;
        handle.Result.GetComponent<FollowAnchor>().SetAnchor(this.gameObject.GetComponent<FollowAnchor>().anchor.name);
        Destroy(this.gameObject);
    }

}
