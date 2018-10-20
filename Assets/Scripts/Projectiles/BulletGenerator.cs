using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGenerator : MonoBehaviour {
    public GameObject bulletPrefab;
    public bool isFriendly = true;
    public float damage = 1.0f;

    private AudioSource bulletSfx;
    private List<GameObject> cache;


    private void Start()
    {
        bulletSfx = GetComponent<AudioSource>();
        cache = new List<GameObject>();
    }

    public void Fire(Vector2 direction)
    {

        bulletSfx.Play();
        GameObject bulletObj;

        if( cache.Count > 0 ) {
            bulletObj = cache[0];
            cache.RemoveAt(0);
            bulletObj.SetActive(true);
        } else {
            bulletObj = Instantiate(this.bulletPrefab);
        }

        Bullet bullet = bulletObj.GetComponent<Bullet>();
        bullet.gun = this;
        bullet.isFriendly = this.isFriendly;
        bullet.dmg = this.damage;
        bullet.transform.position = this.transform.position;
        bullet.GetComponent<Bullet>().SetDirection(direction);

        bullet.transform.Rotate(Vector3.forward, Vector2.Angle(Vector2.right, direction));
    }

    internal void ReturnToCache(GameObject bulletObj)
    {
        bulletObj.transform.SetPositionAndRotation(this.transform.position, Quaternion.identity);
        cache.Add(bulletObj);
    }
}
