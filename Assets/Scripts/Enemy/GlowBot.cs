using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowBot : MonoBehaviour {

    [SerializeField]
    float fireRate = 0.1f;
    public float amplitude = 10f;
    public float period = 5f;

    Vector3 startPos;
    BulletGenerator blaster;
    float fireTimer;
    int fireDirection = 2;

    void Start()
    {
        blaster = GetComponent<BulletGenerator>();
        startPos = transform.position;
    }

    void FixedUpdate()
    {
        fireTimer += Time.fixedDeltaTime;
        if (fireTimer >= fireRate)
        {
            Vector2 direction = Vector2.down;

            switch(fireDirection) {
                case 0: direction = Vector2.up; break;
                case 1: direction = Vector2.right; break;
                case 2: direction = Vector2.down; break;
                case 3: direction = Vector2.left; break;
                default: break;
            }

            blaster.Fire(direction);
            fireTimer = 0.0f;
            fireDirection++;
            fireDirection %= 4;
        }
    }

    protected void Update()
    {
        float theta = Time.timeSinceLevelLoad / period;
        float distance = amplitude * Mathf.Sin(theta);
        transform.position = startPos + Vector3.right * distance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Triggered by " + collision.gameObject.tag);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collided with " + collision.gameObject.tag);
    }
}
