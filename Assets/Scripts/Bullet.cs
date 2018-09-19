using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    [SerializeField]
    private Vector2 direction;
    [SerializeField]
    private float velocity = 1f;
    [SerializeField]
    private float dmg = 1f;
    [SerializeField]
    private bool isFriendly = true;
    [SerializeField]
    private float killAfter = 1f;
    
    void Update()
    {
        this.killAfter -= Time.deltaTime;
        if(this.killAfter < 0f)
        {
            Destroy(this.gameObject);

            return;
        }
        this.transform.position = (Vector2) this.transform.position + this.direction.normalized * this.velocity * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.isFriendly && collision.gameObject.tag == "Enemy")
        {
            // hurt enemy
            Destroy(this.gameObject);
        }
        else if (!this.isFriendly && collision.gameObject.tag == "Player")
        {
            // hurt player
            Destroy(this.gameObject);
        }
    }
}
