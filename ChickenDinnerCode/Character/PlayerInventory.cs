using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerController))]
public class PlayerInventory : MonoBehaviour,IA_Claimable
{
    [System.Serializable]
    public class Slot
    {
      public ItemBase.ItemType itemType;
      public float itemCount;

    }

    public List<Slot> slots=new List<Slot>();
    public List<TextMeshProUGUI> slotText=new List<TextMeshProUGUI>();

   #region CheckArea

     [SerializeField]private float claimRange;
     [SerializeField]private LayerMask claimLayer;

   #endregion

    #region Components

      private InputAction claimInput;
     
      private PlayerController playerController;
      private Animator anim;

    #endregion
    private void Awake() 
    {
       var inputActionAsset=GetComponent<PlayerInput>().actions;

       claimInput=inputActionAsset.FindAction("Claim");
    }
    private void OnEnable() 
    {
      claimInput.Enable();
      claimInput.performed += OnClaimInput;
    }
    private void OnDisable() 
    {
      claimInput.Disable();
      claimInput.performed -= OnClaimInput;
    }
    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        anim = GetComponent<Animator>();
    }



    private void OnClaimInput(InputAction.CallbackContext context)
    {
        if (CheckArea() != null)
        {
            ClaimRequest();
        }
    }

    void ClaimRequest()
    {
        if (playerController.canInteract==false)
        {
            return;
        }

       Collider2D currentTarget=CheckArea();

       if(currentTarget==null)
       {
            return;
       }

        IA_Claimable claimable=currentTarget.GetComponent<IA_Claimable>();
        ItemBase.ItemType targetItemtype=currentTarget.GetComponent<ClaimablePlatform>().desiredItemType;
        foreach (Slot item in slots)
        {
          if (item.itemType==targetItemtype)
          { 

            bool success = claimable.Claim(item.itemCount,item.itemType);
            if (success)
            {
                anim.SetTrigger("interact");
                AudioManager.Instance.PlaySFX("Interact");
            }
            

          }
        }       
    }

    Collider2D[] targets= new Collider2D[5];
    Collider2D CheckArea()
    {
     
      targets=Physics2D.OverlapCircleAll(transform.position,claimRange,claimLayer);

      foreach (Collider2D item in targets)
      {
         if (item.GetComponent<ControllerBase>().Interactable)
         {
            return item;
         }
      }

      return null;

    }
    public Slot HasItem(ItemBase.ItemType itemType)
    {
      return slots.Find(item => item.itemType == itemType);
    }
    public void SetItem(ItemBase.ItemType type,float count)
    {
       foreach(Slot item in slots)
       {
           if (item.itemType==type)
           {
              item.itemCount+=count;
              RefreshUI();
           }
       }
    }
    void RefreshUI()
    {
       for (int i = 0; i < slots.Count; i++)
       {
          slotText[i].text = "x"+slots[i].itemCount;
       }
    }

    public bool Claim(float count, ItemBase.ItemType itemType)
    {
        SetItem(itemType,count);
        return true;
    }
    private void OnDrawGizmos() {
        Gizmos.color=Color.magenta;
        Gizmos.DrawWireSphere(transform.position,claimRange);
    }
}
