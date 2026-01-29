using System;
using Components.Data;
using Components.GameEventSystem;
using UnityEngine;

namespace Components.StateMachine
{
    public class GameState : State
    {
        private int _currentLife;
        private float _timer;
        private int _colorSwapCount;

        public GameState(StateMachine stateMachine, SOLevelParameters levelParameters) : base(stateMachine, levelParameters)
        {
        }

        public override void Enter()
        {
            GameEventService.OnGameState?.Invoke(true);
            GameEventService.OnCollision += HandleCollision;
            GameEventService.OnCollectiblePicked += HandleCollectiblePicked;
            
            _currentLife = LevelParameters.PlayerLife;
            _timer = 0;
        }

        public override void Update()
        {
            // If color swap count is reached, stop updating colors of chunks.
            if (_colorSwapCount >= LevelParameters.MaxColorSwapCount)
            {
                return;
            }
            
            _timer += Time.deltaTime;
            if (_timer >= LevelParameters.ColorChunkTimeInterval)
            {
                var material = LevelParameters.ChunkMaterials[_colorSwapCount];
                GameEventService.OnChunkColorUpdated?.Invoke(material);
                PersistantData.CurrentChunkMaterial = material;
                
                _colorSwapCount++;
                _timer = 0;
            }
        }

        public override void Exit()
        {
            GameEventService.OnGameState?.Invoke(false);
            GameEventService.OnCollision -= HandleCollision;
            GameEventService.OnCollectiblePicked -= HandleCollectiblePicked;
        }
        
        private void HandleCollision()
        {
            _currentLife--;
            Debug.Log($"New life: {_currentLife}");
            GameEventService.OnPlayerLifeUpdated?.Invoke(_currentLife);
            
            if (_currentLife <= 0)
            {
                StateMachine.ChangeState(new GameOverState(StateMachine, LevelParameters));
            }
        }
        
        private void HandleCollectiblePicked()
        {
            // Cannot exceed maximum life for the level.
            if (_currentLife == LevelParameters.PlayerLife)
            {
                return;
            }
            
            _currentLife++;
            GameEventService.OnPlayerLifeUpdated?.Invoke(_currentLife);
        }
    }
}