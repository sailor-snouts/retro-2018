using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerHealth))]
[RequireComponent(typeof(PlayerLives))]
public class PlayerController : PhysicsEntity
{
    public PlayerHealth health;
    public PlayerLives lives;

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
    protected Vector2 knockBackDirection = new Vector2(-1f, 2f);

    [SerializeField]
    protected float knockBackForce = 100f;

    [SerializeField]
    protected float knockBackInvincability = 0.25f;

    [SerializeField]
    protected float knockBackInvincabilityTimer = 0f;

    [SerializeField]
    protected int player = 1;

    [SerializeField]
    private GameObject deathExplosion;

    protected SpriteRenderer spriteRenderer;
    protected Animator anim;

    void Awake()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.anim = GetComponent<Animator>();
    }

   virtual public void Jump()
    {
        if (this.IsGrounded())
        {
            this.velocity.y = this.jumpVelocity;
        }
    }

    virtual public void JumpRelease()
    {
        if (this.velocity.y > 0f)
        {
            this.velocity.y = this.velocity.y * this.jumpRelease;
        }
    }

    virtual public void Attack1()
    {
        this.anim.SetBool("IsShooting", true);
    }

    virtual public void FinishAttak1()
    {
        this.anim.SetBool("IsShooting", false);
    }

    virtual public void Attack2()
    {
        this.anim.SetBool("IsThrowing", true);
    }

    virtual public void FinishAttak2()
    {
        this.anim.SetBool("IsThrowing", false);
    }

    virtual public void Walk(float direction)
    {
        this.velocity.x = Mathf.Clamp(direction, -1f, 1f);
    }

    virtual public void Hurt()
    {
        this.anim.SetBool("IsHurt", true);
    }

    virtual public void Hurt(float dmg)
    {
        this.health.Hurt(dmg);
        this.anim.SetBool("IsHurt", true);
        this.Knockback();
    }

    virtual protected void Knockback()
    {
        this.knockBackInvincabilityTimer = this.knockBackInvincability;
        this.rb2d.velocity = this.knockBackDirection.normalized * this.knockBackForce;
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
        if (!this.health.IsAlive()) {
            HandlePlayerDeath();
            return;
        }

        base.Update();

        if (this.anim.GetBool("IsHurt"))
        {
            this.knockBackInvincabilityTimer -= Time.deltaTime;
            Debug.Log("invincability!");
            if(this.knockBackInvincabilityTimer <= 0)
            {
                Debug.Log("Mortality :(");
                this.anim.SetBool("IsHurt", false);
                this.rb2d.velocity = new Vector2(0f, 0f);
            }
        }
        
        this.anim.SetBool("IsRunning", Mathf.Abs(this.velocity.x) > 0.1);
        this.anim.SetBool("IsGrounded", this.IsGrounded());
        this.anim.SetFloat("JumpVelocity", Mathf.Abs(this.velocity.y));
    }

    protected override void ComputeVelocity()
    {
        if(this.anim.GetBool("IsHurt"))
        {
            return;
        }

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
            this.Hurt(1f);
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
        foreach(PlayerHealth ph in FindObjectsOfType<PlayerHealth>())
        {
            if(this.getPlayerNumber() == ph.getPlayerNumber())
            {
                this.health = ph;
            }
        }
        foreach (PlayerLives pl in FindObjectsOfType<PlayerLives>())
        {
            if (this.getPlayerNumber() == pl.getPlayerNumber())
            {
                this.lives = pl;
            }
        }

        return this;
    }

    private void HandlePlayerDeath() {
        // TODO: Handle properly
        //  - pause update function
        //  - trigger explosion/sounds
        //  - invoke next step after explosion animation is complete
        deathExplosion.SetActive(true);
        Invoke("OnDeath", 0.3f);
    }

    void OnDeath() {
        GameManager.instance.PlayerDeath(this.player);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if( collision.gameObject.tag == "VoidCollider") {
            HandlePlayerDeath();
        }

        if( collision.gameObject.tag == "ExtraLife") {
            GameManager.instance.GainLives(player);
            Destroy(collision.gameObject);
        }
    }
}