using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInputManager : MonoBehaviour
{
    public static PlayerInputManager instance = null;
    public enum States { GAME, PAUSE, LOADING, SELECT, MENU, SPLASH };
    [SerializeField]
    private States state = States.GAME;

    private float menuSelectionLag = 0.2f;
    private float menuSelectionLagCounter = 0f;

    private PlayerController player1;
    private PlayerController player2;
    [SerializeField]
    private bool isKeyboardP1 = false;
    [SerializeField]
    private bool isKeyboardP2 = false;
    private Navigation navigation;
    private MenuController menu;

    public void Start()
    {

        if (PlayerInputManager.instance == null)
        {
            PlayerInputManager.instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.Log("destroying");
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += SceneLoaded;
    }

    // Called on level reload
    public void Reset()
    {
        foreach (PlayerController player in FindObjectsOfType<PlayerController>())
        {
            if (player.getPlayerNumber() == 1 && this.player1 == null)
            {
                this.player1 = player;
                if(Input.GetJoystickNames().Length < 2)// @TODO might be a bug but i have 1 with no joystick plugged in.
                {
                    this.isKeyboardP1 = true;
                }
                continue;
            }
            if (player.getPlayerNumber() == 2 && this.player2 == null)
            {
                this.player2 = player;
                if (Input.GetJoystickNames().Length < 3)
                {
                    this.isKeyboardP2 = true;
                }
                continue;
            }

            Debug.LogError("Player " + player.getPlayerNumber() + " Controller already set or undefined");
        }

        this.navigation = FindObjectOfType<Navigation>();

    }

    public void Restart() 
    {
        this.player1 = this.player2 = null;
        Reset();
    }

    protected void Update()
    {
        if(this.state == States.GAME && this.player1 == null)
        {
            this.Reset();
        }

        if (this.state == States.SPLASH)
        {
            this.SplashInput();
        }
        else if (this.state == States.PAUSE)
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

    public void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "Scenes/Splash":
            case "Scenes/Title":
            case "Scenes/OpeningCrawl":
            case "Scenes/Win":
            case "Scenes/Lose":
                this.state = States.SPLASH;
                break;
            case "Scenes/Loading":
                this.state = States.LOADING;
                break;
            case "Scenes/Pause":
                this.state = States.PAUSE;
                break;
            case "Scenes/Sandbox-robert":
            case "Scenes/LightLevel0":
                this.state = States.GAME;
                break;
            case "Scenes/Options":
                this.state = States.MENU;
                break;
            case "Scenes/PlayerSelect":
                this.state = States.SELECT;
                break;
        }
    }

    protected void PauseInput()
    { 
        if (Input.GetKeyDown(KeyCode.JoystickButton7) || ((Input.GetKeyDown(KeyCode.Escape)) && (this.isKeyboardP1 || this.isKeyboardP2)))
        {
            this.navigation.PauseGame();
            if (Navigation.isPaused)
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

        if (Input.GetKeyDown(KeyCode.JoystickButton1) || (this.isKeyboardP1 && Input.GetKeyDown(KeyCode.Return)))
        {
            menu.SelectOption();
        }

        this.menuSelectionLagCounter = Mathf.Clamp(this.menuSelectionLagCounter - Time.fixedUnscaledDeltaTime, 0, 5f);

        if (this.menuSelectionLagCounter > 0)
        {
            return;
        }

        if (Mathf.Abs(Input.GetAxis("Vertical_PlayerOne_Joystick")) > 0.01f || (this.isKeyboardP1 && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))))
        {
            float menuDirection = Mathf.Abs(Input.GetAxis("Vertical_PlayerOne_Joystick")) > 0.01f ? Input.GetAxis("Vertical_PlayerOne_Joystick") : (Input.GetKeyDown(KeyCode.UpArrow) ? -1f : 1f);
            menu.ChangeSelection(menuDirection);
            this.menuSelectionLagCounter = this.menuSelectionLag;
        }
        else if (Mathf.Abs(Input.GetAxis("Vertical_PlayerTwo_Joystick")) > 0.01f || (this.isKeyboardP2 && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))))
        {
            float menuDirection = Mathf.Abs(Input.GetAxis("Vertical_PlayerTwo_Joystick")) > 0.01f ? Input.GetAxis("Vertical_PlayerTwo_Joystick") : (Input.GetKeyDown(KeyCode.UpArrow) ? -1f : 1f);
            menu.ChangeSelection(Input.GetAxis("Vertical_PlayerTwo_Joystick"));
            this.menuSelectionLagCounter = this.menuSelectionLag;
        }
    }

    protected void GameInput()
    {
        if (this.player1 == null)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button0) || (this.isKeyboardP1 && Input.GetKeyDown(KeyCode.Space)))
        {
            this.player1.Jump();
        }
        if (Input.GetKeyUp(KeyCode.Joystick1Button0) || (this.isKeyboardP1 && Input.GetKeyUp(KeyCode.Space)))
        {
            this.player1.JumpRelease();
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button1) || (this.isKeyboardP1 && Input.GetKeyDown(KeyCode.LeftShift)))
        {
            if (Input.GetAxis("Vertical_PlayerOne_Joystick") < -0.5f || (this.isKeyboardP1 && Input.GetKey(KeyCode.UpArrow)))
            {
                this.player1.Attack2();
            }
            else
            {
                this.player1.Attack1();
            }
        }

        float playerWalk1 = 0f;
        if(this.isKeyboardP1)
        {
            if(Input.GetKey(KeyCode.LeftArrow))
            {
                playerWalk1 = -1f;
            }
            else if(Input.GetKey(KeyCode.RightArrow))
            {
                playerWalk1 = 1f;
            }
        } 
        else
        {
            playerWalk1 = Input.GetAxis("Horizontal_PlayerOne_Joystick");
        }
        player1.Walk(playerWalk1);


        if (this.player2 == null)
        {
            return;
        }
        // @TODO add keyboard controls for player 2

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

    protected void SplashInput()
    {
        if (Input.anyKeyDown)
        {
            SimpleSceneTransition transition = FindObjectOfType<SimpleSceneTransition>();
            transition.Change();
        }
    }

}