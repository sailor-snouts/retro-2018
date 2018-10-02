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

        if( playerOneController && playerOneController.HasJoined()) {
            manager.playerOneActive = true;
        }

        if( playerTwoController && playerTwoController.HasJoined() ) {
            manager.playerTwoActive = true;
        }

        navigation.StartGame();

    }

}
