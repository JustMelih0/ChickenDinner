using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralMobHealth : MobHealth
{
    public override void Death()
    {
        base.Death();
    }
    public override void DestroyMob()
    {
        base.DestroyMob();
    }
    public override void Hit(float damage, bool condition)
    {
        base.Hit(damage, condition);
    }
    protected override void JumpEffect()
    {
        base.JumpEffect();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
    }
    public override void GiveSomething()
    {
        base.GiveSomething();
    }

}
