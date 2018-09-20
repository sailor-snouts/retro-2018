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


    public void SetDirection(Vector2 dir)
    {
        this.direction = dir.normalized;
    }

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
            collision.gameObject.GetComponent<EnemyHealth>().Hurt(this.dmg);
            Destroy(this.gameObject);
        }
        else if (!this.isFriendly && collision.gameObject.tag == "Player")
        {
            collision.GetComponent<PlayerController>().GetHealth().Hurt(this.dmg);
            Destroy(this.gameObject);
        }
    }
}
