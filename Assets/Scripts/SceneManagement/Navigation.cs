using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Navigation : MonoBehaviour
{
    [SerializeField]
    private static bool isPaused = false;
    public static bool IsPaused()
    {
        return Navigation.isPaused;
    }

    #region Handle Input
    void Update()
    {
        if(Navigation.isPaused) {
            return;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseGame();
        }
    }
    #endregion

    #region New Scenes
    public void Title()
    {
        SceneManager.LoadScene("Title");
    }

    public void PlayOnePlayerGame()
    {
        SceneManager.LoadScene("sandbox-robert");
    }

    public void PlayTwoPlayerGame()
    {
        SceneManager.LoadScene("Sandbox");
    }

    public void WinGame()
    {
        SceneManager.LoadScene("Win");
    }

    public void LoseGame()
    {
        SceneManager.LoadScene("Lose");
    }

    public void Loading()
    {
        SceneManager.LoadScene("Loading");
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit ();
        #endif
    }

    internal void GoToScene(string gotoSceneName)
    {
        SceneManager.LoadScene(gotoSceneName);
    }

    #endregion

    #region Modals
    public void PauseGame()
    {
        Time.timeScale = 0F;
        SceneManager.LoadScene("Pause", LoadSceneMode.Additive);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1F;
        GoBack("Pause");
    }

    public void GoBack(string sceneName)
    {
        Debug.Log("Trying to unload Scene: " + sceneName);
        SceneManager.UnloadSceneAsync(sceneName);
    }
    #endregion

}
