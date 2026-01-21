using Components.SODB;
using UnityEngine;

namespace Components.StateMachine
{
    public class StateMachineController : MonoBehaviour
    {
        private StateMachine _stateMachine;
        
        private void Start()
        {
            int levelIndex = 1;
            if (SaveService.TryLoad(out SaveData saveData))
            {
                levelIndex = saveData.LevelIndex;
            }
            
            var parameters = ScriptableObjectDataBase.GetByName("Level" + levelIndex);
            
            _stateMachine = new StateMachine();
            var initialState = new CountdownState(_stateMachine, parameters);
            
            _stateMachine.ChangeState(initialState);
        }
        
        public void Update() => _stateMachine.Update();
    }
}