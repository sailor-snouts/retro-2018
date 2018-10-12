using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour {
    enum States { GAME, PAUSE, SELECT, OPTION };
    private States state = States.GAME;
    private PlayerController player1;
    private PlayerController player2;

    private KeyCode fireKey;
    private KeyCode jumpKey;
    private KeyCode pauseKey;
    private KeyCode cancelKey;

    public void Awake()
    {
        foreach(PlayerController player in FindObjectsOfType<PlayerController>())
        {
            if(player.getPlayerNumber() == 1 && this.player1 == null)
            {
                this.player1 = player;
                continue;
            }
            if(player.getPlayerNumber() == 2 && this.player2 == null)
            {
                this.player2 = player;
                continue;
            }
            Debug.LogError("Player " + player.getPlayerNumber() + " Controller already set or undefined");
        }
    }

    protected void Update()
    {
        // because the player is added dynamically, awake might be called too soon and cant locate the player
        if (this.player1 == null)
        {
            this.Awake();
        }

        switch (this.state)
        {
            case States.GAME:
                this.GameInput();
                break;
        }
    }

    protected void GameInput()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            this.player1.Jump();
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            if (Input.GetAxis("Vertical_PlayerOne_Joystick") < -0.5f)
            {
                this.player1.Attack2();
            }
            else
            {
                this.player1.Attack1();
            }
        }

        player1.Walk(Input.GetAxis("Horizontal_PlayerOne_Joystick"));
    }

}
