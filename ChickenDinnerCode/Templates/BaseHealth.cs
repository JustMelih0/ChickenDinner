using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseHealth : MonoBehaviour,IA_Hitable
{
    [SerializeField]protected float maxHealth;
    protected float currentHealth;
    public virtual void Hit(float damage, bool condition)
    {
        currentHealth-=damage;
        currentHealth=Mathf.Clamp(currentHealth,0,maxHealth);

        if (currentHealth<=0)
        {
            Death();        
        }
    }
    public virtual void Death()
    {
        currentHealth=maxHealth;
    }

   
}
