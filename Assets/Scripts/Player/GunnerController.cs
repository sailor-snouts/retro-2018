using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunnerController : PlayerController
{
    protected bool isAttack1Locked = false;
    protected bool isAttack2Locked = false;

    override public void Attack1()
    {
        if (this.isAttack1Locked)
        {
            return;
        }

        Vector2 fireDirection = new Vector2(transform.localScale.x, 0f);
        GetComponentInChildren<BulletGenerator>().Fire(fireDirection);
        this.anim.SetBool("IsShooting", true);
    }

    override public void FinishAttak1()
    {
        this.anim.SetBool("IsShooting", false);
        this.isAttack1Locked = false;
    }

    override public void Attack2()
    {
        if (this.isAttack2Locked)
        {
            return;
        }

        GetComponentInChildren<BombLauncher>().Fire(new Vector2(transform.localScale.x, 3f));
        this.anim.SetBool("IsThrowing", true);
    }

    override public void FinishAttak2()
    {
        this.anim.SetBool("IsThrowing", false);
        this.isAttack2Locked = false;
    }
}
