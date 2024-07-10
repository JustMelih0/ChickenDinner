using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralPlantHealth : PlantHealthBase
{
    public override void Hit(float damage, bool condition)
    {
        base.Hit(damage, condition);
    }
    public override void Death()
    {
        base.Death();
    }
    public override void SpawnItem()
    {
        base.SpawnItem();
    }
}
