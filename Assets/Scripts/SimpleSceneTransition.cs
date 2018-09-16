using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleSceneTransition : MonoBehaviour
{
    [SerializeField]
    private string scene;
    [SerializeField]
    private bool async = true;
    [SerializeField]
    private bool onAnyKey = true;

    private void Update()
    {
        if(this.onAnyKey && Input.anyKeyDown)
        {
            this.Change();
        }
    }

    public void Change()
    {
        if (this.async)
        {
            SceneManager.LoadSceneAsync(this.scene);
        }
        else
        {
            SceneManager.LoadScene(this.scene);
        }
    }
}
