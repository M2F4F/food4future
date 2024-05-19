/**
    Author: Diro Baloska
    Collaborator: 
*/
using UnityEngine;

public class InitState : State 
{
    public override void OnEnter() {
        this.Subscribe();
        Debug.Log("Init State");
    }

    public override void OnExit()
    {
        this.Unsubscribe();
        // throw new System.NotImplementedException();
    }

    public override void Subscribe()
    {
        // throw new System.NotImplementedException();
    }

    public override void Unsubscribe()
    {
        // throw new System.NotImplementedException();
    }
}