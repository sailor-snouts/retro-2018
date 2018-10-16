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

        //Transform playerTransform = null;
        int playerCount = 0;
        Debug.Log("LevelController::Start");

        GameObject playerOne = null, playerTwo = null;

        if( manager ) {
            if( manager.playerOneActive ) {
                playerOne = Instantiate(manager.GetPlayerOne());
                playerOne.transform.position = playerOneSpawnPoint.transform.position;
                playerOne.GetComponent<PlayerController>().SetPlayerNumber(1);
                //playerTransform = playerOne.transform;
                playerCount++;
            }

            if (manager.playerTwoActive)
            {
                playerTwo = Instantiate(manager.GetPlayerTwo());
                playerTwo.GetComponent<PlayerController>().SetPlayerNumber(2);
                playerTwo.transform.position = playerTwoSpawnPoint.transform.position;
                playerCount++;
            }
        }

        CinemachineTargetGroup targetGroup = GetComponentInChildren<CinemachineTargetGroup>();
        targetGroup.m_Targets = new CinemachineTargetGroup.Target[playerCount];
        for (int i = 0; i < playerCount; i++ ) {
            Cinemachine.CinemachineTargetGroup.Target target;
            target.target = i == 0 ? playerOne.transform : playerTwo.transform;
            target.weight = 1.0f;
            target.radius = 0.0f;
            targetGroup.m_Targets[i] = target;
        }
        //CinemachineVirtualCamera followCam = FindObjectOfType<CinemachineVirtualCamera>();
        //followCam.Follow = playerTransform;

        PlayerInputManager inputManager = manager.GetComponent<PlayerInputManager>();
        inputManager.Restart();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
