/**
    Author: Diro Baloska
    Collaborator: 
*/
public class StartState : State
{
    public override string StateName { get; }= "StartState";
    public delegate void OnStartState();
    public static event OnStartState onStartState;
    public StartState() {
    }
    
    public override void OnEnter() {
        onStartState?.Invoke();
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