using System;
using UnityEngine;

[RequireComponent(typeof(PlayerInputManager))]
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

    // isGameOver: Is the player who died out of the game
    // TODO: If so, is the other player still alive?
    internal void PlayerDeath(int playerNumber, bool isGameOver)
    {
        bool loseGame = false;

        if( isGameOver ) {
            if( playerNumber == 1 ) {
                playerOneActive = false;
                loseGame = playerTwoActive;
            } else {
                playerTwoActive = false;
                loseGame = playerOneActive;
            }
        }

        if (loseGame)
        {
            Navigation navigation = FindObjectOfType<Navigation>();
            navigation.LoseGame();
        } else {
            Navigation navigation = FindObjectOfType<Navigation>();
            navigation.StartGame();
        }
    }

    internal void Victory() {
        Navigation navigation = FindObjectOfType<Navigation>();
        navigation.WinGame();
    }
}