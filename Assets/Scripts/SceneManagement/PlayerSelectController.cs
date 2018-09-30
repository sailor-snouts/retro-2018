using UnityEngine;
using UnityEngine.UI;

public class PlayerSelectController : MonoBehaviour {

    [SerializeField]
    Text pressStartToJoin;
    [SerializeField]
    GameObject[] playerTypeSprites;
    [SerializeField]
    Text playerTypeText;
    [SerializeField]
    Text playerTypeTextShadow;

    [SerializeField]
    GameObject arrowIcon;
    [SerializeField]
    int menuSize;

    bool flashUp;
    bool playerTwo;

    int playerType = 0;
    string[] playerTypeNames = new string[2];

    int selectedMenuOption;

    float menuThreshold = 0.5f;

	void Start () {
        flashUp = false;
        playerTwo = false;

        playerTypeNames[0] = "Gunner";
        playerTypeNames[1] = "Paladin";
	}
	
	void Update () {

        if( !playerTwo ) {
            FlashPlayerTwoMessage(Time.deltaTime);
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
//        Debug.Log("unclamped horizontal: " + horizontal);

        //if (Mathf.Abs(horizontal) < 0.0f)
        //{
        //    horizontal = -1.0f;
        //}
        //else if (horizontal > 0.0f)
        //{
        //    horizontal = 1.0f;
        //}
        //else 
        //    horizontal = 0.0f;

        //Debug.Log("clamped horizontal: " + horizontal);

        if ( horizontal < 0 ) {
            // select character type Left Arrow
        }

        if (horizontal > 0)
        {
            // select character type Right Arrow
        }

        float vertical = Input.GetAxisRaw("Vertical");
        Debug.Log("unclamped vertical: " + vertical);

        if (vertical < 0)
        {
            // select character color Down Arrow
            selectedMenuOption++;
            if (selectedMenuOption >= menuSize)
                selectedMenuOption = 0;

            arrowIcon.transform.position = new Vector3(arrowIcon.transform.position.x, -2.575f - (0.875f * selectedMenuOption));
        }

        if (vertical > 0)
        {
            // select character color Up Arrow
            selectedMenuOption--;
            if (selectedMenuOption < 0)
                selectedMenuOption = (menuSize - 1);

            arrowIcon.transform.position = new Vector3(arrowIcon.transform.position.x, -2.575f - (0.875f * selectedMenuOption));
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
