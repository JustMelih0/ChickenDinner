using System;
using System.Collections;
using UnityEngine;

public abstract class MobHealth : BaseHealth
{
    [SerializeField]protected float destroyTime;
    [SerializeField]protected int addScore;
    [SerializeField]protected SpawnManager.Mobs mobType;

    protected Rigidbody2D rigidBody2D;
    protected MobController mobController;
    protected Collider2D Collider2D;
    protected Animator anim;


    public static event Action<SpawnManager.Mobs> OnMobDeath;
    private void Start() 
    {
        anim = GetComponent<Animator>();
        rigidBody2D=GetComponent<Rigidbody2D>();
    }
    public override void Hit(float damage, bool condition)
    {
        base.Hit(damage, condition);
        if (condition)
        {
            GiveSomething();
        }
    }
    protected virtual void OnEnable() {
        mobController=GetComponent<MobController>();
        Collider2D=GetComponent<Collider2D>();
        mobController.canMove=true;
        mobController.canInteract=true;
        mobController.Interactable=true;
        Collider2D.enabled=true;
    }
    public override void Death()
    {
        anim.SetBool("isFalling",true);
        base.Death();
        JumpEffect();
        mobController.canMove=false;
        mobController.canInteract=false;
        mobController.Interactable=false;
        Collider2D.enabled=false;


        Invoke(nameof(DestroyMob),destroyTime);
        
    }
    public virtual void GiveSomething()
    {
        OnMobDeath?.Invoke(mobType);
        AudioManager.Instance.PlaySFX("AnimalDead");
        LevelManager.Instance.SetScore(addScore);
    }
    public virtual void DestroyMob()
    {
        anim.SetBool("isFalling",false);
        gameObject.SetActive(false);
    }
    protected virtual void JumpEffect()
    {
       rigidBody2D.AddForce(Vector2.up*200);
    }
   
    
}
