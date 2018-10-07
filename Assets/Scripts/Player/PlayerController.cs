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

    // Defaults to PlayerOne, set PlayerTwo to use those axes
    [SerializeField]
    public string axisName = "PlayerOne";
    private string controlAxis = "Joystick";

    public PlayerInputManager inputManager;

    protected float jumpVelocity = 0f;
    private bool isFacingRight = true;

    private SpriteRenderer spriteRenderer;
    private Animator anim;

    void Awake()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.anim = GetComponent<Animator>();
    }

    void Start()
    {
        this.contactFilter.useTriggers = false;
        this.contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(this.gameObject.layer));
        this.contactFilter.useLayerMask = true;

        //float gravity = -1f * (2f * this.jumpHeight) / (this.jumpApexTime * this.jumpApexTime);
        // @TODO causes issues with multiplayer games move to a global entity
        // setting the timesclae r2bd is undefined, if we grab it here then the parent scribt has issues with jumping
        //this.rb2d.gravityScale = gravity;
        //Physics2D.gravity = new Vector2(0f, gravity); 
        this.jumpVelocity = Mathf.Sqrt(-2f * Physics2D.gravity.y * jumpHeight);

        if(Input.GetJoystickNames().Length == 0 ) {
            controlAxis = axisName + "_Keyboard";
        } else {
            int controllerIndex = (axisName == "PlayerOne") ? 0 : 1;
            if (Input.GetJoystickNames()[controllerIndex] != null)
            {
                Debug.Log("Using controller for " + axisName);
                controlAxis = axisName + "_Joystick";
            }
            else
            {
                Debug.Log("Using keyboard for " + axisName);
                controlAxis = axisName + "_Keyboard";
            }
        }
    }

    protected void Update()
    {
        if (!this.isAlive) return;

        this.velocity.x = Input.GetAxis("Horizontal_" + controlAxis);

        if (this.inputManager.Jump() && this.IsGrounded())
        {
            this.velocity.y = this.jumpVelocity;
        }
        else if (this.inputManager.Jump())
        {
            if (this.velocity.y > 0)
            {
                this.velocity.y = this.velocity.y * this.jumpRelease;
            }
        }

        base.Update();

        this.anim.SetBool("IsShooting", false);
        this.anim.SetBool("IsThrowing", false);
        this.anim.SetBool("IsHurt", false);

        if (Input.GetAxis("Vertical_" + controlAxis) > 0.5f && inputManager.Fire())
        {
            this.anim.SetBool("IsThrowing", true);
        }
        else if (inputManager.Fire())
        {
            this.anim.SetBool("IsShooting", true);
        }

        this.anim.SetBool("IsRunning", Mathf.Abs(this.velocity.x) > 0.1);
        this.anim.SetBool("IsGrounded", this.IsGrounded());
        this.anim.SetFloat("JumpVelocity", Mathf.Abs(this.velocity.y));
    }

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        move.x = this.velocity.x;

        if ((move.x > 0.01f && !this.isFacingRight) || (move.x < -0.01f && this.isFacingRight))
        {
            //this.spriteRenderer.flipX = !this.spriteRenderer.flipX;
            this.isFacingRight = !this.isFacingRight;
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

    protected void Throw()
    {
        GetComponentInChildren<BombLauncher>().Fire(new Vector2(this.isFacingRight ? 1f : -1f, 3f));
    }

    protected void Shoot()
    {
        Vector2 fireDirection = new Vector2(this.isFacingRight ? 1f : -1f, 0f);
        GetComponentInChildren<BulletGenerator>().Fire(fireDirection);
    }

    public PlayerHealth GetHealth()
    {
        return this.health;
    }
}