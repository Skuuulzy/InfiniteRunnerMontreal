using UnityEngine;
using Component.SceneLoader;
using Components.Player.Scripts;

public class UIMainMenuController : MonoBehaviour
{
    public void PlayGame()
    {
        if (!SaveService.TryLoad(out SaveData saveData))
        {
            saveData = new SaveData();
        }

        saveData.RunCount++;
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