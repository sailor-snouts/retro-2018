using UnityEngine;
using UnityEngine.UI;

public class PlayerTypeSelectController : MonoBehaviour
{

    [SerializeField]
    GameObject pressStartPanel = null;
    [SerializeField]
    GameObject playerSelectPanel = null;

    [SerializeField]
    GameObject[] playerTypeSprites = null;
    [SerializeField]
    Text playerTypeText = null;
    [SerializeField]
    Text playerTypeTextShadow;
    [SerializeField]
    string axisName = "PlayerOne";
    private string controlAxis = "Joystick";

    [SerializeField]
    bool hasJoined;

    [SerializeField]
    float inputLag = 0.1f;
    float playerSelectLagRemaining = 0.0f;

    bool flashUp;
    int playerType = 0;

    string[] playerTypeNames = new string[2];
    Text[] pressStartText = null;

    PlayerInputManager inputManager;

    void Start()
    {
        flashUp = false;
        hasJoined = false;

        playerTypeNames[0] = "Gunner";
        playerTypeNames[1] = "Paladin";

        // Optimization: cache for text flashing performance
        pressStartText = pressStartPanel.GetComponentsInChildren<Text>();

        //if (Input.GetJoystickNames().Length == 0)
        //{
        //    controlAxis = axisName + "_Keyboard";
        //}
        //else
        //{
        //    int controllerIndex = (axisName == "PlayerOne") ? 0 : 1;
        //    if (Input.GetJoystickNames().Length > controllerIndex)
        //    {
        //        Debug.Log("Using controller for " + axisName);
        //        controlAxis = axisName + "_Joystick";
        //    }
        //    else
        //    {
        //        Debug.Log("Using keyboard for " + axisName);
        //        controlAxis = axisName + "_Keyboard";
        //    }
        //}

    }

    void Update()
    {

        HandlePlayerSelectInput();
    }

    public bool HasJoined() {
        return hasJoined;
    }

    public int GetPlayerType() {
        return playerType;
    }

    private void HandlePlayerSelectInput()
    {
        if (playerSelectLagRemaining >= Mathf.Epsilon)
        {
            playerSelectLagRemaining -= Time.deltaTime;
            return;
        }

        //float horizontal = Input.GetAxisRaw("Horizontal_" + controlAxis);
        float horizontal = Input.GetAxisRaw("Horizontal");

        if (horizontal != 0 && (Mathf.Abs(horizontal) >= inputLag))
        {
            ChangePlayerType();
            playerSelectLagRemaining = inputLag;
        }
    }


    private void FlashPlayerTwoMessage(float deltaTime)
    {
        foreach( Text text in pressStartText ) {
            Color newAlpha = text.color;

            if (flashUp)
            {
                if (newAlpha.a >= 1.0f)
                {
                    flashUp = false;
                }
                else
                {
                    newAlpha.a += (deltaTime * 2);
                }
            }
            else
            {
                if (newAlpha.a <= 0.0f)
                {
                    flashUp = true;
                }
                else
                {
                    newAlpha.a -= (deltaTime * 2);
                }
            }

            text.color = newAlpha;
        }
    }

    public void ChangePlayerType()
    {
        Debug.Log("ChangePlayerType");

        playerTypeSprites[playerType].SetActive(false);

        playerType++;
        playerType %= 2;

        playerTypeText.text = playerTypeNames[playerType];
        playerTypeSprites[playerType].SetActive(true);
    }

    public void AddPlayer()
    {
        hasJoined = true;

        pressStartPanel.SetActive(false);
        playerSelectPanel.SetActive(true);
    }

    public void DropPlayer()
    {
        hasJoined = false;

        pressStartPanel.SetActive(true);
        playerSelectPanel.SetActive(false);
    }
}
