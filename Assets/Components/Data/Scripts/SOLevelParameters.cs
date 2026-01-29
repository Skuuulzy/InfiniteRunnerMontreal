using System.Collections.Generic;
using UnityEngine;

namespace Components.Data
{
    [CreateAssetMenu(menuName = "Data/LevelParameters")]
    public class SOLevelParameters : ScriptableObject
    {
        [SerializeField] private int _playerLife = 3;
        [SerializeField] private float _speed;
        [SerializeField] private float _colorChunkTimeInterval = 20;
        [SerializeField] private List<Material> _chunkMaterials;
        [SerializeField] private int _maxColorSwapCount = 3;
        
        public int PlayerLife => _playerLife;
        public float Speed => _speed;
        public float ColorChunkTimeInterval => _colorChunkTimeInterval;
        public List<Material> ChunkMaterials => _chunkMaterials;
        public int MaxColorSwapCount => _maxColorSwapCount;
    }
}