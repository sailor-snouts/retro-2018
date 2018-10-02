using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] playerPrefabs = null;

    public bool playerOneActive = true;
    public bool playerTwoActive = false;

    public static GameManager instance = null;

    [SerializeField]
    public float jumpHeight = 5f;

    [SerializeField]
    public float jumpApexTime = 0.5f;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        float gravity = -1f * (2f * this.jumpHeight) / (this.jumpApexTime * this.jumpApexTime);
        // setting the timesclae r2bd is undefined, if we grab it here then the parent scribt has issues with jumping
        //this.rb2d.gravityScale = gravity;
        Physics2D.gravity = new Vector2(0f, gravity);
    }

    internal GameObject GetPlayerOne() {
        return playerPrefabs[0];
    }

    internal GameObject GetPlayerTwo() {
        return playerPrefabs[1];
    }
}