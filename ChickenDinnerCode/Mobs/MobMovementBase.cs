using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MobController))]
public abstract class MobMovementBase : MonoBehaviour
{
    [SerializeField]protected float speed;
    protected float currentSpeed;
    [Range(-1,1)][SerializeField]protected int moveDirection;


   #region Components

     protected Rigidbody2D rigidBody2D;
     
     protected Animator anim;

     protected MobController mobController;

   #endregion

    protected virtual void Start()
    {
      mobController = GetComponent<MobController>();
      rigidBody2D = GetComponent<Rigidbody2D>();
      anim = GetComponent<Animator>();
      currentSpeed = speed;
    }

    protected virtual void  FixedUpdate()
    {
         Move();
         Anime();
    }
    public virtual void Anime()
    {
       anim.SetBool("canMove",mobController.canMove);
    }
    public virtual void SetDirection(int direction)
    {
       moveDirection=direction;
       CharacterFlip();
    }

    protected virtual void Move()
    {
       if(mobController.canMove==false)
       {
            rigidBody2D.velocity=new Vector2(0,rigidBody2D.velocity.y);
            return;
       }

       rigidBody2D.velocity=new Vector2(currentSpeed*moveDirection,rigidBody2D.velocity.y);
    }
    protected virtual void CharacterFlip()
    {
       transform.rotation = Quaternion.Euler(0, moveDirection == 1 ? 180 : 0, 0);
    }

}
