using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuState : State
{
    public readonly String stateName = "Menu State";
    public override void OnEnter()
    {
        Debug.Log("Entering: " + this.stateName);
        this.Subscribe();
    }

    public override void OnExit() {
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
