using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraLifePickup : MonoBehaviour {
    [SerializeField]
    int extraLives = 1;

    public int ExtraLives() {
        return extraLives;
    }
}
