/**
    Author: Diro Baloska
    Collaborator: 
*/

namespace StateMachine {
    public abstract class State
    {
        public virtual string StateName { get; } = "State";

        public abstract void OnEnter();

        public abstract void OnExit();

        public abstract void Subscribe();
        public abstract void Unsubscribe();
    }
}
