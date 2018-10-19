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

    public int maxPlayerLives = 3;
    public int playerOneLives = 0;
    public int playerTwoLives = 0;

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

    internal int PlayerLives(int playerNumber) {
        return playerNumber == 1 ? playerOneLives : playerTwoLives;
    }

    internal void PlayerDeath(int playerNumber)
    {
        LoseLives(playerNumber);

        if (PlayerLives(playerNumber) == 0)
        {
            playerOneLives = maxPlayerLives;
            playerTwoLives = maxPlayerLives;
            Navigation navigation = FindObjectOfType<Navigation>();
            navigation.LoseGame();
        } else {
            Navigation navigation = FindObjectOfType<Navigation>();
            navigation.StartGame();
        }
    }

    public void GainLives(int playerNumber)
    {
        if (playerNumber == 1)
        {
            playerOneLives++;
            if (playerOneLives > maxPlayerLives) playerOneLives = maxPlayerLives;
            return;
        }

        if (playerNumber == 2)
        {
            playerTwoLives++;
            if (playerTwoLives > maxPlayerLives) playerTwoLives = maxPlayerLives;
            return;
        }
    }

    public void LoseLives(int playerNumber)
    {
        if ( playerNumber == 1 ) {
            playerOneLives--;
            if (playerOneLives < 0) playerOneLives = 0;
            return;
        }

        if (playerNumber == 2)
        {
            playerTwoLives--;
            if (playerTwoLives < 0) playerTwoLives = 0;
            return;
        }
    }

    public bool IsGameOver(int playerNumber) {
        if( playerNumber == 1 ) {
            return playerOneLives <= 0;
        }
        if( playerNumber == 2 ) {
            return playerTwoLives <= 0;
        }

        return false;
    }

    internal void Victory() {
        Navigation navigation = FindObjectOfType<Navigation>();
        navigation.WinGame();
    }
}