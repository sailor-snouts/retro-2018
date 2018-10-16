using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectProjectile : MonoBehaviour {
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            collision.transform.localScale = new Vector3(-1 * collision.transform.localScale.x, collision.transform.localScale.y, collision.transform.localScale.z);
        }
    }
}
