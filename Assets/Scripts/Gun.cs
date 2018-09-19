using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {
    public GameObject bullet;
	
	public void Fire()
    {
        GameObject bullet = Instantiate(this.bullet);
        bullet.transform.position = this.transform.position;
    }
}
