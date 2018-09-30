using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PhysicsEntity : MonoBehaviour
{
    [SerializeField]
    private float minGroundNormalY = 0.65f;

    protected float minMoveDistance = 0.001f;
    protected float shellRadius = 0.01f;

    protected Vector2 targetVelocity;
    protected bool grounded;
    protected Vector2 groundNormal;
    protected Rigidbody2D rb2d;
    protected Vector2 velocity;
    protected ContactFilter2D contactFilter;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);

    void OnEnable()
    {
        this.rb2d = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        this.contactFilter.useTriggers = false;
        this.contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(this.gameObject.layer));
        this.contactFilter.useLayerMask = true;
    }

    protected void Update()
    {
        this.targetVelocity = Vector2.zero;
        ComputeVelocity();
    }

    protected virtual void ComputeVelocity()
    {

    }

    protected void FixedUpdate()
    {
        this.velocity += Physics2D.gravity * Time.fixedDeltaTime;
        this.velocity.x = this.targetVelocity.x;

        this.grounded = false;

        Vector2 deltaPosition = this.velocity * Time.fixedDeltaTime;
        Vector2 moveAlongGround = new Vector2(this.groundNormal.y, - this.groundNormal.x);
        Vector2 move = moveAlongGround * deltaPosition.x;

        Movement(move, false);

        move = Vector2.up * deltaPosition.y;

        Movement(move, true);
    }

    void Movement(Vector2 move, bool yMovement)
    {
        float distance = move.magnitude;

        if (distance > this.minMoveDistance)
        {
            int count = rb2d.Cast(move, this.contactFilter, this.hitBuffer, distance + this.shellRadius);
            this.hitBufferList.Clear();
            for (int i = 0; i < count; i++)
            {
                this.hitBufferList.Add(this.hitBuffer[i]);
            }

            for (int i = 0; i < this.hitBufferList.Count; i++)
            {
                Vector2 currentNormal = this.hitBufferList[i].normal;
                if (currentNormal.y > this.minGroundNormalY)
                {
                    this.grounded = true;
                    if (yMovement)
                    {
                        this.groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }

                float projection = Vector2.Dot(this.velocity, currentNormal);
                if (projection < 0)
                {
                    this.velocity = this.velocity - projection * currentNormal;
                }

                float modifiedDistance = this.hitBufferList[i].distance - this.shellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }


        }
        
        this.rb2d.position = this.rb2d.position + move.normalized * distance;
    }

    public bool IsGrounded()
    {
        return this.grounded;
    }
}
