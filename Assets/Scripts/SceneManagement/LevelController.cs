using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

    [SerializeField]
    GameObject playerOneSpawnPoint;
    [SerializeField]
    GameObject playerTwoSpawnPoint;

    // Use this for initialization
    void Start () {
        GameManager manager = FindObjectOfType<GameManager>();
        PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();

        if( manager ) {
            if( manager.playerOneActive ) {
                GameObject playerOne = Instantiate(manager.GetPlayerOne());
                playerOne.transform.position = playerOneSpawnPoint.transform.position;
                PlayerController playerController = playerOne.GetComponent<PlayerController>();
                playerController.axisName = "PlayerOne";
                playerController.health = playerHealth;
            }

            if (manager.playerTwoActive)
            {
                GameObject playerTwo = Instantiate(manager.GetPlayerTwo());
                playerTwo.transform.position = playerTwoSpawnPoint.transform.position;
                PlayerController playerController = playerTwo.GetComponent<PlayerController>();
                playerController.axisName = "PlayerTwo";
                playerController.health = playerHealth;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
