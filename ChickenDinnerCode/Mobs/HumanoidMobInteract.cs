using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidMobInteract : MobInteractBase
{
    protected override void Interact(Collider2D target)
    {
        base.Interact(target);
    }
    protected override void InteractControl()
    {
        base.InteractControl();
    }
    protected override void ResetInteract()
    {
        base.ResetInteract();
    }

    protected override Collider2D CheckArea()
    {
        return base.CheckArea();
    }
}
