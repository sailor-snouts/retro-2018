using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnergy : MonoBehaviour
{
    [SerializeField]
    private float maxEnergy = 100;
    [SerializeField]
    private float currentEnergy = 100;
    private SpriteRenderer[] bars;
    private int barCount;
    private int barsDisplayed;

    private void Awake()
    {
        this.bars = this.gameObject.GetComponentsInChildren<SpriteRenderer>();
        this.barCount = this.bars.Length;
        this.barsDisplayed = this.bars.Length;
        this.currentEnergy = this.maxEnergy;
    }

    void Update ()
    {
        int barsToDisplay = Mathf.FloorToInt(this.currentEnergy / this.maxEnergy * (float) this.barCount);
        if (barsToDisplay == this.barsDisplayed) return;
        int i = 1;
        foreach(SpriteRenderer bar in this.bars)
        {
            bar.enabled = (i++ <= barsToDisplay);
        }
        this.barsDisplayed = barsToDisplay;

    }

    public bool IsAlive()
    {
        return this.HasEnoughEnergy(0f);
    }

    public bool HasEnoughEnergy(float amt)
    {
        return this.currentEnergy > amt;
    }

    public void GainEnergy(float amt)
    {
        this.UseEnergy(amt * -1);
    }

    public void UseEnergy(float amt)
    {
        this.currentEnergy = Mathf.Clamp(this.currentEnergy + amt, 0, this.maxEnergy);
    }
}
