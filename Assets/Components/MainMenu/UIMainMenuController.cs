using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMainMenuController : MonoBehaviour
{
    public void PlayGame()
    {
        // Load scenes
        SceneManager.LoadScene("Level", LoadSceneMode.Single);
        SceneManager.LoadScene("LevelUI", LoadSceneMode.Additive);
    }
    
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}