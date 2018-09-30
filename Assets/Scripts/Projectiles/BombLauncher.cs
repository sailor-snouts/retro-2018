using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombLauncher : MonoBehaviour {
    public GameObject bomb;

    public void Fire(Vector2 direction)
    {
        GameObject bomb = Instantiate(this.bomb);
        bomb.transform.position = this.transform.position;
        bomb.GetComponent<Bomb>().SetDirection(direction);
    }
}
