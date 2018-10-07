using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : ScriptableObject {

    private KeyCode fireKey;
    private KeyCode jumpKey;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void Initialize(int playerNum)
    {
        if (Input.GetJoystickNames().Length == 0)
        {
            InitializeKeyboard(playerNum);
        }
        else
        {
            if (Input.GetJoystickNames().Length > playerNum)
            {
                InitializeController(playerNum);
            }
            else
            {
                InitializeKeyboard(playerNum);
            }
        }

    }

    private void InitializeController(int playerNum) {
        switch (playerNum)
        {
            case 0:
                {
                    jumpKey = KeyCode.Joystick1Button0;
                    fireKey = KeyCode.Joystick1Button1;
                    break;
                }
            case 1:
                {
                    jumpKey = KeyCode.Joystick2Button0;
                    fireKey = KeyCode.Joystick2Button1;
                    break;
                }
            default:
                {
                    Debug.LogError("Unconfigured Controller detected");
                    break;
                }
        }
    }

    private void InitializeKeyboard(int playerNum)
    {
        switch (playerNum)
        {
            case 0:
                {
                    jumpKey = KeyCode.LeftAlt;
                    fireKey = KeyCode.LeftCommand;
                    break;
                }
            case 1:
                {
                    jumpKey = KeyCode.Slash;
                    fireKey = KeyCode.RightShift;
                    break;
                }
            default:
                {
                    Debug.LogError("Unconfigured Controller detected");
                    break;
                }
        }
    }

    public bool Fire()
    {
        return Input.GetKeyDown(fireKey);
    }

    public bool Jump()
    {
        return Input.GetKeyDown(jumpKey);
    }

}
