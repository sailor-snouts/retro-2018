using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LevelController : MonoBehaviour {

    [SerializeField]
    GameObject playerOneSpawnPoint;
    [SerializeField]
    GameObject playerTwoSpawnPoint;

    // Use this for initialization
    void Start () {
        GameManager manager = FindObjectOfType<GameManager>();
        PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();

        Transform playerTransform = null;

        if( manager ) {
            if( manager.playerOneActive ) {
                GameObject playerOne = Instantiate(manager.GetPlayerOne());
                playerOne.transform.position = playerOneSpawnPoint.transform.position;
                PlayerController playerController = playerOne.GetComponent<PlayerController>();
                playerController.axisName = "PlayerOne";
                playerController.health = playerHealth;

                PlayerInputManager inputManager = ScriptableObject.CreateInstance<PlayerInputManager>();
                inputManager.Initialize(0);
                playerController.inputManager = inputManager;

                playerTransform = playerOne.transform;

            }

            if (manager.playerTwoActive)
            {
                GameObject playerTwo = Instantiate(manager.GetPlayerTwo());
                playerTwo.transform.position = playerTwoSpawnPoint.transform.position;
                PlayerController playerController = playerTwo.GetComponent<PlayerController>();
                playerController.axisName = "PlayerTwo";
                playerController.health = playerHealth;
                PlayerInputManager inputManager = ScriptableObject.CreateInstance<PlayerInputManager>();
                inputManager.Initialize(1);
                playerController.inputManager = inputManager;
            }
        }

        CinemachineVirtualCamera followCam = FindObjectOfType<CinemachineVirtualCamera>();
        followCam.Follow = playerTransform;


    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
