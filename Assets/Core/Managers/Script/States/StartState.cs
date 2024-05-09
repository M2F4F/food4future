using UnityEngine;

public class StartState : State
{
    public override void OnEnter() {
        Debug.Log("Start Class");
    }

    public override void OnExit()
    {
        GameObject.Find("UI_MainMenu").SetActive(false);
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