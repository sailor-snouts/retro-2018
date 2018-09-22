using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombLauncher : MonoBehaviour {
    public GameObject bomb;

    public void Fire(Vector2 direction)
    {
        GameObject bomb = Instantiate(this.bomb, this.transform);
        bomb.GetComponent<Bomb>().SetDirection(direction);
    }
}
