/**
    Author: Diro Baloska
    Collaborator: 
*/
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

public class MenuState : State
{
    public new readonly string stateName = "MenuState";

    private AsyncOperationHandle<GameObject> m_mainMenuInstantiateHandle;
    private GameObject m_mainMenu;
    
    public delegate void OnMenuState();
    public static event OnMenuState onMenuState;

    public delegate void OnMenuStateExited();
    public static event OnMenuStateExited onMenuStateExited;
    

    public override void OnEnter()
    {
        Debug.Log("Entering: " + this.stateName);
        onMenuState?.Invoke();
        this.m_mainMenuInstantiateHandle = Addressables.InstantiateAsync("UI_MainMenu.prefab");
        this.Subscribe();
    }


    public override void OnExit() {
        onMenuStateExited?.Invoke();
        GameObject.Destroy(this.m_mainMenu);
        this.Unsubscribe();
    }


    private void StartButtonHandler() {
        Debug.Log("Fired");
        GameStateManager.StateChange(new PlayState());
    }


    public override void Subscribe() {
        StartButton.onStartButton += StartButtonHandler;
        this.m_mainMenuInstantiateHandle.Completed += MainMenuInstantiateHandler;
    }


    public override void Unsubscribe() {
        StartButton.onStartButton -= StartButtonHandler;
        this.m_mainMenuInstantiateHandle.Completed -= MainMenuInstantiateHandler;
    }


    private void MainMenuInstantiateHandler(AsyncOperationHandle<GameObject> handle)
    {
        this.m_mainMenu = handle.Result;
    }
}
