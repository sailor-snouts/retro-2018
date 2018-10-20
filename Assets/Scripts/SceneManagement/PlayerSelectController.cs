using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerSelectController : MonoBehaviour {

    [SerializeField]
    Navigation navigation = null;
    [SerializeField]
    PlayerTypeSelectController playerOneController = null;
    [SerializeField]
    PlayerTypeSelectController playerTwoController = null;

    void Start () {
    }
	
	void Update () {
    
    }

    public void StartGame() {
        GameManager manager = FindObjectOfType<GameManager>();
        if( !manager ) {
            Debug.LogError("No GameManager instance in scene!");
            return;
        }

        if( playerOneController ) {
            manager.playerOneActive = true;
            manager.playerOneType = playerOneController.GetPlayerType();
            manager.playerOneLives = manager.maxPlayerLives;
        }

        if( playerTwoController && playerTwoController.HasJoined() ) {
            manager.playerTwoActive = true;
            manager.playerTwoType = playerTwoController.GetPlayerType();
            manager.playerTwoLives = manager.maxPlayerLives;
        }

        navigation.StartGame();

    }

}
