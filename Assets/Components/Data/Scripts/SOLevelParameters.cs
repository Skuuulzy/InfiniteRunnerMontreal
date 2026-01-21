using UnityEngine;

namespace Components.Data
{
    [CreateAssetMenu(menuName = "Data/LevelParameters")]
    public class SOLevelParameters : ScriptableObject
    {
        [SerializeField] private int _playerLife = 3;
        [SerializeField] private float _speed;
        
        public int PlayerLife => _playerLife;
        public float Speed => _speed;
    }
}