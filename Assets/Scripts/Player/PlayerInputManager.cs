using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : ScriptableObject {

    private KeyCode fireKey;
    private KeyCode jumpKey;
    private KeyCode pauseKey;
    private KeyCode cancelKey;

    public void Initialize(int playerNum)
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

    private void InitializeController(int playerNum) {
        switch (playerNum)
        {
            case 0:
                {
                    jumpKey = KeyCode.Joystick1Button0;
                    fireKey = KeyCode.Joystick1Button1;
                    pauseKey = KeyCode.Joystick1Button9;
                    cancelKey = KeyCode.Joystick1Button2;
                    break;
                }
            case 1:
                {
                    jumpKey = KeyCode.Joystick2Button0;
                    fireKey = KeyCode.Joystick2Button1;
                    cancelKey = KeyCode.Joystick2Button2;
                    pauseKey = KeyCode.Joystick2Button9;
                    break;
                }
            default:
                {
                    Debug.LogError("Unconfigured Player Num " + playerNum + " detected");
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
                    pauseKey = KeyCode.P;
                    cancelKey = KeyCode.Escape;
                    break;
                }
            case 1:
                {
                    jumpKey = KeyCode.Slash;
                    fireKey = KeyCode.RightShift;
                    pauseKey = KeyCode.RightBracket;
                    cancelKey = KeyCode.LeftBracket;
                    break;
                }
            default:
                {
                    Debug.LogError("Unconfigured Player Num " + playerNum + " detected");
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

    public bool Cancel()
    {
        return Input.GetKeyDown(cancelKey);
    }

    public bool Pause()
    {
        return Input.GetKeyDown(pauseKey);
    }

    // @TODO Temporary, each use could probably be refactored 
    public float GetAxis(string axisName) 
    {
        return Input.GetAxis(axisName);
    }
    public float GetAxisRaw(string axisName)
    {
        return Input.GetAxisRaw(axisName);
    }

}
