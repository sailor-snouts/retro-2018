using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Currently only supports Vertical movement
public class LightLiftController : MonoBehaviour {

    [SerializeField]
    GameObject liftBar;
    [SerializeField]
    float speed = 4.0f;
    [SerializeField]
    bool up = true;
    [SerializeField]
    float maxDistance = 10.0f;
    [SerializeField]
    float maxIdleTime = 3.0f;

    float startPos = 0.0f;
    bool moving = false;
    Transform playerTransform = null;
    float idleTime = 0.0f;

    bool atMaxHeight = false;
    bool resetPosition = false;

    private void Start()
    {
        startPos = transform.position.x;
    }

    void FixedUpdate () {
        if(moving) {

            float distance = Mathf.Abs(transform.position.x) - startPos;

            if( distance >= maxDistance ) {
                moving = false;
                atMaxHeight = true;
                return;
            }

            if (resetPosition && distance <= Mathf.Epsilon) {
                resetPosition = false;
                moving = false;
                return;
            }

            float moveDelta = Time.fixedDeltaTime * speed;
            moveDelta *= up ? 1 : -1;

            // TODO: Clamp to PPU
            Vector3 liftVector = new Vector3(0, moveDelta, 0);
            gameObject.transform.position += liftVector;
            playerTransform.position += liftVector;
        } 

        if(atMaxHeight) {
            idleTime += Time.fixedDeltaTime;
            if( idleTime >= maxIdleTime ) {
                up = !up;
                idleTime = 0.0f;
                atMaxHeight = false;
                resetPosition = true;
            }
        }

	}

    // TODO: Count # players for multiplayer support
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if( collision.gameObject.tag == "Player" ){
            Debug.Log("Light Lift collision enter");

            // Tracking current player transform to manage the collider positions
            // while moving the platform during Update
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
