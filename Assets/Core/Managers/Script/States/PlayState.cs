/**
    Author: Diro Baloska
    Collaborator: 
*/
using System;
using UnityEngine;

public class PlayState : State {

    public readonly String stateName = "Play State";

    public delegate void OnPlayState();
    public static event OnPlayState onPlayState;

    public delegate void OnPlayStateExit();
    public static event OnPlayStateExit onPlayStateExit;

    public override void OnEnter() {
        Debug.Log("Entering: " + this.stateName);
        this.Subscribe();
        onPlayState?.Invoke();
    }

    public override void OnExit() {
        this.Unsubscribe();
        onPlayStateExit?.Invoke();
    }

    public override void Subscribe()
    {
        throw new NotImplementedException();
    }

    public override void Unsubscribe()
    {
        throw new NotImplementedException();
    }
}