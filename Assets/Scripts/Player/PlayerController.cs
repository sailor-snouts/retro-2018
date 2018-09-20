using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerController : PhysicsEntity
{
    public PlayerHealth health;

    [SerializeField]
    private bool isAlive = true;

    [SerializeField]
    private float maxSpeed = 7f;

    [SerializeField]
    public float jumpHeight = 5f;

    [SerializeField]
    public float jumpApexTime = 0.5f;

    [SerializeField]
    public float jumpRelease = 0.5f;

    protected float jumpVelocity = 0f;
    private bool isFacingRight = true;

    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        this.contactFilter.useTriggers = false;
        this.contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(this.gameObject.layer));
        this.contactFilter.useLayerMask = true;

        float gravity = -1f * (2f * this.jumpHeight) / (this.jumpApexTime * this.jumpApexTime);
        // @TODO causes issues with multiplayer games move to a global entity
        // setting the timesclae r2bd is undefined, if we grab it here then the parent scribt has issues with jumping
        //this.rb2d.gravityScale = gravity;
        Physics2D.gravity = new Vector2(0f, gravity); 
        this.jumpVelocity = Mathf.Sqrt(-2f * gravity * jumpHeight);
    }

    protected override void ComputeVelocity()
    {
        if (!this.isAlive) return;

        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            Vector2 fireDirection = new Vector2(this.isFacingRight ? 1f : -1f, 0f);
            GetComponentInChildren<Gun>().Fire(fireDirection);
        }

        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space) && this.grounded)
        {
            this.velocity.y = this.jumpVelocity;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            if (this.velocity.y > 0)
            {
                this.velocity.y = this.velocity.y * this.jumpRelease;
            }
        }

        if ((move.x > 0.01f && !this.isFacingRight) || (move.x < -0.01f && this.isFacingRight))
        {
            this.spriteRenderer.flipX = !this.spriteRenderer.flipX;
            this.isFacingRight = !this.isFacingRight;

        }

        this.targetVelocity = move * this.maxSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            this.health.Hurt(5f);
        }
    }

    public PlayerHealth GetHealth()
    {
        return this.health;
    }
}