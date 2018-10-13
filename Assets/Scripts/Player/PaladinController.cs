using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaladinController : PlayerController
{
    protected bool isAttack1Locked = false;
    protected bool isAttack2Locked = false;

    override public void Attack1()
    {
        if(this.isAttack2Locked)
        {
            return;
        }

        this.anim.SetBool("IsShooting", true);
        this.isAttack2Locked = true;
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

        this.anim.SetBool("IsThrowing", true);
    }

    override public void FinishAttak2()
    {
        this.anim.SetBool("IsThrowing", false);
        this.isAttack2Locked = false;
    }
}
