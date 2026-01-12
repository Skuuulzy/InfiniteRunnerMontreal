using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}