using Components.Data;
using UnityEngine;

namespace Components.StateMachine
{
    public class CountdownState : State
    {
        private float _countdownTimer;


        public CountdownState(StateMachine stateMachine, SOLevelParameters levelParameters) : base(stateMachine, levelParameters)
        {
        }

        public override void Enter()
        {
            GameEventService.OnCountdownState?.Invoke(true);
            _countdownTimer = 3;
        }

        public override void Update()
        {
            _countdownTimer -= Time.deltaTime;
            if (_countdownTimer > 0)
            {
                GameEventService.OnCountdownTick?.Invoke(_countdownTimer);
                return;
            }
            
            StateMachine.ChangeState(new GameState(StateMachine, LevelParameters));
        }

        public override void Exit()
        {
            GameEventService.OnCountdownState?.Invoke(false);
        }
    }
}