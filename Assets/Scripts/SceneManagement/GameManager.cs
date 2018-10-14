using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] playerPrefabs = null;

    public bool playerOneActive = true;
    public bool playerTwoActive = false;

    public static GameManager instance = null;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    internal GameObject GetPlayerOne() {
        return playerPrefabs[0];
    }

    internal GameObject GetPlayerTwo() {
        return playerPrefabs[1];
    }

    internal void PlayerDeath(int playerNumber)
    {
        Navigation navigation = FindObjectOfType<Navigation>();
        navigation.StartGame();
    }
}