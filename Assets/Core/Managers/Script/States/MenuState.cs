/**
    Author: Diro Baloska
    Collaborator: 
*/
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuState : State
{
    public readonly String stateName = "Menu State";
    
    public delegate void OnMenuState();
    public static event OnMenuState onMenuState;

    public delegate void OnMenuStateExited();
    public static event OnMenuStateExited onMenuStateExited;
    

    public override void OnEnter()
    {
        Debug.Log("Entering: " + this.stateName);
        this.Subscribe();
    }


    public override void OnExit() {
        onMenuStateExited?.Invoke();
        SceneManager.LoadScene("TestScene", LoadSceneMode.Additive);
        this.Unsubscribe();
    }


    private void StartButtonHandler() {
        Debug.Log("Fired");
        GameStateManager.StateChange(new PlayState());
    }


    public override void Subscribe() {
        StartButton.onStartButton += StartButtonHandler;
    }


    public override void Unsubscribe() {
        StartButton.onStartButton -= StartButtonHandler;
    }
}
