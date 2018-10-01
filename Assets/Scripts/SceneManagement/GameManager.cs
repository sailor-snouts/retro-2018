using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] playerPrefabs = null;

    GameObject playerOne;
    GameObject playerTwo;

    public static GameManager instance = null;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    internal void AddPlayerOne(int type)
    {
        playerOne = Instantiate(playerPrefabs[type]);
    }

    internal void AddPlayerTwo(int type)
    {
        playerTwo = Instantiate(playerPrefabs[type]);
    }
}