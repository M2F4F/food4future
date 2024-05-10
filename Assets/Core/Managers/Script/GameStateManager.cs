/**
    Author: Diro Baloska
    Collaborator: 
*/
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static State state;
    // Start is called before the first frame update
    void Start()
    {
        state = new MenuState();
        state.OnEnter();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void StateChange(State newState) {
        state.OnExit();
        state = newState;
        state.OnEnter();
    }
}
