using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightLiftController : MonoBehaviour {

    [SerializeField]
    GameObject liftBar;
    [SerializeField]
    float speed = 4.0f;
    [SerializeField]
    bool up = true;

    bool moving = false;
    Transform playerTransform = null;
    	
	void FixedUpdate () {
        if(moving) {
            float moveDelta = Time.fixedDeltaTime * speed;
            moveDelta *= up ? 1 : -1;

            // TODO: Clamp to PPU
            Vector3 liftVector = new Vector3(0, moveDelta, 0);
            gameObject.transform.position += liftVector;
            playerTransform.position += liftVector;
        }
	}

    // TODO: Count # players for multiplayer support
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if( collision.gameObject.tag == "Player" ){
            Debug.Log("Light Lift collision enter");

            // TODO: This is hacky, but without it the player gets "stuck" in the rb2d
            //collision.gameObject.transform.position += new Vector3(0, 0.13f, 0);
            playerTransform = collision.gameObject.transform;

            moving = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Light Lift collision enter");
            moving = false;
        }
    }
}
