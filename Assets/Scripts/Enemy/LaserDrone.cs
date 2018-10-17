using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDrone : MonoBehaviour {

    [SerializeField]
    float dirX, dirY;
    [SerializeField]
    float fireRate = 0.1f;

    BulletGenerator blaster;
    Vector2 direction;
    float fireTimer;

    // TODO: Add movement?
    void Start () {
        blaster = GetComponent<BulletGenerator>();
        direction = new Vector2(dirX, dirY);
	}
	
	void FixedUpdate () {
        fireTimer += Time.fixedDeltaTime;
        if (fireTimer >= fireRate) {
            blaster.Fire(direction);
            fireTimer = 0.0f;
        }
            
	}
}
