using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheapVictoryController : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if( collision.gameObject.tag == "Player") {
            Debug.Log("YAY YOU DID IT");
            GameManager.instance.Victory();
        }
    }

}
