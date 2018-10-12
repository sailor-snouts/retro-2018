using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
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

    [SerializeField]
    protected float jumpVelocity = 0f;

    [SerializeField]
    protected int player = 1;

    private SpriteRenderer spriteRenderer;
    private Animator anim;

    void Awake()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.anim = GetComponent<Animator>();
    }

    public void Jump()
    {
        if (this.IsGrounded())
        {
            this.velocity.y = this.jumpVelocity;
        }
    }

    public void JumpRelease()
    {
        if (this.velocity.y > 0)
        {
            this.velocity.y = this.velocity.y * this.jumpRelease;
        }
    }

    public void Attack1()
    {
        Vector2 fireDirection = new Vector2(transform.localScale.x, 0f);
        GetComponentInChildren<BulletGenerator>().Fire(fireDirection);
        this.anim.SetBool("IsShooting", true);
    }

    public void FinishAttak1()
    {
        this.anim.SetBool("IsShooting", false);
    }

    public void Attack2()
    {
        GetComponentInChildren<BombLauncher>().Fire(new Vector2(transform.localScale.x, 3f));
        this.anim.SetBool("IsThrowing", true);
    }

    public void FinishAttak2()
    {
        this.anim.SetBool("IsThrowing", false);
    }

    public void Walk(float direction)
    {
        this.velocity.x = Mathf.Clamp(direction, -1f, 1f);
    }

    protected void Start()
    {
        base.Start();

        float gravity = -1f * (2f * this.jumpHeight) / (this.jumpApexTime * this.jumpApexTime);
        Physics2D.gravity = new Vector2(0f, gravity); 
        this.jumpVelocity = Mathf.Sqrt(2f * -1f * Physics2D.gravity.y * jumpHeight);
    }

    protected void Update()
    {
        if (!this.isAlive) return;

        base.Update();
        
        this.anim.SetBool("IsRunning", Mathf.Abs(this.velocity.x) > 0.1);
        this.anim.SetBool("IsGrounded", this.IsGrounded());
        this.anim.SetFloat("JumpVelocity", Mathf.Abs(this.velocity.y));
    }

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        move.x = this.velocity.x;

        if ((move.x > 0.01f && transform.localScale.x < 0) || (move.x < -0.01f && transform.localScale.x > 0))
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }

        this.targetVelocity = move * this.maxSpeed;
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            this.health.Hurt(5f);
            this.anim.SetBool("IsHurt", true);
        }
    }

    public PlayerHealth GetHealth()
    {
        return this.health;
    }

    public int getPlayerNumber()
    {
        return this.player;
    }

    public PlayerController SetPlayerNumber(int number)
    {
        this.player = number;

        return this;
    }
}