using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastDoorController : MonoBehaviour {

    [SerializeField]
    GameObject doorTile = null;
    [SerializeField]
    int doorSizeH = 5;
    [SerializeField]
    float openDuration = 1.0f;

    bool closed = true;
    float openTimer = 1.0f;
    List<GameObject> doorTiles = null;
    int closeCount = 0;

    void Start () {

        doorTiles = new List<GameObject>(doorSizeH);

        for (int i = 0; i < doorSizeH; i++ ) {
            GameObject doorPiece = Instantiate(doorTile, this.transform);
            doorPiece.transform.position += new Vector3(0, -i, 0);
            doorTiles.Add(doorPiece);
        }	
	}

    // TODO: could care about the weapon type here if we want to create special kinds of doors!
    internal void Blast(float dmg)
    {
        if (!closed)
            return;

        closed = false;
        closeCount = doorSizeH - 1;
        openTimer = openDuration / doorSizeH;

        Invoke("RemoveBlastDoor", openTimer);
    }

    void RemoveBlastDoor() {
        doorTiles[closeCount].SetActive(false);
        closeCount--;
        if (closeCount < 0)
            Destroy(gameObject);
        else 
            Invoke("RemoveBlastDoor", openTimer);
    }
}
