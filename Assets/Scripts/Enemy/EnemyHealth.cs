﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private float maxHealth = 5;
    [SerializeField]
    private float currentHealth = 5;
    [SerializeField]
    private GameObject deathExplosion;

    private void OnEnable()
    {
        if (this.currentHealth <= 0)
        {
            this.currentHealth = this.maxHealth;
        }
    }

    private void Update()
    {
        if(!this.IsAlive())
        {
            deathExplosion.SetActive(true);
            Invoke("OnDeath", 1.0f);
        }
    }

    void OnDeath() {
        Destroy(this.gameObject);
    }

    public bool IsAlive()
    {
        return this.currentHealth > 0f;
    }

    public void GainHealth(float amt)
    {
        this.Hurt(amt * -1);
    }

    public void Hurt(float amt)
    {
        Debug.Log("Enemy hurt by " + amt);
        this.currentHealth = Mathf.Clamp(this.currentHealth - amt, 0, this.maxHealth);
    }
}
