
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PlantController))]
public abstract class PlantHealthBase : BaseHealth
{

   protected PlantController plantController;
   [SerializeField]protected PlantBase plantBase;
   [SerializeField]protected PowerUpBase powerUp;

   


    private void Start() 
    {
        plantController=GetComponent<PlantController>();
        currentHealth=maxHealth; 
    }


    public override void Hit(float damage, bool condition)
    {
        base.Hit(damage, condition);
        if (condition)
        {
            SpawnItem();
        }
    }
    public override void Death()
    {
        base.Death();
        plantBase.HarvestPlant();

    }
    public virtual void SpawnItem()
    {
        AudioManager.Instance.PlaySFX("Harvest");
        GameObject item = PoolManager.Instance.SpawnFromPool("Ability",transform.position,Quaternion.identity);
        item.GetComponent<ItemBase>().powerUp = powerUp;
        item.GetComponent<ItemBase>().SpawnDropped();
    }

}
