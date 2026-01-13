using UnityEngine;
using Component.SceneLoader;

public class UIMainMenuController : MonoBehaviour
{
    private void Start()
    {
        var save = new SaveData();
        save.PlayerName = "Player";
        
        SaveService.Save(save);
    }
    
    public void PlayGame()
    {
        // Load scenes
        SceneLoaderService.LoadLevel();
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