using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Bullet : MonoBehaviour {
    [SerializeField]
    protected Vector2 direction;
    [SerializeField]
    protected float velocity = 1f;
    [SerializeField]
    protected float dmg = 1f;
    [SerializeField]
    protected bool isFriendly = true;
    [SerializeField]
    protected float killAfter = 1f;

    protected Animator anim;


    public void OnEnable()
    {
        this.anim = GetComponent<Animator>();
    }

    public void SetDirection(Vector2 dir)
    {
        this.direction = dir.normalized;
    }

    void Update()
    {
        this.killAfter -= Time.deltaTime;
        if(this.killAfter < 0f)
        {
            this.Hit();

            return;
        }
        this.transform.position = (Vector2) this.transform.position + this.direction.normalized * this.velocity * Time.deltaTime;
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.isFriendly && collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyHealth>().Hurt(this.dmg);
            this.Hit();
        }
        else if (!this.isFriendly && collision.gameObject.tag == "Player")
        {
            collision.GetComponent<PlayerController>().GetHealth().Hurt(this.dmg);
            this.Hit();
        }
    }

    protected void Hit()
    {
        this.velocity = 0;
        this.anim.SetBool("IsHit", true);
    }

    protected void Die()
    {
        Destroy(this.gameObject);
    }
}
