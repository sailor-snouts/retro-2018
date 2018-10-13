using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaladinController : PlayerController
{
    override public void Attack1()
    {
        this.anim.SetBool("IsShooting", true);
    }

    override public void FinishAttak1()
    {
        this.anim.SetBool("IsShooting", false);
    }

    override public void Attack2()
    {
        this.anim.SetBool("IsThrowing", true);
    }

    override public void FinishAttak2()
    {
        this.anim.SetBool("IsThrowing", false);
    }
}
