using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerSelectController : MonoBehaviour {

    [SerializeField]
    Text pressStartToJoin = null;
    [SerializeField]
    GameObject[] playerTypeSprites = null;
    [SerializeField]
    Text playerTypeText = null;
    [SerializeField]
    Text playerTypeTextShadow;

    //[SerializeField]
    //GameObject arrowIcon = null;
    //[SerializeField]
    //GameObject[] menuOptions;
    //[SerializeField]
    //EventSystem eventSystem;


    [SerializeField]
    float inputLag = 0.1f;

//    float inputLagRemaining = 0.0f;
    float playerSelectLagRemaining = 0.0f;

    bool flashUp;
    bool playerTwo;

    int playerType = 0;
    string[] playerTypeNames = new string[2];

//    int selectedMenuOption;

	void Start () {
        flashUp = false;
        playerTwo = false;

        playerTypeNames[0] = "Gunner";
        playerTypeNames[1] = "Paladin";

        //eventSystem.SetSelectedGameObject(menuOptions[0]);
	}
	
	void Update () {

        if( !playerTwo ) {
            FlashPlayerTwoMessage(Time.deltaTime);
        }

        //if( selectedMenuOption == 0)
            HandlePlayerSelectInput();
    }

    private void HandlePlayerSelectInput()
    {
        if (playerSelectLagRemaining >= Mathf.Epsilon)
        {
            playerSelectLagRemaining -= Time.deltaTime;
            return;
        }

        float horizontal = Input.GetAxisRaw("Horizontal");

        if (horizontal != 0)
        {
            ChangePlayerOne();
            playerSelectLagRemaining = inputLag;
        }
    }


    private void FlashPlayerTwoMessage(float deltaTime)
    {
        Color newAlpha = pressStartToJoin.color;

        if ( flashUp ) {
            if (newAlpha.a >= 1.0f)
            {
                flashUp = false;
            }
            else
            {
                newAlpha.a += (deltaTime * 2);
            }
        }
        else {
            if (newAlpha.a <= 0.0f)
            {
                flashUp = true;
            }
            else
            {
                newAlpha.a -= (deltaTime * 2);
            }
        }

        pressStartToJoin.color = newAlpha;
    }

    public void ChangePlayerOne() {
        playerTypeSprites[playerType].SetActive(false);

        playerType++;
        playerType %= 2;

        playerTypeText.text = playerTypeNames[playerType];
        playerTypeSprites[playerType].SetActive(true);
    }

    public void ChangePlayerTwo()
    {

    }

    public void AddPlayerTwo() {

    }

    public void DropPlayerTwo() {

    }
}
