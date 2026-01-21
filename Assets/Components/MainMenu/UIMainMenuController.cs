using UnityEngine;
using Component.SceneLoader;

public class UIMainMenuController : MonoBehaviour
{
    
    public void PlayGame(int levelIndex)
    {
        if (!SaveService.TryLoad(out SaveData saveData))
        {
            saveData = new SaveData();
        }

        saveData.RunCount++;
        saveData.LevelIndex = levelIndex;
        SaveService.Save(saveData);
        
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