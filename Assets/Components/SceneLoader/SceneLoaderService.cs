using UnityEngine;
using UnityEngine.SceneManagement;

namespace Component.SceneLoader
{
    public static class SceneLoaderService
    {
        public static void LoadLevel()
        {
            Debug.Log("Loading level...");
            SceneManager.LoadScene("Level", LoadSceneMode.Single);
            SceneManager.LoadScene("LevelUI", LoadSceneMode.Additive);
            Debug.Log("Level loaded !");
        }

        public static void LoadMainMenu()
        {
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }
    }
}