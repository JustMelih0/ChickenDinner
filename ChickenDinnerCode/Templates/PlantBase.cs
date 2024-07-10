using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlantController))]
public abstract class PlantBase : MonoBehaviour
{
    [SerializeField]protected float growTimer;
    protected PlantController plantController;
    protected Animator anim;

    private void Start() 
    {

        plantController=GetComponent<PlantController>();
        anim=GetComponent<Animator>();

    }

    protected virtual void Grow()
    {

        plantController.canHarvest=true;
        plantController.Interactable=true;
        anim.SetBool("grow",true);

    }
    public virtual void HarvestPlant()
    {
        plantController.canHarvest=false;
        plantController.Interactable=false;
        anim.SetTrigger("growing");
        anim.SetBool("grow",false);
        Invoke(nameof(Grow),growTimer);

    }
    
    
}
