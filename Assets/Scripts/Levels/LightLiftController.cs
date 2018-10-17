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

    float distanceTravelled = 0.0f;
    bool moving = false;
    Transform playerTransform = null;
    bool on = false;

    //float idleTime = 0.0f;

    bool atMaxHeight = false;
    //bool resetPosition = false;

    Vector3 startPosition, maxPosition;

    private void Start()
    {
        startPosition = transform.position;
        Vector3 maxVector = up ? new Vector3(0.0f, maxDistance) : new Vector3(0.0f, -maxDistance);
        maxPosition = startPosition + maxVector;
    }

    void FixedUpdate () {

        if(moving) {

            float moveDelta = Time.fixedDeltaTime * speed;
            distanceTravelled += moveDelta;
            moveDelta *= up ? 1 : -1;

            // Clamped to PPU by speed setting
            Vector3 liftVector = new Vector3(0, moveDelta, 0);
            gameObject.transform.position += liftVector;

            // If a player is on the lift, nudge them so their collider doesn't get stuck!
            if(playerTransform)
                playerTransform.position += liftVector;

            atMaxHeight = up ? transform.position.y >= maxPosition.y : transform.position.y <= maxPosition.y;

            if (atMaxHeight)
            {
                moving = false;
                Debug.Log("Setting up ResetPosition call for " + maxIdleTime + "secs");
                Invoke("ResetPosition", maxIdleTime);
            }
        }
    }

    private void ResetPosition() {
        Debug.Log("Resetting Lift Position");
        up = !up;
        Vector3 maxVector = up ? new Vector3(0.0f, maxDistance) : new Vector3(0.0f, -maxDistance);
        maxPosition = transform.position + maxVector;
        Debug.Log("New maxPosition.y = " + maxPosition.y);
        moving = true;
        distanceTravelled = 0.0f;
    }

    // TODO: Count # players for multiplayer support
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerTransform = collision.gameObject.transform;
            if (!on && !moving) {
                on = true;
                moving = true;
            }
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (playerTransform)
        {
            playerTransform = null;
        }
    }
}
