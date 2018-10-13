using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LevelController : MonoBehaviour {

    [SerializeField]
    GameObject playerOneSpawnPoint;

    [SerializeField]
    GameObject playerTwoSpawnPoint;
    
    void Start () {
        GameManager manager = FindObjectOfType<GameManager>();
        PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();

        Transform playerTransform = null;

        if( manager ) {
            if( manager.playerOneActive ) {
                GameObject playerOne = Instantiate(manager.GetPlayerOne());
                playerOne.transform.position = playerOneSpawnPoint.transform.position;
                playerOne.GetComponent<PlayerController>().SetPlayerNumber(1);
                playerTransform = playerOne.transform;
            }

            if (manager.playerTwoActive)
            {
                GameObject playerTwo = Instantiate(manager.GetPlayerTwo());
                playerTwo.GetComponent<PlayerController>().SetPlayerNumber(2);
                playerTwo.transform.position = playerTwoSpawnPoint.transform.position;
            }
        }

        CinemachineVirtualCamera followCam = FindObjectOfType<CinemachineVirtualCamera>();
        followCam.Follow = playerTransform;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
