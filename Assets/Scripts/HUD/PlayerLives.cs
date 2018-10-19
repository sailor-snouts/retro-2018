using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLives : MonoBehaviour
{
    [SerializeField]
    protected int player = 1;
    [SerializeField]
    SpriteRenderer[] lives;

    void Update()
    {
        int playerLives = GameManager.instance.PlayerLives(player);

        for (int i = 0; i < lives.Length; i++)
        {
            //Debug.Log("Setting enabled on life " + i + " = " + (i < playerLives));
            lives[i].enabled = (i < playerLives);
        }
    }

    public int getPlayerNumber()
    {
        return this.player;
    }
}
