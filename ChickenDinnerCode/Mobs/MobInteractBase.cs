using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MobController))]
public abstract class MobInteractBase : MonoBehaviour
{
    [SerializeField]protected float damage;
    [SerializeField]protected float reInteractTime;

    #region CheckArea

        [SerializeField]protected float interactRadius;
        [SerializeField]protected Transform interactPoint;
        [SerializeField]protected LayerMask interactLayer;

    #endregion

    protected MobController mobController;
    protected Animator anim;
    protected List<Collider2D> targets = new List<Collider2D>();

    void Start()
    {
        anim = GetComponent<Animator>();
        mobController=GetComponent<MobController>();
    }

    
    void FixedUpdate()
    {
        InteractControl();
    }
    protected virtual void InteractControl()
    {
        if (mobController.canInteract==false)
        {
            return;
        }

        Collider2D target=CheckArea();

        if (target)
        {
            Interact(target);
            mobController.canInteract=false;
            mobController.canMove=false;
            Invoke(nameof(ResetInteract),reInteractTime);
        }
    }
    protected virtual void Interact(Collider2D target)
    {
       anim.SetTrigger("eat");
       IA_Hitable hitable=target.GetComponent<IA_Hitable>();
       hitable.Hit(damage,false);
    }

    protected virtual void ResetInteract()
    {
        mobController.canInteract=true;
        mobController.canMove=true;
    }

    
    protected virtual Collider2D CheckArea()
    {
        targets.Clear();
        targets.AddRange(Physics2D.OverlapCircleAll(interactPoint.position , interactRadius , interactLayer));

        foreach (Collider2D item in targets)
        {
            if (item.GetComponent<ControllerBase>().Interactable)
            {
                return item;
            }
        }

        return null;

    }

    private void OnDrawGizmos() {
        Gizmos.color=Color.red;
        Gizmos.DrawWireSphere(interactPoint.position,interactRadius);
    }
    
    
}
