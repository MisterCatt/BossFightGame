
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : PersistentSingleton<GameManager>
{
    public void LoadOptionsScene()
    {

    }
    public static void ToggleDeathScene(bool ToggleOn = true)
    {
        if(ToggleOn)
            SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        else
            SceneManager.UnloadSceneAsync(1);
    }

    public static void ToggleOptionsMenu(bool ToggleOn = true)
    {
        if (ToggleOn)
            SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
        else
            SceneManager.UnloadSceneAsync(2);
    }

    public static void LoadMainMenu()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
    public static void LoadPlayground()
    {
        SceneManager.LoadScene(3, LoadSceneMode.Single);
    }
}
