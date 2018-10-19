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
        if( targetGroup ) {
            targetGroup.m_Targets = new CinemachineTargetGroup.Target[playerCount];
            for (int i = 0; i < playerCount; i++)
            {
                CinemachineTargetGroup.Target target;
                target.target = i == 0 ? playerOne.transform : playerTwo.transform;
                target.weight = 1.0f;
                target.radius = 0.0f;
                targetGroup.m_Targets[i] = target;
            }
        } else {
            Debug.LogWarning("Cinemachine Target Group not configured correctly, get ready for a boring view");
        }

        PlayerInputManager inputManager = FindObjectOfType<PlayerInputManager>();
        if (inputManager)
            inputManager.Restart();
        else
            Debug.LogWarning("Couldn't find the input manager, get ready for a boring game!");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
