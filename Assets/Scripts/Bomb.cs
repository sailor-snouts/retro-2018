using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Bomb : Bullet {
    [SerializeField]
    protected float apexHeight = 2f;

    [SerializeField]
    protected float apexTime = 5f;

    protected Rigidbody2D rb2d;

    private void OnEnable()
    {
        this.rb2d = GetComponent<Rigidbody2D>();

        rb2d.velocity = this.direction.normalized * velocity;
    }

    private void Update()
    {
        
    }
}
