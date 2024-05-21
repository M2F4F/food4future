/**
    Author: Diro Baloska
    Collaborator: 
*/
using UnityEngine;
[DefaultExecutionOrder(-1)]
public class GameStateManager : MonoBehaviour
{
    public static State state;
    
    void OnEnable() {
        state = new InitState();
        state.OnEnter();
    }
    // Start is called before the first frame update
    void Start()
    {
        StateChange(new MenuState());
    }

    void Update() {
        
    }

    public static void StateChange(State newState) {
        state.OnExit();
        state = newState;
        state.OnEnter();
    }
}
