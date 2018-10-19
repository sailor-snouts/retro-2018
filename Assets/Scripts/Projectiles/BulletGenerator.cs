using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGenerator : MonoBehaviour {
    public GameObject bulletPrefab;
    public bool isFriendly = true;
    public float damage = 1.0f;

	public void Fire(Vector2 direction)
    {
        GameObject bulletObj = Instantiate(this.bulletPrefab);
        Bullet bullet = bulletObj.GetComponent<Bullet>();
        bullet.isFriendly = this.isFriendly;
        bullet.dmg = this.damage;
        bullet.transform.position = this.transform.position;
        bullet.GetComponent<Bullet>().SetDirection(direction);

        bullet.transform.Rotate(Vector3.forward, Vector2.Angle(Vector2.right, direction));
    }
}
