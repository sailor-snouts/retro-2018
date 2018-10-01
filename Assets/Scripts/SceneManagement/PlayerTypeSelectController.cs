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

    [SerializeField]
    float inputLag = 0.1f;
    float playerSelectLagRemaining = 0.0f;

    bool flashUp;
    bool hasJoined;
    int playerType = 0;

    string[] playerTypeNames = new string[2];
    Text[] pressStartText = null;


    void Start()
    {
        flashUp = false;
        hasJoined = false;

        playerTypeNames[0] = "Gunner";
        playerTypeNames[1] = "Paladin";

        // Optimization: cache for text flashing performance
        pressStartText = pressStartPanel.GetComponentsInChildren<Text>();
    }

    void Update()
    {
        if (!hasJoined)
        {
            FlashPlayerTwoMessage(Time.deltaTime);

            if( Input.GetAxisRaw("Pause_" + axisName) > 0 ) {
                AddPlayer();
            }
        } else {
            if (Input.GetAxisRaw("Cancel_" + axisName) > 0)
            {
                DropPlayer();
            }
        }

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

        float horizontal = Input.GetAxisRaw("Horizontal_" + axisName);

        if (horizontal != 0)
        {
            ChangePlayerType();
            playerSelectLagRemaining = inputLag;
        }
    }


    private void FlashPlayerTwoMessage(float deltaTime)
    {
        Debug.Log("PressStartText size: " + pressStartText.Length);

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
