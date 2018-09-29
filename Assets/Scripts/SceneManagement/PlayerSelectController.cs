using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelectController : MonoBehaviour {

    [SerializeField]
    Text pressStartToJoin;

    bool flashUp;
    bool playerTwo;

	// Use this for initialization
	void Start () {
        flashUp = false;
        playerTwo = false;
	}
	
	// Update is called once per frame
	void Update () {

        if( !playerTwo ) {
            FlashPlayerTwoMessage(Time.deltaTime);
        }

        // TODO: Figure out how to snap to -1 < 0 > 1
        if( Input.GetAxisRaw("Horizontal") < Mathf.Epsilon ) {
            // select character type Left Arrow
        }

        if (Input.GetAxisRaw("Horizontal") > Mathf.Epsilon)
        {
            // select character type Right Arrow
        }

        if (Input.GetAxisRaw("Vertical") < Mathf.Epsilon)
        {
            // select character color Down Arrow
        }

        if (Input.GetAxisRaw("Vertical") > Mathf.Epsilon)
        {
            // select character color Up Arrow
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

    }

    public void ChangePlayerTwo()
    {

    }

    public void AddPlayerTwo() {

    }

    public void DropPlayerTwo() {

    }
}
