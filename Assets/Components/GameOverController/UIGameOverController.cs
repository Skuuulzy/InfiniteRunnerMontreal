using UnityEngine;

public class UIGameOverController : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverPanel;
    
    private void Start()
    {
        GameEventSystem.OnGameOver += HandleGameOver;
    }
    
    private void OnDestroy()
    {
        GameEventSystem.OnGameOver -= HandleGameOver;
    }

    private void HandleGameOver()
    {
        _gameOverPanel.SetActive(true);
    }
}
