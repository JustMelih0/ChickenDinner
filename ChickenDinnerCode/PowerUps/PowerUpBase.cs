using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUpBase : ScriptableObject
{
    public string powerUpName; 
    public Sprite icon;
    public float duration;
    public float dropChance;

    
    public abstract void Apply(GameObject target, GameObject sendObject);
    
    protected abstract IEnumerator PowerUpWithDuration();
}
