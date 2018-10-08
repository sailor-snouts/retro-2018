using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Animator))]
public class Bomb : Bullet {
    protected Rigidbody2D rb2d;

    [SerializeField]
    protected float delay = 2f;

    [SerializeField]
    protected float radius = 1f;

    private void OnEnable()
    {
        base.OnEnable();
        this.rb2d = GetComponent<Rigidbody2D>();
        this.SetDirection(this.direction);
    }

    void Update()
    {
        // do nothing, overwrite parent
    }

    public void SetDirection(Vector2 dir)
    {
        this.direction = dir.normalized;
        rb2d.velocity = this.direction.normalized * this.velocity;
    }

    private void FixedUpdate()
    {
        if (Navigation.IsPaused()) return;

        this.delay -= Time.fixedDeltaTime;
        if(this.delay < 0f)
        {
            this.StartExplosion();
        }
    }

    protected void StartExplosion()
    {
        this.anim.SetBool("IsExplode", true);
    }

    protected void Explode()
    {
        this.rb2d.gravityScale = 0;
        Collider2D[] collisions = Physics2D.OverlapCircleAll(this.transform.position, this.radius);
        foreach(Collider2D collision in collisions)
        {
            if (this.isFriendly && collision.gameObject.tag == "Enemy")
            {
                collision.gameObject.GetComponent<EnemyHealth>().Hurt(this.dmg);
            }
            else if (!this.isFriendly && collision.gameObject.tag == "Player")
            {
                collision.GetComponent<PlayerController>().GetHealth().Hurt(this.dmg);
            }
        }
    }
}
