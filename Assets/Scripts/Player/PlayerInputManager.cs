using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour {
    enum States { GAME, PAUSE, SELECT, OPTION };
    [SerializeField]
    private States state = States.GAME;

    private float menuSelectionLag = 0.2f;
    private float menuSelectionLagCounter = 0f;

    private PlayerController player1;
    private PlayerController player2;
    private Navigation navigation;
    private MenuController menu;

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
        this.navigation = FindObjectOfType<Navigation>();
    }

    protected void Update()
    {
        // because the player is added dynamically, awake might be called too soon and cant locate the player
        if (this.player1 == null)
        {
            this.Awake();
        }
        
        if (this.state == States.PAUSE)
        {
            this.PauseInput();
            this.MenuInput();
        }
        else if (this.state == States.GAME)
        {
            this.GameInput();
            this.PauseInput();
        }
    }

    protected void PauseInput()
    {
        if(Input.GetKeyDown(KeyCode.JoystickButton7))
        {
            this.navigation.PauseGame();
            if(Navigation.isPaused)
            {
                this.state = States.PAUSE;
            }
            else
            {
                this.state = States.GAME;
                this.menu = null;
            }
        }
    }

    protected void MenuInput()
    {
        if (this.menu == null)
        {
            this.menu = FindObjectOfType<MenuController>();
        }

        if (Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            menu.SelectOption();
        }

        this.menuSelectionLagCounter = Mathf.Clamp(this.menuSelectionLagCounter - Time.fixedUnscaledDeltaTime, 0, 5f);

        if (this.menuSelectionLagCounter > 0)
        {
            return;
        }

        if (Mathf.Abs(Input.GetAxis("Vertical_PlayerOne_Joystick")) > 0.01f)
        {
            menu.ChangeSelection(Input.GetAxis("Vertical_PlayerOne_Joystick"));
            this.menuSelectionLagCounter = this.menuSelectionLag;
        }
        else if(Mathf.Abs(Input.GetAxis("Vertical_PlayerTwo_Joystick")) > 0.01f)
        {
            menu.ChangeSelection(Input.GetAxis("Vertical_PlayerTwo_Joystick"));
            this.menuSelectionLagCounter = this.menuSelectionLag;
        }
    }

    protected void GameInput()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            this.player1.Jump();
        }
        if(Input.GetKeyUp(KeyCode.Joystick1Button0))
        {
            this.player1.JumpRelease();
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


        if (this.player2 == null)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Joystick2Button0))
        {
            this.player2.Jump();
        }
        if (Input.GetKeyUp(KeyCode.Joystick2Button0))
        {
            this.player2.JumpRelease();
        }
        if (Input.GetKeyDown(KeyCode.Joystick2Button1))
        {
            if (Input.GetAxis("Vertical_PlayerTwo_Joystick") < -0.5f)
            {
                this.player2.Attack2();
            }
            else
            {
                this.player2.Attack1();
            }
        }
        player2.Walk(Input.GetAxis("Horizontal_PlayerTwo_Joystick"));
    }

}
