using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLives : MonoBehaviour
{
    [SerializeField]
    private float maxLives = 3;
    [SerializeField]
    private float currentLives = 3;
    [SerializeField]
    private float previousLives = 3;
    [SerializeField]
    protected int player = 1;
    [SerializeField]
    SpriteRenderer[] lives;

    public void OnEnable()
    {
        this.currentLives = this.maxLives;
        this.previousLives = this.currentLives;
    }

    void Update()
    {
        if (this.currentLives == this.previousLives) return;

        this.previousLives = this.currentLives;

        for (int i = 0; i < maxLives; i++)
        {
            lives[i].enabled = (i < currentLives);
        }
    }

    public bool IsGameOver()
    {
        return this.currentLives <= Mathf.Epsilon;
    }

    public void GainLives(float amt)
    {
        this.LoseLives(amt * -1);
    }

    public void LoseLives(float amt)
    {
        this.currentLives = Mathf.Clamp(this.currentLives - amt, 0, this.maxLives);
    }

    public int getPlayerNumber()
    {
        return this.player;
    }
}
