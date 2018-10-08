using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimplePause : MonoBehaviour
{
    [SerializeField]
    private string pauseScene = "Scenes/Pause";

    [SerializeField]
    private static bool isPaused = false;

    public static bool IsPaused()
    {
        return SimplePause.isPaused;
    }

	void Update () {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (SimplePause.isPaused)
            {
                SimplePause.isPaused = false;
              //  Time.timeScale = 1F;
                SceneManager.UnloadSceneAsync(this.pauseScene);
            }
            else
            {
                SimplePause.isPaused = true;
               // Time.timeScale = 0F;
                SceneManager.LoadScene(this.pauseScene, LoadSceneMode.Additive);
            }
        }
    }
}
