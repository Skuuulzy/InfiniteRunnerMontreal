using UnityEngine;
using Component.SceneLoader;

public class UIGameOverController : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverPanel;
    
    private void Start()
    {
        GameEventService.OnGameOverState += HandleGameOver;
    }
    
    private void OnDestroy()
    {
        GameEventService.OnGameOverState -= HandleGameOver;
    }

    private void HandleGameOver(bool enterState)
    {
        _gameOverPanel.SetActive(enterState);
    }

    public void BackToMainMenu()
    {
        SceneLoaderService.LoadMainMenu();
    }
}