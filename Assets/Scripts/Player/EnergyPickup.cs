using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPickup : MonoBehaviour {

    [SerializeField]
    int energyAmount = 10;

    public int EnergyAmount() {
        return energyAmount;
    }
}
