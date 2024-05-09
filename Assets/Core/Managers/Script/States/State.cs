using UnityEngine;

public abstract class State
{
    public abstract void OnEnter();

    public abstract void OnExit();

    public abstract void Subscribe();
    public abstract void Unsubscribe();
}
