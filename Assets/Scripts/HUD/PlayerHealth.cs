using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public SpriteMask mask;
    [SerializeField]
    private float maxHealth = 100;
    [SerializeField]
    private float currentHealth = 100;
    [SerializeField]
    private float previousHealth = 100;

    public void OnEnable()
    {
        this.currentHealth = this.maxHealth;
        this.previousHealth = this.currentHealth;
        this.mask = GetComponentInChildren<SpriteMask>();
    }

    void Update()
    {
        if (this.currentHealth == this.previousHealth) return;

        float scale = Mathf.Clamp(this.currentHealth / this.maxHealth, 0, this.maxHealth);
        this.mask.transform.localScale = new Vector3(1f, scale, 1f);
        this.previousHealth = this.currentHealth;
    }

    public bool IsAlive()
    {
        return this.HasEnoughHealth(0f);
    }

    public bool HasEnoughHealth(float amt)
    {
        return this.currentHealth > amt;
    }

    public void GainHealth(float amt)
    {
        this.Hurt(amt * -1);
    }

    public void Hurt(float amt)
    {
        this.currentHealth = Mathf.Clamp(this.currentHealth - amt, 0, this.maxHealth);
    }
}
