using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Navigation : MonoBehaviour
{
    [SerializeField]
    public static bool isPaused = false;

    #region Handle Input
    void Update()
    {
        if (Navigation.isPaused)
        {
            return;
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
        SceneManager.LoadScene("PlayerSelect");
    }

    public void PlayTwoPlayerGame()
    {
        SceneManager.LoadScene("PlayerSelect");
    }

    public void StartGame() 
    {
        SceneManager.LoadScene("sandbox-robert");
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
<<<<<<< HEAD
        Navigation.isPaused = true;
        //Time.timeScale = 0F;
        SceneManager.LoadScene("Pause", LoadSceneMode.Additive);
    }

    public void UnpauseGame() 
    {
        Navigation.isPaused = false;
        //Time.timeScale = 1F;
    }

    public void ResumeGame()
    {
        UnpauseGame();
        GoBack("Pause");
=======
        if (Navigation.isPaused)
        {
            Navigation.isPaused = false;
            Time.timeScale = 1F;
            SceneManager.UnloadSceneAsync("Pause");
        }
        else
        {
            Navigation.isPaused = true;
            Time.timeScale = 0F;
            SceneManager.LoadScene("Pause", LoadSceneMode.Additive);
        }
>>>>>>> 014c752cf2cbd75c3b7373c9d70f0ff9787cefc0
    }

    public void GoBack(string sceneName)
    {
        Debug.Log("Trying to unload Scene: " + sceneName);
        SceneManager.UnloadSceneAsync(sceneName);
    }
    #endregion

}
