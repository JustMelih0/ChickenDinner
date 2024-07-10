using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropableMobHealth : MobHealth
{
    [SerializeField]protected PowerUpBase powerUp;
    [SerializeField]protected List<PowerUpBase> dropablePowerUps;
    public override void Death()
    {
        base.Death();
    }
    public override void DestroyMob()
    {
        base.DestroyMob();
    }
    public virtual void SpawnPowerUp()
    {
       GameObject tempPowerUp= PoolManager.Instance.SpawnFromPool("Ability",transform.position,Quaternion.identity);
       tempPowerUp.GetComponent<ItemBase>().powerUp = GetRandomPowerUp();
       tempPowerUp.GetComponent<ItemBase>().SpawnDropped();
    }
    private PowerUpBase GetRandomPowerUp()
    {
        float totalChance = 0f;
        foreach (var powerUp in dropablePowerUps)
        {
            totalChance += powerUp.dropChance;
        }

        float randomValue = Random.Range(0f, totalChance);
        float cumulativeChance = 0f;

        foreach (var powerUp in dropablePowerUps)
        {
            cumulativeChance += powerUp.dropChance;
            if (randomValue <= cumulativeChance)
            {
                return powerUp;
            }
        }

        
        return null;
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
        SpawnPowerUp();
        base.GiveSomething();

    }

}
