using UnityEngine;

namespace Components.StateMachine
{
    public class GameState : State
    {
        private int _life = 3;
        
        public GameState(StateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            GameEventService.OnGameState?.Invoke(true);
            GameEventService.OnCollision += HandleCollision;
            GameEventService.OnPlayerLifeUpdated?.Invoke(_life);
        }

        public override void Update()
        {
            
        }

        public override void Exit()
        {
            GameEventService.OnGameState?.Invoke(false);
            GameEventService.OnCollision -= HandleCollision;
        }
        
        private void HandleCollision()
        {
            _life--;
            Debug.Log($"New life: {_life}");
            GameEventService.OnPlayerLifeUpdated?.Invoke(_life);
            
            if (_life <= 0)
            {
                StateMachine.ChangeState(new GameOverState(StateMachine));
            }
        }
    }
}