using UnityEngine;

namespace Components.StateMachine
{
    public class StateMachine
    {
        public State CurrentState { get; private set; }

        public void ChangeState(State newState)
        {
            Debug.Log($"Changing state from {CurrentState?.GetType().Name} to {newState.GetType().Name}");
            
            CurrentState?.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }
        
        public void Update() => CurrentState?.Update();
    }
}