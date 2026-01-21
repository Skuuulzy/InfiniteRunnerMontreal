using Components.Data;

namespace Components.StateMachine
{
    public abstract class State
    {
        protected readonly StateMachine StateMachine;
        protected readonly SOLevelParameters LevelParameters;
        
        protected State(StateMachine stateMachine, SOLevelParameters levelParameters)
        {
            StateMachine = stateMachine;
            LevelParameters = levelParameters;
        }
        
        public abstract void Enter();
        public abstract void Update();
        public abstract void Exit();
    }
}