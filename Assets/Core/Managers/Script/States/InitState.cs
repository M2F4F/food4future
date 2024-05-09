using UnityEngine;

public class InitState : State 
{
    public override void OnEnter() {
        Debug.Log("Init State");
    }

    public override void OnExit()
    {
        throw new System.NotImplementedException();
    }

    public override void Subscribe()
    {
        throw new System.NotImplementedException();
    }

    public override void Unsubscribe()
    {
        throw new System.NotImplementedException();
    }
}