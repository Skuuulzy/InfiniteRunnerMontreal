using UnityEngine;
using Component.SceneLoader;

public class UIGameOverController : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverPanel;
    
    private void Start()
    {
        GameEventService.OnGameOver += HandleGameOver;
    }
    
    private void OnDestroy()
    {
        GameEventService.OnGameOver -= HandleGameOver;
    }

    private void HandleGameOver()
    {
        _gameOverPanel.SetActive(true);
    }

    public void BackToMainMenu()
    {
        SceneLoaderService.LoadMainMenu();
    }
}