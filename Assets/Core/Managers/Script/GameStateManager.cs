/**
    Author: Diro Baloska
    Collaborator: 
*/
using UnityEngine;
namespace StateMachine{

    [DefaultExecutionOrder(-1)]
    public class GameStateManager : MonoBehaviour
    {
        public static GameStateManager _gameStateManager {get; private set;}
        public State state;
        public delegate void OnGameStateChange(string stateName);
        public static event OnGameStateChange onGameStateChange;
        
        void OnEnable() {
            state = new InitState();
            state.OnEnter();
        }
        // Start is called before the first frame update
        void Start()
        {
            _gameStateManager = this;
            StateChange(new MenuState());
        }

        void Update() {
            
        }
    
        public static void StateChange(State newState) {
            _gameStateManager.state.OnExit();
            _gameStateManager.state = newState;
            _gameStateManager.state.OnEnter();
            onGameStateChange?.Invoke(_gameStateManager.state.StateName);
        }
    }
}
