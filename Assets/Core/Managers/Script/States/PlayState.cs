using System;
using UnityEngine;

public class PlayState : State {

    public readonly String stateName = "Play State";
    public override void OnEnter() {
        Debug.Log("Entering: " + this.stateName);
    }

    public override void OnExit() {
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