using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerController))]
public class PlayerInteract : MonoBehaviour
{
   

   [SerializeField]private float damage;

   #region CheckArea

    [SerializeField]private Transform interactPoint;
    [SerializeField]private float interactRadius;
    [SerializeField]private LayerMask interactLayer;

   #endregion
    
   #region Components

      private InputAction interactInput;
      private PlayerController playerController;
      private Animator anim;

   #endregion

 

   private void Awake() 
   {

      var inputActionAsset = GetComponent<PlayerInput>().actions;

      interactInput = inputActionAsset.FindAction("Interact");
   }
   private void Start() 
   {
    playerController = GetComponent<PlayerController>();
    anim = GetComponent<Animator>();

   }
   private void OnEnable()
   {
      interactInput.Enable();
      interactInput.performed += OnInteractAction;      

   }
   private void OnDisable()
   {
      interactInput.Disable();
      interactInput.performed -= OnInteractAction; 
   }
   private void OnInteractAction(InputAction.CallbackContext context)
   {
      
      if(playerController.canInteract==false)
      return;

      if (CheckArea()!=null)
      {

        Interact();

      }
   }
  
   private void Interact()
   {

      Collider2D currentTarget=CheckArea();

      if(currentTarget==null)
      {
            return;
      }

      anim.SetTrigger("attack");
      IA_Hitable hitable=currentTarget?.GetComponent<IA_Hitable>();
      hitable.Hit(damage,true);

   }


   Collider2D[] targets= new Collider2D[5];
   Collider2D CheckArea()
   {
     
      targets=Physics2D.OverlapCircleAll(interactPoint.position,interactRadius,interactLayer);

      foreach (Collider2D item in targets)
      {
         if (item.GetComponent<ControllerBase>().Interactable)
         {
            return item;
         }
      }

      return null;

   }

   private void OnDrawGizmos() 
   {
     
    Gizmos.color=Color.blue;
    Gizmos.DrawWireSphere(interactPoint.position,interactRadius);

   }

}
