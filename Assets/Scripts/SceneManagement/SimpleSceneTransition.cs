using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleSceneTransition : MonoBehaviour
{
    [SerializeField]
    private string scene = null;
    [SerializeField]
    private bool async = true;
    [SerializeField]
    private bool timed = false;
    [SerializeField]
    private float lifetime = 10.0f;

    private void Start()
    {
        if( timed ) {
            Invoke("Change", lifetime);
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
