using Components.GameEventSystem;
using UnityEngine;

public class ChunkController : MonoBehaviour
{
    [SerializeField] private Transform _endAnchor;
    [SerializeField] private GameObject _collectiblePrefab;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField, Range(0,99)] private int _spawnChance;
    [SerializeField] private MeshRenderer _meshRenderer;
    
    public Transform EndAnchor => _endAnchor;

    public bool IsBehind => _endAnchor.position.z <= 0;

    public void Start()
    {
        GameEventService.OnChunkColorUpdated += HandleChunkColorUpdated;
        HandleChunkColorUpdated(PersistantData.CurrentChunkMaterial);
        
        if (_spawnChance != 0) 
        {
            bool randomSpawnChance = Random.Range(0, 100) <= _spawnChance;
            if (randomSpawnChance)
            {
                Instantiate(_collectiblePrefab, _spawnPoint);
            }
        }
    }
    
    private void OnDestroy()
    {
        GameEventService.OnChunkColorUpdated -= HandleChunkColorUpdated;
    }
    
    private void HandleChunkColorUpdated(Material newMaterial)
    {
        if (!newMaterial)
        {
            return;
        }
        
        _meshRenderer.material = newMaterial;
    }
}