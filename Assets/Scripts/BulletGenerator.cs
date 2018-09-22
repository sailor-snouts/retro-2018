using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGenerator : MonoBehaviour {
    public GameObject bullet;
	
	public void Fire(Vector2 direction)
    {
        GameObject bullet = Instantiate(this.bullet);
        bullet.transform.position = this.transform.position;
        bullet.GetComponent<Bullet>().SetDirection(direction);

        bullet.transform.Rotate(Vector3.forward, Vector2.Angle(Vector2.right, direction));
    }
}
